using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Migrations
{
    internal class CommandLineResult
    {
        public int ExitCode { get; set; } 
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
