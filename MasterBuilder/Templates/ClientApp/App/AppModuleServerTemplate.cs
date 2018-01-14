using MasterBuilder.Interfaces;

namespace MasterBuilder.Templates.ClientApp.App
{
    /// <summary>
    /// App module server template
    /// </summary>
    public class AppModuleServerTemplate : ITemplate
    {
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "app.module.server.ts";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new[] { "ClientApp", "app" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return @"import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { BrowserModule } from '@angular/platform-browser';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

import { AppModuleShared } from './app.module';
import { AppComponent } from './app.component';
import { ServerTransferStateModule } from '@angular/platform-server';

import { ServerPrebootModule } from 'preboot/server';

@NgModule({
  bootstrap: [AppComponent],
  imports: [
    // Our Common AppModule
    AppModuleShared,

    ServerModule,
    ServerPrebootModule.recordEvents({ appRoot: 'app-root' }),
    NoopAnimationsModule,

    // HttpTransferCacheModule still needs fixes for 5.0
    //   Leave this commented out for now, as it breaks Server-renders
    //   Looking into fixes for this! - @MarkPieszak
    // ServerTransferStateModule // <-- broken for the time-being with ASP.NET
  ]
})
export class AppModule {
  constructor() { }
}";
        }
    }
}
