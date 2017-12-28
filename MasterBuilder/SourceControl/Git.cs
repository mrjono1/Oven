using LibGit2Sharp;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MasterBuilder.SourceControl
{
    public class Git
    {
        private readonly string _baseDirectory;
        private readonly Project _project;
        private readonly string _username;
        private readonly string _email;
        private readonly string _personalAccessToken;

        public Git(string baseDirectory, Project project, string username, string email, string personalAccessToken)
        {
            _baseDirectory = baseDirectory;
            _project = project;
            _username = username;
            _email = email;
            _personalAccessToken = personalAccessToken;
        }


        internal async Task<Dictionary<string, Models.GetRepository>> SetupAndGetRepos()
        {
            var vsts = new VisualStudioTeamServices(_username, _project.InternalName, _personalAccessToken);
            var project = await vsts.GetProject();
            if (project == null)
            {
                project = await vsts.CreateProject(_project.InternalName, _project.Title);
            }
            if (project == null)
            {
                return null;
            }

            var repositoriesNeeded = new Dictionary<string, Models.GetRepository>
            {
                { "Json", null },
                { _project.InternalName, null}
            };

            // Get repositories from VSTS
            var repositories = await vsts.GetProjectRepositories();
            if (repositories != null)
            {
                foreach (var repo in repositories)
                {
                    if (repositoriesNeeded.ContainsKey(repo.Name))
                    {
                        repositoriesNeeded[repo.Name] = repo;
                    }
                }
            }

            var repositoriesCreated = new Dictionary<string, Models.GetRepository>() { };
            // Double check each repository is setup correctly
            foreach (var repositoryNeeded in repositoriesNeeded)
            {
                var path = Path.Combine(_baseDirectory, repositoryNeeded.Key);
                repositoriesCreated.Add(repositoryNeeded.Key, await EnsureRepoistoryIsInitialised(repositoryNeeded.Key, path, repositoryNeeded.Value, vsts));
            }

            return repositoriesCreated;
        }

        internal async Task<Models.GetRepository> EnsureRepoistoryIsInitialised(string name, string directory, Models.GetRepository repo, VisualStudioTeamServices vsts)
        {
            if (repo == null)
            {
                repo = await vsts.CreateRepository(name);
            }

            // Create repo folder
            FileHelper.CreateFolder(directory);

            // Check if local repo has been initalised
            if (!Repository.IsValid(directory))
            {
                // if not intialised then also check if directory is empty if not clear it
                if (!FileHelper.IsDirectoryEmpty(directory))
                {
                    Directory.Delete(directory, true);
                    FileHelper.CreateFolder(directory);
                }

                // clone remote repo
                var cloneOptions = new CloneOptions
                {
                    CredentialsProvider =
                    (_url, _user, _cred) => new UsernamePasswordCredentials
                    {
                        Username = "Basic",
                        Password = _personalAccessToken
                    }
                };
                Repository.Clone(repo.RemoteUrl, directory, cloneOptions);
            }

            return repo;
        }

        /// <summary>
        /// Stage, Commit, and Push
        /// </summary>
        internal void StageCommitPush(Models.GetRepository getRepository)
        {
            var path = Path.Combine(_baseDirectory, getRepository.Name);
            try
            {
                using (var repository = new Repository(path))
                {
                    if (!repository.RetrieveStatus().IsDirty)
                    {
                        return;
                    }

                    Commands.Stage(repository, "*");

                    // Create the committer's signature and commit
                    Signature author = new Signature(_username, _email, DateTime.Now);
                    Signature committer = author;

                    // Commit to the repository
                    Commit commit = repository.Commit("Here's a commit i made!", author, committer);

                    var remote = repository.Network.Remotes["origin"];
                    var options = new PushOptions
                    {
                        CredentialsProvider = new LibGit2Sharp.Handlers.CredentialsHandler(
                        (url, usernameFromUrl, types) =>
                            new UsernamePasswordCredentials()
                            {
                                Username = "Basic",
                                Password = _personalAccessToken
                            })
                    };
                    var pushRefSpec = @"refs/heads/master";
                    repository.Network.Push(remote, pushRefSpec, options);
                }
            } catch (Exception ex)
            {
                var ss = ex;
            }
        }
    }
}
