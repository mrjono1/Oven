using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Request
{
    public class ScreenFeature
    {
        public Guid Id { get; set; }
        public Guid FeatureId { get; set; }


        internal FeatureEnum ScreenType
        {
            get
            {
                var options = new Dictionary<Guid, FeatureEnum>
                {
                    { new Guid("{6114120E-BD93-4CE4-A673-7DC295F93CFE}"), FeatureEnum.New }
                };

                return options.GetValueOrDefault(FeatureId, FeatureEnum.Undefined);
            }
        }
    }
}
