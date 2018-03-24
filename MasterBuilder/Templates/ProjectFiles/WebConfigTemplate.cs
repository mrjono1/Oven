using MasterBuilder.Interfaces;

namespace MasterBuilder.Templates.ProjectFiles
{
    /// <summary>
    /// Web config
    /// </summary>
    public class WebConfigTemplate : ITemplate
    {
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "web.config";
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
            return @"<?xml version=""1.0"" encoding=""utf-8""?>
<configuration>

  <!--
    Configure your application settings in appsettings.json. Learn more at https://go.microsoft.com/fwlink/?LinkId=786380
  -->

  <system.webServer>
    <handlers>
      <add name=""aspNetCore"" path=""*"" verb=""*"" modules=""AspNetCoreModule"" resourceType=""Unspecified"" />
    </handlers>
    <aspNetCore processPath=""%LAUNCHER_PATH%"" arguments=""%LAUNCHER_ARGS%"" stdoutLogEnabled=""false"" stdoutLogFile="".\\logs\\stdout"" forwardWindowsAuthToken=""false"" />
  </system.webServer>
</configuration>";
        }
    }
}
