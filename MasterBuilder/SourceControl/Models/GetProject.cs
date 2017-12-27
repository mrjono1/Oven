using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.SourceControl.Models
{
    public class GetProject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string State { get; set; }
        public string Status { get; set; }
    }
}
