using MasterBuilder.Helpers;
using MasterBuilder.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace MasterBuilder.SourceControl
{
    /// <summary>
    /// Save request as json
    /// </summary>
    public class RequestToFileSystem
    {
        private readonly string _baseDirectory;
        private readonly Project _project;

        /// <summary>
        /// Constructor
        /// </summary>
        public RequestToFileSystem(string topProjectDirectory, Project project)
        {
            _baseDirectory = FileHelper.CreateFolder(topProjectDirectory, "Json");
            _project = project;
        }

        /// <summary>
        /// Write files
        /// </summary>
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
            FileHelper.DeleteFilesInDirectory("json", _baseDirectory, "Services");
            if (_project.Services != null)
            {
                foreach (var service in _project.Services)
                {
                    await WriteObject($"{service.Id}.json", service, true, "Services");
                }
            }
        }

        /// <summary>
        /// Write Object
        /// </summary>
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

        /// <summary>
        /// Seralize object
        /// </summary>
        private string SerializeObjectWithoutCollections(object obj)
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
}
