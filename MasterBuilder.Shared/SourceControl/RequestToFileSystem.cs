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
        private readonly string BaseDirectory;
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public RequestToFileSystem(string topProjectDirectory, Project project)
        {
            BaseDirectory = FileHelper.CreateFolder(topProjectDirectory, "Json");
            Project = project;
        }

        /// <summary>
        /// Write files
        /// </summary>
        public async Task Write()
        {
            FileHelper.DeleteFilesInDirectory("json", BaseDirectory);
            await WriteObject("project.json", Project, false);

            FileHelper.DeleteFilesInDirectory("json", BaseDirectory, "Entities");
            if (Project.Entities != null)
            {
                foreach (var entity in Project.Entities)
                {
                    await WriteObject($"{entity.Id}.json", entity, true, "Entities");
                }
            }
            FileHelper.DeleteFilesInDirectory("json", BaseDirectory, "Screens");
            if (Project.Screens != null)
            {
                foreach (var screen in Project.Screens)
                {
                    await WriteObject($"{screen.Id}.json", screen, true, "Screens");
                }
            }
            FileHelper.DeleteFilesInDirectory("json", BaseDirectory, "MenuItems");
            if (Project.MenuItems != null)
            {
                foreach (var menuItem in Project.MenuItems)
                {
                    await WriteObject($"{menuItem.Id}.json", menuItem, true, "MenuItems");
                }
            }
            FileHelper.DeleteFilesInDirectory("json", BaseDirectory, "Services");
            if (Project.Services != null)
            {
                foreach (var service in Project.Services)
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
            var directory = FileHelper.CreateFolder(BaseDirectory, folder);
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
