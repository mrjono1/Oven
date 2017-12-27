using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.SourceControl.Models
{
    public class CreateRepository
    {
        public string Name { get; set; }
        public RepoProject Project { get; set; }

        public class RepoProject
        {
            public Guid Id { get; set; }
        }
    }
}
