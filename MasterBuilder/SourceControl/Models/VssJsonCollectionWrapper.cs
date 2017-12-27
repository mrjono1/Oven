using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.SourceControl.Models
{
    public class VssJsonCollectionWrapper
    {
        public VssJsonCollectionWrapper() { }
        public GetRepository[] Value { get; set; }
        public int Count { get; set; }
    }
}
