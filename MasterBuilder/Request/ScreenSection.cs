using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Request
{
    public class ScreenSection
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string InternalName { get; set; }
        public Guid ScreenSectionTypeId { get; set; }
        public Guid? EntityId { get; set; }
    }
}
