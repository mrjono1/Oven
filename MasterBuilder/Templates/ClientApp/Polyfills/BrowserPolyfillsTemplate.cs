using MasterBuilder.Helpers;

namespace MasterBuilder.Templates.ClientApp.Polyfills
{
    public class BrowserPolyfillsTemplate : ITemplate
    {
        public string GetFileName()
        {
            return "browser.polyfills.ts";
        }

        public string[] GetFilePath()
        {
            return new[] { "ClientApp", "polyfills" };
        }
        public string GetFileContent()
        {
            return @"import './polyfills.ts';
import 'zone.js/dist/zone';
import 'reflect-metadata';";
        }
    }
}
