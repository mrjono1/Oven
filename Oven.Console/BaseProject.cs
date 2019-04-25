using MongoDB.Bson;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oven.ConsoleApp
{
    public class BaseProject
    {
        public IEnumerable<Entity> Entities
        {
            get
            {
                return new Entity[]
                {
                    new Entity("Role")
                    {
                        Id = new ObjectId("5ca87499e0927b2a006fb431"),
                        Title = "Role",
                        Properties = new Property[]
                        {
                            new Property
                            {
                                Id = new ObjectId("5ca876dc4a73264e4c06decd"),
                                Title = "Title",
                                InternalName = "Title",
                                PropertyType = PropertyType.String,
                                ValidationItems = new Validation[]
                                {
                                    new Validation
                                    {
                                        ValidationType = ValidationType.Required
                                    }
                                }
                            }
                        }
                    },
                    new Entity("GroupRole")
                    {
                        Id = new ObjectId("5ca876e64a73264e4c06dece"),
                        Title = "Group Role",
                        Properties = new Property[]
                        {
                            new Property
                            {
                                Id = new ObjectId("5ca876e74a73264e4c06decf"),
                                Title = "Role",
                                InternalName = "Role",
                                PropertyType = PropertyType.ParentRelationshipOneToMany,
                                ReferenceEntityId = new ObjectId("5ca87499e0927b2a006fb431"),
                                ValidationItems = new Validation[]
                                {
                                    new Validation
                                    {
                                        ValidationType = ValidationType.Required
                                    }
                                }
                            },
                            new Property
                            {
                                Id = new ObjectId("5ca876e84a73264e4c06ded0"),
                                Title = "Group",
                                InternalName = "Group",
                                PropertyType = PropertyType.ParentRelationshipOneToMany,
                                ReferenceEntityId = new ObjectId("5ca876e84a73264e4c06ded1"),
                                ValidationItems = new Validation[]
                                {
                                    new Validation
                                    {
                                        ValidationType = ValidationType.Required
                                    }
                                }
                            }
                        }
                    },
                    new Entity("Group")
                    {
                        Id = new ObjectId("5ca876e84a73264e4c06ded1"),
                        Title = "Group",
                        Properties = new Property[]
                        {
                            new Property
                            {
                                Id = new ObjectId("5ca876eb4a73264e4c06ded2"),
                                Title = "Title",
                                InternalName = "Title",
                                PropertyType = PropertyType.String,
                                ValidationItems = new Validation[]
                                {
                                    new Validation
                                    {
                                        ValidationType = ValidationType.Required
                                    }
                                }
                            }
                        }
                    },
                    new Entity("UserGroup")
                    {
                        Id = new ObjectId("5ca876eb4a73264e4c06ded3"),
                        Title = "User Group",
                        Properties = new Property[]
                        {
                            new Property
                            {
                                Id = new ObjectId("5ca876e84a73264e4c06ded0"),
                                Title = "Group",
                                InternalName = "Group",
                                PropertyType = PropertyType.ParentRelationshipOneToMany,
                                ReferenceEntityId = new ObjectId("5ca876e84a73264e4c06ded1"),
                                ValidationItems = new Validation[]
                                {
                                    new Validation
                                    {
                                        ValidationType = ValidationType.Required
                                    }
                                }
                            },
                            new Property
                            {
                                Id = new ObjectId("5ca876ec4a73264e4c06ded4"),
                                Title = "User",
                                InternalName = "User",
                                PropertyType = PropertyType.ParentRelationshipOneToMany,
                                ReferenceEntityId = new ObjectId("5ca876ec4a73264e4c06ded5"),
                                ValidationItems = new Validation[]
                                {
                                    new Validation
                                    {
                                        ValidationType = ValidationType.Required
                                    }
                                }
                            }
                        }
                    },
                    new Entity("User")
                    {
                        Id = new ObjectId("5ca876ec4a73264e4c06ded5"),
                        Title = "User",
                        InternalName = "User",
                        Properties = new Property[]
                        {
                            new Property
                            {
                                Id = new ObjectId("5ca876ec4a73264e4c06ded6"),
                                Title = "User Name",
                                InternalName = "UserName",
                                PropertyType = PropertyType.String,
                                ValidationItems = new Validation[]
                                {
                                    new Validation
                                    {
                                        ValidationType = ValidationType.Required
                                    }
                                }
                            }
                        }
                    },
                    new Entity("Log")
                    {
                        Id = new ObjectId("5ca876ed4a73264e4c06ded7"),
                        Title = "Log",
                        Properties = new Property[]
                        {
                            new Property
                            {
                                Id = new ObjectId("5ca876ed4a73264e4c06ded8"),
                                Title = "Title",
                                InternalName = "Title",
                                PropertyType = PropertyType.String
                            }
                        }
                    },
                    new Entity("Audit")
                    {
                        Id = new ObjectId("5ca876ed4a73264e4c06ded9"),
                        Title = "Audit",
                        Properties = new Property[]
                        {
                            new Property("Id")
                            {
                                Id = new ObjectId("5ca876ed4a73264e4c06deda"),
                                Title = "Id",
                                PropertyType = PropertyType.PrimaryKey
                            }
                        }
                    }
                };
            }
        }
    }
}
