using MasterBuilder.Interfaces;

namespace MasterBuilder.Templates.ClientApp.Polyfills
{
    /// <summary>
    /// Browser polyfills template
    /// </summary>
    public class BrowserPolyfillsTemplate : ITemplate
    {
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "browser.polyfills.ts";
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
import 'zone.js/dist/zone';
import 'reflect-metadata';";
        }
    }
}
