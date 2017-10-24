using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.Views.Shared
{
    public class ErrorTemplate
    {
        public static string FileName(string folder)
        {
            return Path.Combine(FileHelper.CreateFolder(folder, "Shared"), "Error.cshtml");
        }

        public static string Evaluate(Project project)
        {
            return $@"@{{
    ViewData[""Title""] = ""Error"";
}}

<h1 class=""text-danger"">Error.</h1>
<h2 class=""text-danger"">An error occurred while processing your request.</h2>

@if (!string.IsNullOrEmpty((string)ViewData[""RequestId""]))
{{
    <p>
        <strong>Request ID:</strong> <code>@ViewData[""RequestId""]</code>
    </p>
}}

<h3>Development Mode</h3>
<p>
    Swapping to <strong>Development</strong> environment will display more detailed information about the error that occurred.
</p>
<p>
    <strong>Development environment should not be enabled in deployed applications</strong>, as it can result in sensitive information from exceptions being displayed to end users. For local debugging, development environment can be enabled by setting the <strong>ASPNETCORE_ENVIRONMENT</strong> environment variable to <strong>Development</strong>, and restarting the application.
</p>";
        }
    }
}