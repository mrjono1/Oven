using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.App.Models
{
    public class ModelTemplate : ITemplate
    {
        private readonly Project Project;

        private readonly Screen Screen;

        private readonly ScreenSection ScreenSection;

        public ModelTemplate(Project project, Screen screen, ScreenSection screenSection)
        {
            Project = project;
            Screen = screen;
            ScreenSection = screenSection;
        }
        public string GetFileContent()
        {
            throw new NotImplementedException();
        }

        public string GetFileName()
        {
            throw new NotImplementedException();
        }

        public string[] GetFilePath()
        {
            throw new NotImplementedException();
        }
    }
}
