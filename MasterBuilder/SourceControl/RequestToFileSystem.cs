using MasterBuilder.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MasterBuilder.SourceControl
{
    public class RequestToFileSystem
    {
        private readonly string _baseDirectory;
        private readonly Project _project;

        public RequestToFileSystem(string baseDirectory, Project project)
        {
            _baseDirectory = baseDirectory;
            _project = project;
        }

        public async Task Write()
        {
            FileHelper.DeleteFilesInDirectory(_baseDirectory);
            await WriteObject("project.json", _project, false);

            FileHelper.DeleteFilesInDirectory(_baseDirectory, "Entities");
            if (_project.Entities != null)
            {
                foreach (var entity in _project.Entities)
                {
                    await WriteObject($"{entity.Id}.json", entity, true, "Entities");
                }
            }
            FileHelper.DeleteFilesInDirectory(_baseDirectory, "Screens");
            if (_project.Screens != null)
            {
                foreach (var screen in _project.Screens)
                {
                    await WriteObject($"{screen.Id}.json", screen, true, "Screens");
                }
            }
            FileHelper.DeleteFilesInDirectory(_baseDirectory, "MenuItems");
            if (_project.MenuItems != null)
            {
                foreach (var menuItem in _project.MenuItems)
                {
                    await WriteObject($"{menuItem.Id}.json", menuItem, true, "MenuItems");
                }
            }
            FileHelper.DeleteFilesInDirectory(_baseDirectory, "WebServices");
            if (_project.WebServices != null)
            {
                foreach (var webService in _project.WebServices)
                {
                    await WriteObject($"{webService.Id}.json", webService, true, "WebServices");
                }
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
