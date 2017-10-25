using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Request
{
    public class Screen
    {
        public string InternalName { get; set; }
        public string ControllerCode { get; set; }
        public Guid Id { get; internal set; }

        internal bool HasControllerCode
        {
            get
            {
                return !string.IsNullOrWhiteSpace(ControllerCode);
            }
        }
    }
}
