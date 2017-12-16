using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.App.Shared
{
    public class DataServiceTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Entity Entity;
        private readonly Screen Screen;

        public DataServiceTemplate(Project project, Entity entity, Request.Screen screen)
        {
            Project = project;
            Entity = entity;
            Screen = screen;
        }

        public string GetFileName()
        {
            var internalName = (Entity != null ? Entity.InternalName : Screen.InternalName);
            
            return $"{internalName.ToCamlCase()}.service.ts";
        }

        public string[] GetFilePath()
        {
            return new string[] { "ClientApp", "app", "shared"};
        }

        public string GetFileContent()
        {
            return $@"import {{ Injectable, Inject, Injector }} from '@angular/core';
import {{ HttpClient }} from '@angular/common/http';
import {{ Http, URLSearchParams }} from '@angular/http';
import {{ APP_BASE_HREF }} from '@angular/common';
import {{ ORIGIN_URL }} from '@nguniversal/aspnetcore-engine';
import {{ IUser }} from '../models/User';
import {{ Observable }} from 'rxjs/Observable';


@Injectable()
export class UserService {{

  private baseUrl: string;

  constructor(
    private http: HttpClient,
    private injector: Injector
  ) {{
    this.baseUrl = this.injector.get(ORIGIN_URL);
  }}

}}
";   
        }
    }
}
