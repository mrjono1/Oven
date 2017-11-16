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

        internal ScreenSectionTypeEnum ScreenSectionType
        {
            get
            {
                var types = new Dictionary<Guid, ScreenSectionTypeEnum>
                {
                    { new Guid("{0637300C-B76E-45E2-926A-055BB335129F}"), ScreenSectionTypeEnum.Search },
                    { new Guid("{DC1169A8-8F49-45E9-9969-B64BEF4D0F42}"), ScreenSectionTypeEnum.Form },
                    { new Guid("{4270A420-64CB-4A2C-B718-2C645DB2B57B}"), ScreenSectionTypeEnum.Grid },
                    { new Guid("{38EF9B44-A993-479B-91EC-1FE436E91556}"), ScreenSectionTypeEnum.Html }
                };

                return types.GetValueOrDefault(ScreenSectionTypeId, ScreenSectionTypeEnum.Search);
            }
        }
        public Guid? EntityId { get; set; }
        public Guid? NavigateToScreenId { get; set; }
        public MenuItem[] MenuItems { get; internal set; }
    }
}
