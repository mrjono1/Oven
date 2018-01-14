using MasterBuilder.Interfaces;

namespace MasterBuilder.Templates.ClientApp.Test
{
    /// <summary>
    /// Boot test template
    /// </summary>
    public class BootTestTemplate : ITemplate
    {
        /// <summary>
        /// get file name
        /// </summary>
        public string GetFileName()
        {
            return "boot-test.js";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new[] { "ClientApp", "test" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return @"Error.stackTraceLimit = Infinity;
// Load required polyfills and testing libraries
require('core-js'); // Added for Phantomjs
require('zone.js');
require('zone.js/dist/long-stack-trace-zone');
require('zone.js/dist/proxy.js');
require('zone.js/dist/sync-test');
require('zone.js/dist/jasmine-patch');
require('zone.js/dist/async-test');
require('zone.js/dist/fake-async-test');
const testing = require('@angular/core/testing');
const testingBrowser = require('@angular/platform-browser-dynamic/testing');

// Prevent Karma from running prematurely
__karma__.loaded = function () {};

// First, initialize the Angular testing environment
testing.getTestBed().initTestEnvironment(
    testingBrowser.BrowserDynamicTestingModule,
    testingBrowser.platformBrowserDynamicTesting()
);

// Then we find all the tests
const context = require.context('../', true, /\.spec\.ts$/);

// And load the modules
context.keys().map(context);

// Finally, start Karma to run the tests
__karma__.start();";
        }
    }
}
