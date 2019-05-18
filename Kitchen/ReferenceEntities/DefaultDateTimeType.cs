using MongoDB.Bson;
using Newtonsoft.Json;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Kitchen.ReferenceEntities
{
    public class DefaultDateTimeType
    {
        public static Entity GetEntity()
        {
            return new Entity("DefaultDateTimeType")
            {
                Id = new ObjectId("5cdf46ec008e488714f7cbe3"),
                Title = "Default Date Time Type",
                EntityTemplate = EntityTemplate.Reference,
                Properties = new Property[]
                {
                    new Property()
                    {
                        Id = new ObjectId("5cdf46f30b48bb0d262c170c"),
                        InternalName = "Id",
                        PropertyType = PropertyType.PrimaryKey,
                        Title = "Id"
                    },
                    new Property()
                    {
                        Id = new ObjectId("5cdf46fb326c22cbaf89b1da"),
                        InternalName = "Title",
                        PropertyType = PropertyType.String,
                        Title = "Title",
                        PropertyTemplate = PropertyTemplate.ReferenceTitle
                    }
                },
                Seed = new Seed
                {
                    SeedType = SeedType.EnsureAllUpdated,
                    JsonData = JsonConvert.SerializeObject(new[]
                    {
                        new {
                            Id = Property.DefaultDateTimeTypeDictonary.Single(a => a.Value == Oven.Request.DefaultDateTimeType.Now).Key,
                            Title = "Now"
                        },
                        new {
                            Id = Property.DefaultDateTimeTypeDictonary.Single(a => a.Value == Oven.Request.DefaultDateTimeType.SpecifiedValue).Key,
                            Title = "Specify Value"
                        }
                    })
                }
            };
            
        }
    }
}
