using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Request
{
    public class Project
    {

        public Project()
        {
            CleanDirectory = true;
            CleanDirectoryIgnoreDirectories = new string[]
            {
                "bin",
                "obj",
                "node_modules"
            };
        }

        public bool CleanDirectory { get; set; }
        public Guid Id { get; set; }

        public string InternalName { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }

        public IEnumerable<Entity> Entities { get; set; }
        public IEnumerable<Screen> Screens { get; set; }

        public string[] CleanDirectoryIgnoreDirectories { get; set; }
    }
}
