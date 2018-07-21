using System.Collections.Generic;

namespace MasterBuilder.Templates.React.Src.Containers.Sections
{
    interface ISectionTemplate
    {
        IEnumerable<string> Constructor();
        IEnumerable<string> Imports();
        IEnumerable<string> Methods();
    }
}