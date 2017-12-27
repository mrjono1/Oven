using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.SourceControl.Models
{
    public class GetRepository
    {
        public GetRepository() { }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public GetProject Project { get; set; }
        public string DefaultBranch { get; set; }
        public string RemoteUrl { get; set; }
    }
}
