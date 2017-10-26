using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Request
{
    public class Validation
    {
        public Guid Id { get; set; }
        public ValidationTypeEnum ValidationType { get; set; }
        public int? IntegerValue { get; set; }
        public string Message { get; set; }
    }
}
