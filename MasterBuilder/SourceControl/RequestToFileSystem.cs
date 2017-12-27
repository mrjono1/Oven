using LibGit2Sharp;
using MasterBuilder.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MasterBuilder.SourceControl
{
    public class RequestToFileSystem
    {
        private readonly string _baseDirectory;
        private readonly Project _project;
        private readonly string _username;
        private readonly string _email;
        private readonly string _personalAccessToken;

        public RequestToFileSystem(string baseDirectory, Project project, string username, string email, string personalAccessToken)
        {
            _baseDirectory = baseDirectory;
            _project = project;
            _username = username;
            _email = email;
            _personalAccessToken = personalAccessToken;
        }

        public async Task Write()
        {
            FileHelper.DeleteFilesInDirectory("json", _baseDirectory);
            await WriteObject("project.json", _project, false);

            FileHelper.DeleteFilesInDirectory("json", _baseDirectory, "Entities");
            if (_project.Entities != null)
            {
                foreach (var entity in _project.Entities)
                {
                    await WriteObject($"{entity.Id}.json", entity, true, "Entities");
                }
            }
            FileHelper.DeleteFilesInDirectory("json", _baseDirectory, "Screens");
            if (_project.Screens != null)
            {
                foreach (var screen in _project.Screens)
                {
                    await WriteObject($"{screen.Id}.json", screen, true, "Screens");
                }
            }
            FileHelper.DeleteFilesInDirectory("json", _baseDirectory, "MenuItems");
            if (_project.MenuItems != null)
            {
                foreach (var menuItem in _project.MenuItems)
                {
                    await WriteObject($"{menuItem.Id}.json", menuItem, true, "MenuItems");
                }
            }
            FileHelper.DeleteFilesInDirectory("json", _baseDirectory, "WebServices");
            if (_project.WebServices != null)
            {
                foreach (var webService in _project.WebServices)
                {
                    await WriteObject($"{webService.Id}.json", webService, true, "WebServices");
                }
            }
        }

        internal async Task SetupAndGetRepos()
        {
            var path = Path.Combine(_baseDirectory, "json");
            
            var vsts = new VisualStudioTeamServices(_username, _project.InternalName, _personalAccessToken);
            var project = await vsts.GetProject();
            if (project == null)
            {
                project = await vsts.CreateProject(_project.InternalName, _project.Title);
            }
            if (project == null)
            {
                return;
            }

            var repositories = await vsts.GetProjectRepositories();

            Models.GetRepository jsonRepo = null;
            if (repositories != null)
            {
                foreach (var repo in repositories)
                {
                    if (repo.Name.Equals("Json", StringComparison.OrdinalIgnoreCase))
                    {
                        jsonRepo = repo;
                    }
                }
            }

            if (jsonRepo == null)
            {
                jsonRepo = await vsts.CreateRepository("Json");
            }

            if (!Repository.IsValid(_baseDirectory))
            {
                //var cloneOptions = new CloneOptions
                //{
                //    CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials
                //    {
                //        Username = "Basic",
                //        Password = _personalAccessToken
                //    }
                //};
                //Repository.Clone(jsonRepo.RemoteUrl, _baseDirectory, cloneOptions);

                var rootedPath = Repository.Init(_baseDirectory);
                using (var repository = new Repository(_baseDirectory))
                {
                    
                    var remotes = repository.Network.Remotes;
                    remotes.Add("origin", jsonRepo.RemoteUrl);
                }
            }


        }

        internal void LocalGit()
        {
            

            using (var repository = new Repository(_baseDirectory))
            {
                var remotes = repository.Network.Remotes;

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
        }

        private async Task WriteObject(string fileName, object obj, bool includeCollections, params string[] folder)
        {
            string jsonString = null;
            if (includeCollections)
            {
                jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented);
            }
            else
            {
                jsonString = SerializeObjectWithoutCollections(obj);
            }
            var directory = FileHelper.CreateFolder(_baseDirectory, folder);
            await FileHelper.WriteAllText(Path.Combine(directory, fileName), jsonString);
        }

        public string SerializeObjectWithoutCollections(object obj)
        {
            using (var strWriter = new StringWriter())
            {
                var resolver = new CustomContractResolver();
                var serializer = new JsonSerializer
                {
                    ContractResolver = resolver,
                    Formatting = Formatting.Indented
                };
                serializer.Serialize(strWriter, obj);
                
                return strWriter.ToString();
            }
        }
    }

    public class CustomContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(
            MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (property.PropertyType != typeof(string) &&
                typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                property.ShouldSerialize = instance =>
                {
                    // this value could be in apublic property
                    if (member.MemberType ==  MemberTypes.Property)
                    {
                        IEnumerable enumerable = instance
                                .GetType()
                                .GetProperty(member.Name)
                                .GetValue(instance, null) as IEnumerable;
                        return enumerable == null;
                    }
                    else
                    {
                        return true;
                    }
                };
            }

            return property;
        }
    }
}
