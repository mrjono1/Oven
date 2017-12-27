using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MasterBuilder.SourceControl
{
    public class VisualStudioTeamServices
    {
        private readonly RestClient _restClient;
        private readonly string _project;

        public Guid? ProjectId { get; set; }

        public VisualStudioTeamServices(string account, string project, string personalAccessToken)
        {
            _restClient = new RestClient($"https://{account}.visualstudio.com/DefaultCollection/")
            {
                Authenticator = new HttpBasicAuthenticator("Basic", personalAccessToken)
            };
            _project = project;
        }
        public async Task<Models.GetProject> GetProject()
        {
            var request = new RestRequest($"_apis/projects/{_project}", Method.GET);
            request.AddQueryParameter("api-version", "1.0");
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            
            var result = await _restClient.ExecuteTaskAsync<Models.GetProject>(request);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ProjectId = result.Data.Id;
                return result.Data;
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                throw new Exception(result.Content);
            }
        }


        public async Task<Models.GetProject> CreateProject(string name, string description)
        {
            var request = new RestRequest($"_apis/projects", Method.POST);
            request.AddQueryParameter("api-version", "2.0-preview");
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            var body = new Models.CreateProject
            {
                Name = name,
                Description = description
            };
            var bodyString = JsonConvert.SerializeObject(body, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            request.AddParameter("application/json", bodyString, ParameterType.RequestBody);

            var result = await _restClient.ExecuteTaskAsync<Models.GetProject>(request);

            if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                return result.Data;
            }
            else
            {
                throw new Exception(result.Content);
            }
        }

        public async Task<Models.GetRepository[]> GetProjectRepositories()
        {
            var request = new RestRequest($"{_project}/_apis/git/repositories", Method.GET);
            request.AddQueryParameter("api-version", "1.0");
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");

            var result = await _restClient.ExecuteTaskAsync(request);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var data = JsonConvert.DeserializeObject<Models.VssJsonCollectionWrapper>(result.Content);
                return data.Value;
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                throw new Exception(result.Content);
            }
        }
        public async Task<Models.GetRepository> CreateRepository(string name)
        {
            var request = new RestRequest($"{_project}/_apis/git/repositories", Method.POST);
            request.AddQueryParameter("api-version", "1.0");
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            var body = new Models.CreateRepository
            {
                Name = name,
                Project = new Models.CreateRepository.RepoProject()
                {
                    Id = ProjectId.Value
                }
            };
            var bodyString = JsonConvert.SerializeObject(body, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            request.AddParameter("application/json", bodyString, ParameterType.RequestBody);

            var result = await _restClient.ExecuteTaskAsync<Models.GetRepository>(request);

            if (result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return result.Data;
            }
            else
            {
                throw new Exception(result.Content);
            }
        }
    }
}
