using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.App.Models
{
    public class OperationTemplate : ITemplate
    {
        public string GetFileName()
        {
            return "Operation.ts";
        }

        public string[] GetFilePath()
        {
            return new string[] { "ClientApp", "app", "models" };
        }
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
