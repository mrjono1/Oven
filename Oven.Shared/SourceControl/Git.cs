using LibGit2Sharp;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Oven.SourceControl
{
    /// <summary>
    /// Git Integration
    /// </summary>
    public class Git
    {
        private readonly string BaseDirectory;
        private readonly Project Project;
        private readonly string Username;
        private readonly string Email;
        private readonly string PersonalAccessToken;

        /// <summary>
        /// Constructor
        /// </summary>
        public Git(string baseDirectory, Project project, string username, string email, string personalAccessToken)
        {
            BaseDirectory = baseDirectory;
            Project = project;
            Username = username;
            Email = email;
            PersonalAccessToken = personalAccessToken;
        }

        /// <summary>
        /// Setup and Get Repositories
        /// </summary>
        public async Task<Dictionary<string, Models.GetRepository>> SetupAndGetRepos()
        {
            var vsts = new VisualStudioTeamServices(Username, Project.InternalName, PersonalAccessToken);
            var project = await vsts.GetProject();
            if (project == null)
            {
                project = await vsts.CreateProject(Project.InternalName, Project.Title);
            }
            if (project == null)
            {
                return null;
            }

            var repositoriesNeeded = new Dictionary<string, Models.GetRepository>
            {
                { "Json", null },
                { Project.InternalName, null}
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
                var path = Path.Combine(BaseDirectory, repositoryNeeded.Key);
                repositoriesCreated.Add(repositoryNeeded.Key, await EnsureRepoistoryIsInitialised(repositoryNeeded.Key, path, repositoryNeeded.Value, vsts));
            }

            return repositoriesCreated;
        }

        /// <summary>
        /// Ensure Repository is Intitialised
        /// </summary>
        public async Task<Models.GetRepository> EnsureRepoistoryIsInitialised(string name, string directory, Models.GetRepository repo, VisualStudioTeamServices vsts)
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
                        Password = PersonalAccessToken
                    }
                };
                Repository.Clone(repo.RemoteUrl, directory, cloneOptions);
            }
            else
            {
                Pull(repo);
            }

            return repo;
        }

        /// <summary>
        /// Pull
        /// </summary>
        public void Pull(Models.GetRepository getRepository)
        {
            var path = Path.Combine(BaseDirectory, getRepository.Name);

            using (var repository = new Repository(path))
            {
                var masterBranch = repository.Branches["master"];
                if (masterBranch == null)
                {
                    // nothing to pull
                    return;
                }

                if (repository.RetrieveStatus().IsDirty)
                {
                    Commands.Checkout(repository, masterBranch, new CheckoutOptions()
                    {
                        CheckoutModifiers = CheckoutModifiers.Force
                    });
                }
                
                Commands.Pull(repository,
                    new Signature(Username, Email, DateTime.Now),
                    new PullOptions()
                    {
                        FetchOptions = new FetchOptions()
                        {
                            CredentialsProvider = new LibGit2Sharp.Handlers.CredentialsHandler(
                                (url, usernameFromUrl, types) =>
                            new UsernamePasswordCredentials()
                            {
                                Username = "Basic",
                                Password = PersonalAccessToken
                            })
                        }
                    }
                );
            }
        }

        /// <summary>
        /// Stage, Commit, and Push
        /// </summary>
        public void StageCommitPush(Models.GetRepository getRepository, string message)
        {
            var path = Path.Combine(BaseDirectory, getRepository.Name);

            using (var repository = new Repository(path))
            {
                if (!repository.RetrieveStatus().IsDirty)
                {
                    return;
                }

                Commands.Stage(repository, "*");

                // Create the committer's signature and commit
                Signature author = new Signature(Username, Email, DateTime.Now);
                Signature committer = author;

                // Commit to the repository
                Commit commit = repository.Commit(message, author, committer);

                var remote = repository.Network.Remotes["origin"];
                if (!remote.Url.Equals(getRepository.RemoteUrl, StringComparison.OrdinalIgnoreCase))
                {
                    repository.Network.Remotes.Update("origin", r => r.Url = getRepository.RemoteUrl);
                    remote = repository.Network.Remotes["origin"];
                }
                var options = new PushOptions
                {
                    CredentialsProvider = new LibGit2Sharp.Handlers.CredentialsHandler(
                    (url, usernameFromUrl, types) =>
                        new UsernamePasswordCredentials()
                        {
                            Username = "Basic",
                            Password = PersonalAccessToken
                        })
                };
                var pushRefSpec = @"refs/heads/master";
                repository.Network.Push(remote, pushRefSpec, options);
            }
        }

        public bool FolderChanged(Models.GetRepository getRepository, string folder)
        {
            var path = Path.Combine(BaseDirectory, getRepository.Name);

            using (var repository = new Repository(path))
            {
                var status = repository.RetrieveStatus(new StatusOptions
                {
                    PathSpec = new string[] { folder }
                });

                foreach (var statusEntry in status)
                {
                    if (statusEntry.State != FileStatus.Ignored)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
