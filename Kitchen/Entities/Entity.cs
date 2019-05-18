using MongoDB.Bson;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kitchen.Entities
{
    public class Entity
    {
        public static Oven.Request.Entity GetEntity()
        {
            return new Oven.Request.Entity("Entity")
            {
                Id = new ObjectId("5ca876f24a73264e4c06def3"),
                Title = "Entity",
                Properties = new Property[]
                {
                    new Property("Id")
                    {
                        Id = new ObjectId("5ca876f24a73264e4c06def4"),
                        PropertyType = PropertyType.PrimaryKey,
                        Title = "Id"
                    },
                    new Property()
                    {
                        Id = new ObjectId("5ca876f24a73264e4c06def5"),
                        InternalName = "Title",
                        PropertyType = PropertyType.String,
                        Title = "Title",
                        ValidationItems = new Validation[]
                        {
                            new Validation
                            {
                                Id = new ObjectId("5ca876f04a73264e4c06dedf"),
                                ValidationType = ValidationType.MaximumLength,
                                IntegerValue = 100
                            },
                            new Validation{
                                Id = new ObjectId("5ca876f04a73264e4c06dee0"),
                                ValidationType = ValidationType.Required
                            },
                        }
                    },
                    new Property()
                    {
                        Id = new ObjectId("5ca876f24a73264e4c06def6"),
                        InternalName = "InternalName",
                        PropertyType = PropertyType.String,
                        Title = "Internal Name",
                        ValidationItems = new Validation[]
                        {
                            new Validation
                            {
                                Id = new ObjectId("5ca876f04a73264e4c06dee3"),
                                ValidationType = ValidationType.MaximumLength,
                                IntegerValue = 100
                            },
                            new Validation{
                                Id = new ObjectId("5ca876f04a73264e4c06dee4"),
                                ValidationType = ValidationType.Required
                            },
                            new Validation{
                                ValidationType = ValidationType.Pattern,
                                StringValue = "^[a-zA-Z]+$"
                            }
                        }
                    },
                    new Property()
                    {
                        Id = new ObjectId("5ca876f24a73264e4c06def7"),
                        InternalName = "EntityTemplate",
                        PropertyType = PropertyType.ReferenceRelationship,
                        Title = "Entity Template",
                        ReferenceEntityId = new ObjectId("5ca876f24a73264e4c06def8")
                    },
                    new Property("Project")
                    {
                        Id = new ObjectId("5ca876f24a73264e4c06def9"),
                        PropertyType = PropertyType.ParentRelationshipOneToMany,
                        Title = "Project",
                        ReferenceEntityId = new ObjectId("5ca869596668b25914b67e6e"),
                        ValidationItems = new Validation[]
                        {
                            new Validation{
                                Id = new ObjectId("5ca876f04a73264e4c06dee0"),
                                ValidationType = ValidationType.Required
                            },
                        }
                    },
                    new Property()
                    {
                        Id = new ObjectId("5ca876f24a73264e4c06defa"),
                        InternalName = "HasSeed",
                        PropertyType = PropertyType.Boolean,
                        Title = "Has Seed Data",
                        DefaultBooleanValue = false,
                        ValidationItems = new Validation[]
                        {
                            new Validation
                            {
                                Id = new ObjectId("5ca876f24a73264e4c06defb"),
                                ValidationType = ValidationType.Required
                            }
                        }
                    }
                }
            };
        }
    }
}