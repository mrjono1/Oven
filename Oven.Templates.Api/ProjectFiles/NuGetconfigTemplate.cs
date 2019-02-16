using Oven.Interfaces;
using Oven.Request;
using System;
using System.Text;

namespace Oven.Templates.Api.ProjectFiles
{
    /// <summary>
    /// Nuget Config
    /// </summary>
    public class NuGetconfigTemplate: ITemplate
    {
        private Project Project { get; set; }
        /// <summary>
        /// NuGet config Constructor
        /// </summary>
        public NuGetconfigTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "NuGet.config";
        }
        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { };
        }
        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            StringBuilder packages = new StringBuilder();
            StringBuilder credentials = new StringBuilder();
            StringBuilder apikeys = new StringBuilder();

            if (Project.NuGetPackageSources != null)
            {
                foreach (var src in Project.NuGetPackageSources)
                {
                    packages.AppendLine($@"    <add key=""{src.Key}"" value=""{src.Value}"" />");

                    if (!String.IsNullOrEmpty(src.ApiKey))
                    {
                        apikeys.AppendLine($@"    <add key=""{src.Value}"" value=""{src.Value}"" />");
                    }
                    if (!String.IsNullOrEmpty(src.Username))
                    {
                        credentials.AppendLine($@"    <{src.Key}>
      <add key=""Username"" value=""{src.Username}"" />
      <add key=""ClearTextPassword"" value=""{src.ClearTextPassword}"" />
    </{src.Key}>");
                    }
                }
            }

            return $@"<?xml version=""1.0"" encoding=""utf-8""?>
<configuration>
  <packageSources>
    <add key=""nuget.org"" value=""https://www.nuget.org/api/v2/"" />
{packages.ToString()}
  </packageSources>
  <packageSourceCredentials>
{credentials.ToString()}
  </packageSourceCredentials>
  <apikeys>
{apikeys.ToString()}
  </apikeys>
</configuration>";
        }
    }
}
