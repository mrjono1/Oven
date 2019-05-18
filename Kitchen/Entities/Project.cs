using MongoDB.Bson;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kitchen.Entities
{
    public class Project
    {
        public static Entity GetEntity()
        {
            return new Entity()
            {
                Id = new ObjectId("5ca869596668b25914b67e6e"),
                InternalName = "Project",
                Title = "Project",
                Properties = new Property[]
                {
                    new Property("Id")
                    {
                        Id = new ObjectId("5ca876ed4a73264e4c06dedc"),
                        PropertyType = PropertyType.PrimaryKey,
                        Title = "Id"
                    },
                    new Property("Title")
                    {
                        Id = new ObjectId("5ca876ee4a73264e4c06dedd"),
                        PropertyType = PropertyType.String,
                        Title = "Title",
                        ValidationItems = new Validation[]
                        {
                            new Validation
                            {
                                Id = new ObjectId("5ca876ee4a73264e4c06dede"),
                                ValidationType = ValidationType.Unique
                            },
                            new Validation
                            {
                                Id = new ObjectId("5ca876f04a73264e4c06dedf"),
                                ValidationType = ValidationType.MaximumLength,
                                IntegerValue = 100
                            },
                            new Validation
                            {
                                Id = new ObjectId("5ca876f04a73264e4c06dee0"),
                                ValidationType = ValidationType.Required
                            }
                        }
                    },
                    new Property("InternalName")
                    {
                        Id = new ObjectId("5ca876f04a73264e4c06dee1"),
                        PropertyType = PropertyType.String,
                        Title = "Internal Name",
                        ValidationItems = new Validation[]
                        {
                            new Validation
                            {
                                Id = new ObjectId("5ca876f04a73264e4c06dee2"),
                                ValidationType = ValidationType.Unique
                            },
                            new Validation
                            {
                                Id = new ObjectId("5ca876f04a73264e4c06dee3"),
                                ValidationType = ValidationType.MaximumLength,
                                IntegerValue = 100
                            },
                            new Validation
                            {
                                Id = new ObjectId("5ca876f04a73264e4c06dee4"),
                                ValidationType = ValidationType.Required
                            },
                            new Validation
                            {
                                Id = new ObjectId("5ca876f04a73264e4c06dee5"),
                                ValidationType = ValidationType.Pattern,
                                StringValue = "^[a-zA-Z]+$"
                            }
                        }
                    },
                    new Property("MajorVersion")
                    {
                        Id = new ObjectId("5ca876f14a73264e4c06dee6"),
                        PropertyType = PropertyType.Integer,
                        Title = "Major Version",
                        ValidationItems = new Validation[]
                        {
                            new Validation
                            {
                                Id = new ObjectId("5ca876f14a73264e4c06dee7"),
                                ValidationType = ValidationType.Required
                            }
                        },
                        DefaultIntegerValue = 1
                    },
                    new Property("MinorVersion")
                    {
                        Id = new ObjectId("5ca876f14a73264e4c06dee8"),
                        PropertyType = PropertyType.Integer,
                        Title = "Minor Version",
                        ValidationItems = new Validation[]
                        {
                            new Validation
                            {
                                Id = new ObjectId("5ca876f14a73264e4c06dee9"),
                                ValidationType = ValidationType.Required
                            }
                        },
                        DefaultIntegerValue = 0
                    },
                    new Property("BuildVersion")
                    {
                        Id = new ObjectId("5ca876f14a73264e4c06deea"),
                        PropertyType = PropertyType.Integer,
                        Title = "Build Version",
                        DefaultIntegerValue = 0,
                        ValidationItems = new Validation[]
                        {
                            new Validation
                            {
                                Id = new ObjectId("5ca876f14a73264e4c06deeb"),
                                ValidationType = ValidationType.Required
                            }
                        }
                    },
                    new Property("CreatedDate")
                    {
                        Id = new ObjectId("5ca876f14a73264e4c06deec"),
                        PropertyType = PropertyType.DateTime,
                        Title = "Created Date",
                        ValidationItems = new Validation[]
                        {
                            new Validation
                            {
                                Id = new ObjectId("5ca876f14a73264e4c06deed"),
                                ValidationType = ValidationType.Required
                            }
                        },
                        DefaultDateTimeType = DefaultDateTimeType.Now 
                    },
                    new Property("DefaultScreen")
                    {
                        Id = new ObjectId("5ca876f14a73264e4c06deee"),
                        PropertyType = PropertyType.ReferenceRelationship,
                        Title = "Default Screen",
                        ReferenceEntityId = new ObjectId("5ca876f14a73264e4c06deef"),
                        FilterExpression = new Expression
                        {
                            PropertyId = new ObjectId("5ca876ed4a73264e4c06dedc"),
                            Operator = Oven.Request.Enumerations.ExpressionOperator.Equal,
                            ReferencePropertyId = new ObjectId("5ca876f14a73264e4c06def0")
                        }
                    },
                    new Property("EnableCustomCode")
                    {
                        Id = new ObjectId("5ca876f24a73264e4c06def1"),
                        PropertyType = PropertyType.Boolean,
                        Title = "Enable Custom Code",
                        ValidationItems = new Validation[]
                        {
                            new Validation
                            {
                                Id = new ObjectId("5ca876f24a73264e4c06def2"),
                                ValidationType = ValidationType.Required
                            }
                        }
                    }
                }
            };
        }
    }
}
