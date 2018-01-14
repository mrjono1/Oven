using MasterBuilder.Interfaces;

namespace MasterBuilder.Templates.ClientApp
{
    /// <summary>
    /// Boot browser ts template
    /// </summary>
    public class BootBrowserTsTemplate: ITemplate
    {
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "boot.browser.ts";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new[] { "ClientApp" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return @"import './polyfills/browser.polyfills';
import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module.browser';

// Enable either Hot Module Reloading or production mode
if (module['hot']) {
    module['hot'].accept();
    module['hot'].dispose(() => {
        modulePromise.then(appModule => appModule.destroy());
    });
} else {
    enableProdMode();
}

const modulePromise = platformBrowserDynamic().bootstrapModule(AppModule);";
        }
    }
}
