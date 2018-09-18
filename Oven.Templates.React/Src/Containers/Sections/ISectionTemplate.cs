using System.Collections.Generic;

namespace Oven.Templates.React.Src.Containers.Sections
{
    interface ISectionTemplate
    {
        IEnumerable<string> Constructor();
        IEnumerable<string> Imports();
        IEnumerable<string> Methods();
        IEnumerable<string> Functions();
    }
}