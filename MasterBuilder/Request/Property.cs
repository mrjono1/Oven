using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Request
{
    public class Property
    {
        public string InternalName { get; set; }

        public string Type { get; set; }
        internal bool HasCalculation
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Calculation);
            }
        }
        public string Calculation { get; set; }
    }
}
