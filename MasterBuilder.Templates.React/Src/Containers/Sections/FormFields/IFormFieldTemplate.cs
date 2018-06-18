using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.React.Src.Containers.Sections.FormFields
{
    interface IFormFieldTemplate
    {
        string Elements { get; }
        IEnumerable<string> Imports { get; }
    }
}
