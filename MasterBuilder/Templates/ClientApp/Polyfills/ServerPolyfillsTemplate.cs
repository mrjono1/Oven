using MasterBuilder.Helpers;

namespace MasterBuilder.Templates.ClientApp.Polyfills
{
    public class ServerPolyfillsTemplate : ITemplate
    {
        public string GetFileName()
        {
            return "server.polyfills.ts";
        }

        public string[] GetFilePath()
        {
            return new[] { "ClientApp", "polyfills" };
        }
        public string GetFileContent()
        {
            return @"import './polyfills.ts';
import 'reflect-metadata';
import 'zone.js';";
        }
    }
}
