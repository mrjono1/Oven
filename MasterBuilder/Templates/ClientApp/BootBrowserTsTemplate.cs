using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System.IO;

namespace MasterBuilder.Templates.ClientApp
{
    public class BootBrowserTsTemplate: ITemplate
    {
        public string GetFileName()
        {
            return "boot.browser.ts";
        }

        public string[] GetFilePath()
        {
            return new[] { "ClientApp" };
        }

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
