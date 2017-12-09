using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.ProjectFiles
{
    public class WebConfigTemplate
    {
        public static string FileName()
        {
            return "web.config";
        }
        public static string Evaluate(Project project)
        {
            return @"<?xml version=""1.0"" encoding=""utf-8""?>
<configuration>

  <!--
    Configure your application settings in appsettings.json. Learn more at https://go.microsoft.com/fwlink/?LinkId=786380
  -->

  <system.webServer>
    <handlers>
      <add name=""aspNetCore"" path=""*"" verb=""*"" modules=""AspNetCoreModule"" resourceType=""Unspecified""/>
    </handlers>
    <aspNetCore processPath=""%LAUNCHER_PATH%"" arguments=""%LAUNCHER_ARGS%"" stdoutLogEnabled=""false"" stdoutLogFile="".\\logs\\stdout"" forwardWindowsAuthToken=""false""/>
  </system.webServer>
</configuration>";
        }
    }
}
