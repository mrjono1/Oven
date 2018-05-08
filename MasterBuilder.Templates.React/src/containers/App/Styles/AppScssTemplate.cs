using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Src.Containers.App.Styles
{
    /// <summary>
    /// app.scss Template
    /// </summary>
    public class AppScssTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public AppScssTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "app.scss";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "src", "containers", "App", "styles" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return @"/* global styles */

@import 'variables';
@import 'third-party/normalize';
@import 'fonts/roboto';
@import 'typography';
@import 'links';

:global(body) {

	position: relative;

  img {
    max-width: 100%;
  }

  p {
  	color: #333333;
  	line-height:1.3;
  	margin-bottom:4px;
  }

  ul {
    padding: 0;
    margin: 0;
  }

  li {
    list-style-type: none;
  }

  fieldset {
    border: none;
  }

  .container{
    width: 1350px;
  }

  .error {
    color: #a94442;
    background-color: #f2dede;
    display: inline-block;
    padding: 5px 10px;
    border-radius: $border-radius;
  }

}

@media (max-width: 1414px) {
  .container {
    width: 100% !important;
  }
}";
        }
    }
}