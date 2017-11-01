using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Request
{
    public class Screen
    {
        public string InternalName { get; set; }
        public string ControllerCode { get; set; }
        public string Title { get; set; }
        public Guid Id { get; set; }
        public Guid? EntityId { get; set; }
        public Guid ScreenTypeId { get; set; }
        public string Path { get; set; }
        public string Html { get; set; }
        public string TypeScript { get; set; }
        public Guid? TemplateId { get; set; }
        public string Css { get; set; }
        public IEnumerable<ScreenFeature> ScreenFeatures { get; set; }

        internal ScreenTypeEnum ScreenType
        {
            get
            {
                var screenTypes = new Dictionary<Guid, ScreenTypeEnum>
                {
                    { new Guid("{03CD1D4E-CA2B-4466-8016-D96C2DABEB0D}"), ScreenTypeEnum.Search },
                    { new Guid("{9B422DE1-FACE-4A63-9A46-0BD1AF3D47F4}"), ScreenTypeEnum.Edit },
                    { new Guid("{ACE5A965-7005-4E34-9C66-AF0F0CD15AE9}"), ScreenTypeEnum.View },
                    { new Guid("{7A37305E-C518-4A16-91AE-BCF2AE032A9C}"), ScreenTypeEnum.Html }
                };
                
                return screenTypes.GetValueOrDefault(ScreenTypeId, ScreenTypeEnum.Search);
            }
        }

        internal bool HasControllerCode
        {
            get
            {
                return !string.IsNullOrWhiteSpace(ControllerCode);
            }
        }
    }
}
