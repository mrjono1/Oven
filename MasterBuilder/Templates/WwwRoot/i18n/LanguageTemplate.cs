using MasterBuilder.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.WwwRoot.i18n
{
    public class LanguageTemplate : ITemplate
    {
        public string GetFileName()
        {
            return "en.json";
        }

        public string[] GetFilePath()
        {
            return new string[] { "wwwroot", "assets", "i18n" };
        }

        public string GetFileContent()
        {
            return @"{
    ""HOME"":""Home"",
    ""ENGLISH"":""English""
}";
        }
    }
}
