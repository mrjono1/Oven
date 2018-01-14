using MasterBuilder.Interfaces;

namespace MasterBuilder.Templates.ClientApp.App.Models
{
    /// <summary>
    /// Patch Operation Template
    /// </summary>
    public class OperationTemplate : ITemplate
    {
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "Operation.ts";
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
            return @"export class Operation {
    op: string;
    path: string;
    value: any;
}";
        }
    }
}
