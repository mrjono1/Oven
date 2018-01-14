using MasterBuilder.Interfaces;

namespace MasterBuilder.Templates.ClientApp.Polyfills
{
    /// <summary>
    /// Server polufills template
    /// </summary>
    public class ServerPolyfillsTemplate : ITemplate
    {
        /// <summary>
        /// Get file name
        /// </summary>
        /// <returns></returns>
        public string GetFileName()
        {
            return "server.polyfills.ts";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new[] { "ClientApp", "polyfills" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return @"import './polyfills.ts';
import 'reflect-metadata';
import 'zone.js';";
        }
    }
}
