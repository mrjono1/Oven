using MasterBuilder.Interfaces;

namespace MasterBuilder.Templates.ClientApp.App.Models.Reference
{
    /// <summary>
    /// Reference Request Template
    /// </summary>
    public class ReferenceRequestTemplate : ITemplate
    {
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"ReferenceRequest.ts";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "ClientApp", "app", "models" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"export class ReferenceRequest {{
    page: number;
    pageSize: number;
    query: string;
}}";
        }
    }
}
