using Oven.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace Oven
{
    public class KitchenModel
    {
        public Project Project { get; set; }
        public KitchenModel()
        {
            var project = new Project
            {
                Id = Project.KitchenId,
                InternalName = "Kitchen",
                MajorVersion = 0,
                MinorVersion = 3,
                BuildVersion = 0,
                Title = "Oven - Kitchen",
                DefaultScreenId = new ObjectId("5ca876ed4a73264e4c06dedb"),
                EnableCustomCode = true
            };

            project.Entities = new Entity[]
            {
                Kitchen.Entities.Project.GetEntity(),
                #region Entity Entity
                new Entity("Entity")
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
                },
                #endregion
                #region Property Entity
                new Entity()
                {
                    Id = new ObjectId("5ca876f34a73264e4c06defc"),
                    InternalName = "Property",
                    Title = "Property",
                    Properties = new Property[]
                    {
                        new Property("Id")
                        {
                            Id = new ObjectId("5ca876f34a73264e4c06defd"),
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property("Entity")
                        {
                            Id = new ObjectId("5ca876f34a73264e4c06defe"),
                            PropertyType = PropertyType.ParentRelationshipOneToMany,
                            Title = "Entity",
                            ReferenceEntityId = new ObjectId("5ca876f24a73264e4c06def3"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new ObjectId("5ca876f34a73264e4c06deff"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property("Title")
                        {
                            Id = new ObjectId("5ca876f34a73264e4c06df00"),
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca876f34a73264e4c06df01"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new ObjectId("5ca876f34a73264e4c06df02"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property("InternalName")
                        {
                            Id = new ObjectId("5ca876f34a73264e4c06df03"),
                            PropertyType = PropertyType.String,
                            Title = "Internal Name",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca876f34a73264e4c06df04"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new ObjectId("5ca876f34a73264e4c06df05"),
                                    ValidationType = ValidationType.Required
                                },
                                new Validation{
                                    ValidationType = ValidationType.Pattern,
                                    StringValue = "^[a-zA-Z]+$"
                                }
                            }
                        },
                        new Property("PropertyType")
                        {
                            Id = new ObjectId("5ca876f34a73264e4c06df06"),
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Property Type",
                            ReferenceEntityId = new ObjectId("5ca876f34a73264e4c06df07"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new ObjectId("5ca876f44a73264e4c06df08"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property("PropertyTemplate")
                        {
                            Id = new ObjectId("5ca876f44a73264e4c06df09"),
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Property Template",
                            ReferenceEntityId = new ObjectId("5ca876f44a73264e4c06df0a")
                        },
                        new Property("ParentEntity")
                        {
                            Id = new ObjectId("5ca876f44a73264e4c06df0b"),
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "ParentEntity",
                            // Entity
                            ReferenceEntityId = new ObjectId("5ca876f24a73264e4c06def3"),
                            FilterExpression = new Expression
                            {
                                // ProjectId
                                PropertyId = new ObjectId("5ca876f14a73264e4c06def0"),
                                Operator = Request.Enumerations.ExpressionOperator.Equal,
                                // Entity.ProjectId
                                ReferencePropertyId = new ObjectId("5ca876f24a73264e4c06def9")
                            }
                        },
                        new Property("DefaultIntegerValue")
                        {
                            Id = new ObjectId("5ca876f44a73264e4c06df0c"),
                            PropertyType = PropertyType.Integer,
                            Title = "Default Value"
                        },
                        new Property("DefaultDoubleValue")
                        {
                            Id = new ObjectId("5ca876f44a73264e4c06df0d"),
                            PropertyType = PropertyType.Double,
                            Title = "Default Value"
                        },
                        new Property("DefaultStringValue")
                        {
                            Id = new ObjectId("5ca876f44a73264e4c06df0e"),
                            PropertyType = PropertyType.String,
                            Title = "Default Value"
                        },
                        new Property("DefaultBooleanValue")
                        {
                            Id = new ObjectId("5ca876f44a73264e4c06df0f"),
                            PropertyType = PropertyType.Boolean,
                            Title = "Default Value",
                            DefaultBooleanValue = false
                        }
                    }
                },
                #endregion
                #region Validation Entity
                new Entity()
                {
                    Id = new ObjectId("5ca876f44a73264e4c06df10"),
                    InternalName = "Validation",
                    Title = "Validation",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca876f44a73264e4c06df11"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876f44a73264e4c06df12"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca876f54a73264e4c06df13"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new ObjectId("5ca876f54a73264e4c06df14"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876f54a73264e4c06df15"),
                            InternalName = "Property",
                            PropertyType = PropertyType.ParentRelationshipOneToMany,
                            Title = "Property",
                            ReferenceEntityId = new ObjectId("5ca876f34a73264e4c06defc"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new ObjectId("5ca876f54a73264e4c06df16"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876f54a73264e4c06df17"),
                            InternalName = "ValidationType",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Validation Type",
                            ReferenceEntityId = new ObjectId("5ca876f54a73264e4c06df18"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new ObjectId("5ca876f54a73264e4c06df19"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876f54a73264e4c06df1a"),
                            InternalName = "IntegerValue",
                            PropertyType = PropertyType.Integer,
                            Title = "Integer Value"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876f54a73264e4c06df1b"),
                            InternalName = "DoubleValue",
                            PropertyType = PropertyType.Double,
                            Title = "Double Value"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876f54a73264e4c06df1c"),
                            InternalName = "StringValue",
                            PropertyType = PropertyType.String,
                            Title = "String Value"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876f64a73264e4c06df1d"),
                            InternalName = "Message",
                            PropertyType = PropertyType.String,
                            Title = "Message"
                        },
                    }
                },
                #endregion

                new Entity()
                {
                    Id = new ObjectId("5ca876f64a73264e4c06df1e"),
                    InternalName = "MenuItem",
                    Title = "Menu Item",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca876f64a73264e4c06df1f"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876f64a73264e4c06df20"),
                            InternalName = "Project",
                            PropertyType = PropertyType.ParentRelationshipOneToMany,
                            Title = "Project",
                            ReferenceEntityId = new ObjectId("5ca869596668b25914b67e6e"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new ObjectId("5ca876f64a73264e4c06df21"),
                                    ValidationType = ValidationType.Required
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876f64a73264e4c06df22"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca876f64a73264e4c06df23"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new ObjectId("5ca876f64a73264e4c06df24"),
                                    ValidationType = ValidationType.Required
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876f64a73264e4c06df25"),
                            InternalName = "MenuItemType",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Type",
                            ReferenceEntityId = new ObjectId("5ca876f64a73264e4c06df26")
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876f64a73264e4c06df27"),
                            InternalName = "Screen",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Screen",
                            ReferenceEntityId = new ObjectId("5ca876f14a73264e4c06deef")
                        },
                    }
                },
                #region Screen Entity
                new Entity("Screen")
                {
                    Id = new ObjectId("5ca876f14a73264e4c06deef"),
                    Title = "Screen",
                    Properties = new Property[]
                    {
                        new Property("Id")
                        {
                            Id = new ObjectId("5ca876f74a73264e4c06df28"),
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property("Project")
                        {
                            Id = new ObjectId("5ca876f14a73264e4c06def0"),
                            PropertyType = PropertyType.ParentRelationshipOneToMany,
                            Title = "Project",
                            ReferenceEntityId = new ObjectId("5ca869596668b25914b67e6e"),
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca876f74a73264e4c06df29"),
                                    ValidationType = ValidationType.Required
                                }
                            }
                        },
                        new Property("Title")
                        {
                            Id = new ObjectId("5ca876f74a73264e4c06df2a"),
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca876f74a73264e4c06df2b"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new ObjectId("5ca876f74a73264e4c06df2c"),
                                    ValidationType = ValidationType.Required
                                }
                            }
                        },
                        new Property("Path")
                        {
                            Id = new ObjectId("5ca876f74a73264e4c06df2d"),
                            PropertyType = PropertyType.String,
                            Title = "Path/Slug"
                        },
                        new Property("Entity")
                        {
                            Id = new ObjectId("5ca876f74a73264e4c06df2e"),
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Entity",
                            // Entity
                            ReferenceEntityId = new ObjectId("5ca876f24a73264e4c06def3"),
                            FilterExpression = new Expression
                            {
                                // ProjectId
                                PropertyId = new ObjectId("5ca876f14a73264e4c06def0"),
                                Operator = Request.Enumerations.ExpressionOperator.Equal,
                                // Entity.ProjectId
                                ReferencePropertyId = new ObjectId("5ca876f24a73264e4c06def9")
                            }
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876f74a73264e4c06df2f"),
                            InternalName = "Template",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Template",
                            ReferenceEntityId = new ObjectId("5ca876f74a73264e4c06df30")
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876f74a73264e4c06df31"),
                            InternalName = "ScreenType",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Screen Type",
                            ReferenceEntityId = new ObjectId("5ca876f74a73264e4c06df32")
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876f84a73264e4c06df33"),
                            InternalName = "HasDefaultObject",
                            PropertyType = PropertyType.Boolean,
                            Title = "Has Default Object",
                            DefaultBooleanValue = false,
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca876f84a73264e4c06df34"),
                                    ValidationType = ValidationType.Required
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876f84a73264e4c06df35"),
                            InternalName = "DefaultObjectJsonData",
                            PropertyType = PropertyType.String,
                            Title = "Default Object Json Data",
                            ValidationItems = new Validation[]
                            {
                                //new Validation{
                                //    Id = new ObjectId("5ca876f84a73264e4c06df36"),
                                //    ValidationType = ValidationType.RequiredExpression
                                //}
                            }
                        }
                    }
                },
#endregion
                new Entity()
                {
                    Id = new ObjectId("5ca876f84a73264e4c06df37"),
                    InternalName = "ScreenFeature",
                    Title = "Screen Feature",
                    Properties = new Property[]
                    {
                        new Property("Id")
                        {
                            Id = new ObjectId("5ca876f84a73264e4c06df38"),
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                    }
                },
                #region Screen Section Entity
                new Entity()
                {
                    Id = new ObjectId("5ca876f84a73264e4c06df39"),
                    InternalName = "ScreenSection",
                    Title = "Screen Section",
                    Properties = new Property[]
                    {
                        new Property("Id")
                        {
                            Id = new ObjectId("5ca876f84a73264e4c06df3a"),
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property("Title")
                        {
                            Id = new ObjectId("5ca876f84a73264e4c06df3b"),
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca876f84a73264e4c06df3c"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new ObjectId("5ca876f84a73264e4c06df3d"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property("InternalName")
                        {
                            Id = new ObjectId("5ca876f84a73264e4c06df3e"),
                            PropertyType = PropertyType.String,
                            Title = "Internal Name",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca876f94a73264e4c06df3f"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new ObjectId("5ca876f94a73264e4c06df40"),
                                    ValidationType = ValidationType.Required
                                },
                                new Validation{
                                    ValidationType = ValidationType.Pattern,
                                    StringValue = "^[a-zA-Z]+$"
                                }
                            }
                        },
                        new Property("Screen")
                        {
                            Id = new ObjectId("5ca876f94a73264e4c06df41"),
                            PropertyType = PropertyType.ParentRelationshipOneToMany,
                            Title = "Screen",
                            ReferenceEntityId = new ObjectId("5ca876f14a73264e4c06deef"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new ObjectId("5ca876f94a73264e4c06df42"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property("NavigateToScreen")
                        {
                            Id = new ObjectId("5ca876f94a73264e4c06df43"),
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Navigate to screen",
                            ReferenceEntityId = new ObjectId("5ca876f14a73264e4c06deef"),
                            FilterExpression = new Expression
                            {
                                // Screen
                                EntityId = new ObjectId("5ca876f14a73264e4c06deef"),
                                // Screen.ProjectId
                                PropertyId = new ObjectId("5ca876f14a73264e4c06def0"),
                                Operator = Request.Enumerations.ExpressionOperator.Equal,
                                // Screen.ProjectId
                                ReferencePropertyId = new ObjectId("5ca876f14a73264e4c06def0")
                            },
                        },
                        new Property("ScreenSectionType")
                        {
                            Id = new ObjectId("5ca876f94a73264e4c06df44"),
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Screen Section Type",
                            ReferenceEntityId = new ObjectId("5ca876f94a73264e4c06df45"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new ObjectId("5ca876f94a73264e4c06df46"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property("Entity")
                        {
                            Id = new ObjectId("5ca876f94a73264e4c06df47"),
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Entity",
                            // Entity
                            ReferenceEntityId = new ObjectId("5ca876f24a73264e4c06def3"),
                            FilterExpression = new Expression
                            {
                                // Screen
                                EntityId = new ObjectId("5ca876f14a73264e4c06deef"),
                                // Screen.ProjectId
                                PropertyId = new ObjectId("5ca876f14a73264e4c06def0"),
                                Operator = Request.Enumerations.ExpressionOperator.Equal,
                                // Entity.ProjectId
                                ReferencePropertyId = new ObjectId("5ca876f24a73264e4c06def9")
                            },
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca876f94a73264e4c06df48"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property("Html")
                        {
                            Id = new ObjectId("5ca876fa4a73264e4c06df49"),
                            PropertyType = PropertyType.String,
                            Title = "Html"
                        }
                    }
                },
                #endregion
                #region Seed Entity
                new Entity()
                {
                    Id = new ObjectId("5ca876fa4a73264e4c06df4a"),
                    InternalName = "Seed",
                    Title = "Seed",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca876fa4a73264e4c06df4b"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876fa4a73264e4c06df4c"),
                            InternalName = "Entity",
                            PropertyType = PropertyType.ParentRelationshipOneToOne,
                            Title = "Entity",
                            ReferenceEntityId = new ObjectId("5ca876f24a73264e4c06def3")
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876fa4a73264e4c06df4d"),
                            InternalName = "JsonData",
                            PropertyType = PropertyType.String,
                            Title = "Json Data",
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new ObjectId("5ca876fa4a73264e4c06df4e"),
                                    ValidationType = ValidationType.Required
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876fa4a73264e4c06df4f"),
                            InternalName = "SeedType",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Seed Type",
                            ReferenceEntityId = new ObjectId("5ca876fa4a73264e4c06df50"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new ObjectId("5ca876fa4a73264e4c06df51"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        }
                    }
                },
                #endregion
                #region Service Entity
                new Entity()
                {
                    Id = new ObjectId("5ca876fa4a73264e4c06df52"),
                    InternalName = "Service",
                    Title = "Service",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca876fa4a73264e4c06df53"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876fa4a73264e4c06df54"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca876fb4a73264e4c06df55"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new ObjectId("5ca876fb4a73264e4c06df56"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876fb4a73264e4c06df57"),
                            InternalName = "ServiceType",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Service Type",
                            ReferenceEntityId = new ObjectId("5ca876fb4a73264e4c06df58"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876fb4a73264e4c06df59"),
                            InternalName = "Project",
                            PropertyType = PropertyType.ParentRelationshipOneToMany,
                            Title = "Project",
                            ReferenceEntityId = new ObjectId("5ca869596668b25914b67e6e"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new ObjectId("5ca876fb4a73264e4c06df5a"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        }
                    }
                },
#endregion
                #region Export Service Entity
                new Entity()
                {
                    Id = new ObjectId("5ca876fb4a73264e4c06df5b"),
                    InternalName = "ExportService",
                    Title = "Export Service",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca876fb4a73264e4c06df5c"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876fb4a73264e4c06df5d"),
                            InternalName = "Service",
                            PropertyType = PropertyType.ParentRelationshipOneToOne,
                            Title = "Service",
                            ReferenceEntityId = new ObjectId("5ca876fa4a73264e4c06df52")
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876fb4a73264e4c06df5e"),
                            InternalName = "Entity",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Root Entity",
                            ReferenceEntityId = new ObjectId("5ca876f24a73264e4c06def3"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new ObjectId("5ca876fb4a73264e4c06df5f"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        }
                    }
                },
#endregion
                #region Web Service Entity
                new Entity()
                {
                    Id = new ObjectId("5ca876fb4a73264e4c06df60"),
                    InternalName = "WebService",
                    Title = "Web Service",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca876fc4a73264e4c06df61"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876fc4a73264e4c06df62"),
                            InternalName = "Service",
                            PropertyType = PropertyType.ParentRelationshipOneToOne,
                            Title = "Service",
                            ReferenceEntityId = new ObjectId("5ca876fa4a73264e4c06df52")
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876fc4a73264e4c06df63"),
                            InternalName = "DefaultBaseEndpoint",
                            PropertyType = PropertyType.String,
                            Title = "Default Base Endpoint",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca876fc4a73264e4c06df64"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new ObjectId("5ca876fc4a73264e4c06df65"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        }
                    }
                },
#endregion
                #region Web Service Operation Entity
                new Entity()
                {
                    Id = new ObjectId("5ca876fc4a73264e4c06df66"),
                    InternalName = "WebServiceOperation",
                    Title = "Web Service Operation",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca876fc4a73264e4c06df67"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876fc4a73264e4c06df68"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca876fc4a73264e4c06df69"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new ObjectId("5ca876fc4a73264e4c06df6a"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876fc4a73264e4c06df6b"),
                            InternalName = "RelativeRoute",
                            PropertyType = PropertyType.String,
                            Title = "Relative Route",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca876fd4a73264e4c06df6c"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                }
                            }
                        },
                        // TODO: Convert verb to reference
                        new Property()
                        {
                            Id = new ObjectId("5ca876fd4a73264e4c06df6d"),
                            InternalName = "Verb",
                            PropertyType = PropertyType.String,
                            Title = "Verb",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca876fd4a73264e4c06df6e"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876fd4a73264e4c06df6f"),
                            InternalName = "WebService",
                            PropertyType = PropertyType.ParentRelationshipOneToMany,
                            Title = "Web Service",
                            ReferenceEntityId = new ObjectId("5ca876fb4a73264e4c06df60"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new ObjectId("5ca876fd4a73264e4c06df70"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        }
                    }
                },
#endregion
                #region Form Field Entity
                new Entity()
                {
                    Id = new ObjectId("5ca876fd4a73264e4c06df71"),
                    InternalName = "FormField",
                    Title = "Form Field",
                    Properties = new Property[]
                    {
                        new Property("Id")
                        {
                            Id = new ObjectId("5ca876fd4a73264e4c06df72"),
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property("Title")
                        {
                            Id = new ObjectId("5ca876fd4a73264e4c06df73"),
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca876fd4a73264e4c06df74"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                }
                            }
                        },
                        new Property("FormSection")
                        {
                            Id = new ObjectId("5ca876fd4a73264e4c06df75"),
                            PropertyType = PropertyType.ParentRelationshipOneToMany,
                            Title = "Form Section",
                            ReferenceEntityId = new ObjectId("5ca876fd4a73264e4c06df76")
                        },
                        new Property("EntityProperty")
                        {
                            Id = new ObjectId("5ca876fe4a73264e4c06df77"),
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Entity Property",
                            ReferenceEntityId = new ObjectId("5ca876f34a73264e4c06defc"),
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca876fe4a73264e4c06df78"),
                                    ValidationType = ValidationType.Required
                                }
                            },
                            FilterExpression = new Expression
                            {
                                // ScreenSection.EntityId
                                PropertyId = new ObjectId("5ca876f94a73264e4c06df47"),
                                Operator = Request.Enumerations.ExpressionOperator.Equal,
                                // Property.EntityId
                                ReferencePropertyId = new ObjectId("5ca876f34a73264e4c06defe")
                            }
                        }
                    }
                },
#endregion
                #region Form Section Entity
                new Entity()
                {
                    Id = new ObjectId("5ca876fd4a73264e4c06df76"),
                    InternalName = "FormSection",
                    Title = "Form Section",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca876fe4a73264e4c06df79"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property("Dummy")
                        {
                            Id = new ObjectId("5ca876fe4a73264e4c06df7a"),
                            PropertyType = PropertyType.String,
                            Title = "Dummy",
                            DefaultStringValue = "Dummy",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    ValidationType = ValidationType.Required
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876fe4a73264e4c06df7b"),
                            InternalName = "ScreenSection",
                            PropertyType = PropertyType.ParentRelationshipOneToOne,
                            Title = "Screen Section",
                            ReferenceEntityId = new ObjectId("5ca876f84a73264e4c06df39"),
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    ValidationType = ValidationType.RequiredExpression,
                                    Expression = new Expression
                                    {
                                        // Screen Section
                                        EntityId = new ObjectId("5ca876f84a73264e4c06df39"),
                                        // Screen Section Type
                                        PropertyId = new ObjectId("5ca876f94a73264e4c06df44"),
                                        Operator = Request.Enumerations.ExpressionOperator.Equal,
                                        // Form
                                        UniqueidentifierValue = new ObjectId("5ca876fe4a73264e4c06df7c")
                                    }
                                }
                            }
                        }
                        // At the moment it only has a collection of form Fields
                    }
                },
#endregion
                #region Search Section Entity
                new Entity()
                {
                    Id = new ObjectId("5ca876fe4a73264e4c06df7d"),
                    InternalName = "SearchSection",
                    Title = "Search Section",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca876fe4a73264e4c06df7e"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876fe4a73264e4c06df7f"),
                            InternalName = "ScreenSection",
                            PropertyType = PropertyType.ParentRelationshipOneToOne,
                            Title = "Screen Section",
                            ReferenceEntityId = new ObjectId("5ca876f84a73264e4c06df39")
                        },
                        new Property("Dummy")
                        {
                            Id = new ObjectId("5ca876fe4a73264e4c06df80"),
                            PropertyType = PropertyType.String,
                            Title = "Dummy",
                            DefaultStringValue = "Dummy",
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
#endregion
                #region Search Column Entity
                new Entity()
                {
                    Id = new ObjectId("5ca876fe4a73264e4c06df81"),
                    InternalName = "SearchColumn",
                    Title = "Search Column",
                    Properties = new Property[]
                    {
                        new Property("Id")
                        {
                            Id = new ObjectId("5ca876ff4a73264e4c06df82"),
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property("Title")
                        {
                            Id = new ObjectId("5ca876ff4a73264e4c06df83"),
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca876ff4a73264e4c06df84"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                }
                            }
                        },
                        new Property("Ordinal")
                        {
                            Id = new ObjectId("5ca876ff4a73264e4c06df85"),
                            PropertyType = PropertyType.Integer,
                            Title = "Ordinal",
                            DefaultIntegerValue = 1,
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca876ff4a73264e4c06df84"),
                                    ValidationType = ValidationType.Required
                                }
                            }
                        },
                        new Property("SearchSection")
                        {
                            Id = new ObjectId("5ca876ff4a73264e4c06df86"),
                            PropertyType = PropertyType.ParentRelationshipOneToMany,
                            Title = "Search Section",
                            ReferenceEntityId = new ObjectId("5ca876fe4a73264e4c06df7d")
                        },
                        new Property("EntityProperty")
                        {
                            Id = new ObjectId("5ca876ff4a73264e4c06df87"),
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Entity Property",
                            ReferenceEntityId = new ObjectId("5ca876f34a73264e4c06defc"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new ObjectId("5ca876ff4a73264e4c06df88"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        }
                    }
                },
#endregion
                #region NuGet Import Entity
                new Entity()
                {
                    Id = new ObjectId("5ca876ff4a73264e4c06df89"),
                    InternalName = "NuGetDependency",
                    Title = "NuGet Import",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca876ff4a73264e4c06df8a"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca876ff4a73264e4c06df8b"),
                            InternalName = "Project",
                            PropertyType = PropertyType.ParentRelationshipOneToMany,
                            Title = "Project",
                            ReferenceEntityId = new ObjectId("5ca869596668b25914b67e6e"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new ObjectId("5ca876ff4a73264e4c06df8c"),
                                    ValidationType = ValidationType.Required
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca877004a73264e4c06df8d"),
                            InternalName = "Include",
                            PropertyType = PropertyType.String,
                            Title = "Include",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca877004a73264e4c06df8e"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation
                                {
                                    Id = new ObjectId("5ca877004a73264e4c06df8f"),
                                    ValidationType = ValidationType.Required
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca877004a73264e4c06df90"),
                            InternalName = "Version",
                            PropertyType = PropertyType.String,
                            Title = "Version",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca877004a73264e4c06df91"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca877004a73264e4c06df92"),
                            InternalName = "PrivateAssets",
                            PropertyType = PropertyType.String,
                            Title = "Private Assets",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca877004a73264e4c06df93"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 2048
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca877004a73264e4c06df94"),
                            InternalName = "IncludeAssets",
                            PropertyType = PropertyType.String,
                            Title = "Include Assets",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca877004a73264e4c06df95"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 2048
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca877004a73264e4c06df96"),
                            InternalName = "ExcludeAssets",
                            PropertyType = PropertyType.String,
                            Title = "Exclude Assets",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca877004a73264e4c06df97"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 2048
                                },
                            }
                        }
                    }
                },
                #endregion
                #region NuGet Package Source Entity
                new Entity()
                {
                    Id = new ObjectId("5ca877004a73264e4c06df98"),
                    InternalName = "NuGetPackageSource",
                    Title = "NuGet Package Source",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca877014a73264e4c06df99"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca877014a73264e4c06df9a"),
                            InternalName = "Project",
                            PropertyType = PropertyType.ParentRelationshipOneToMany,
                            Title = "Project",
                            ReferenceEntityId = new ObjectId("5ca869596668b25914b67e6e"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new ObjectId("5ca877014a73264e4c06df9b"),
                                    ValidationType = ValidationType.Required
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca877014a73264e4c06df9c"),
                            InternalName = "Key",
                            PropertyType = PropertyType.String,
                            Title = "Key",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca877014a73264e4c06df9d"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation
                                {
                                    Id = new ObjectId("5ca877014a73264e4c06df9e"),
                                    ValidationType = ValidationType.Required
                                },
                                new Validation{
                                    Id = new ObjectId("5ca877014a73264e4c06df9f"),
                                    ValidationType = ValidationType.Pattern,
                                    StringValue = "^[a-zA-Z]+$"
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca877014a73264e4c06dfa0"),
                            InternalName = "Value",
                            PropertyType = PropertyType.String,
                            Title = "Value",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca877014a73264e4c06dfa1"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 2048
                                },
                                new Validation
                                {
                                    Id = new ObjectId("5ca877014a73264e4c06dfa2"),
                                    ValidationType = ValidationType.Required
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca877014a73264e4c06dfa3"),
                            InternalName = "ApiKey",
                            PropertyType = PropertyType.String,
                            Title = "Api Key",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca877024a73264e4c06dfa4"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca877024a73264e4c06dfa5"),
                            InternalName = "Username",
                            PropertyType = PropertyType.String,
                            Title = "User name",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca877024a73264e4c06dfa6"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca877024a73264e4c06dfa7"),
                            InternalName = "ClearTextPassword",
                            PropertyType = PropertyType.String,
                            Title = "Password",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca877024a73264e4c06dfa8"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                }
                            }
                        },
                    }
                },
#endregion
            };

            project.Screens = new Screen[]
            {
                #region Home Screen
                new Screen()
                {
                    Id = new ObjectId("5ca876ed4a73264e4c06dedb"),
                    EntityId = new ObjectId("5ca869596668b25914b67e6e"),
                    ScreenType = ScreenType.Search,
                    Title = "Home",
                    Path = "home",
                    Template = ScreenTemplate.Home,
                    ScreenFeatures = new ScreenFeature[]
                    {
                        new ScreenFeature{
                            Id = new ObjectId("5ca877024a73264e4c06dfa9"),
                            Feature = Feature.New
                        }
                    },
                    ScreenSections = new ScreenSection[]
                    {
                        new ScreenSection
                        {
                            Id = new ObjectId("5ca877024a73264e4c06dfaa"),
                            Title = "Home",
                            InternalName = "Home",
                            ScreenSectionType = ScreenSectionType.Html,
                            Html = @"
<p>Welcome to Master Builder an application builder that you configure and we handle the rest</p>
<ul>
    <li><a href=""https://get.asp.net/"">ASP.NET Core</a> and <a href=""https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx"">C#</a> for cross-platform server-side code</li>
    <li><a href=""https://webpack.github.io/"">Webpack</a> for building and bundling client-side resources</li>
</ul>"
                        },
                        new ScreenSection
                        {
                            Id = new ObjectId("5ca877024a73264e4c06dfab"),
                            EntityId = new ObjectId("5ca869596668b25914b67e6e"),
                            ScreenSectionType = ScreenSectionType.Search,
                            Title = "Projects",
                            InternalName = "Projects",
                            NavigateToScreenId = new ObjectId("5ca877024a73264e4c06dfac"),
                            SearchSection = new SearchSection
                            {

                                SearchColumns = new SearchColumn[]
                                {
                                    new SearchColumn
                                    {
                                        EntityPropertyId = new ObjectId("5ca876ee4a73264e4c06dedd"),
                                        Title = "Title",
                                        Ordinal = 0
                                    },
                                    new SearchColumn
                                    {
                                        EntityPropertyId = new ObjectId("5ca876f14a73264e4c06dee6"),
                                        Title = "Major Version",
                                        Ordinal = 10
                                    },
                                    new SearchColumn
                                    {
                                        EntityPropertyId = new ObjectId("5ca876f14a73264e4c06dee8"),
                                        Title = "Minor Version",
                                        Ordinal = 20
                                    },
                                    new SearchColumn
                                    {
                                        EntityPropertyId = new ObjectId("5ca876f14a73264e4c06deea"),
                                        Title = "Build Version",
                                        Ordinal = 40
                                    },
                                }
                            }
                        }
                    }
                },
                #endregion
                Kitchen.Screens.Project.GetScreen(),
                #region Entity Form Screen
                new Screen()
                {
                    Id = new ObjectId("5ca877034a73264e4c06dfaf"),
                    EntityId = new ObjectId("5ca876f24a73264e4c06def3"),
                    ScreenType = ScreenType.Form,
                    Title = "Entity",
                    Path = "entity",
                    DefaultObjectJsonData = JsonConvert.SerializeObject(new
                    {
                        Properties =new []
                        {
                            new {
                                Title = "Title",
                                InternalName = "Title",
                                PropertyTypeId = "5ca877044a73264e4c06dfc1"
                            }
                        }
                    }),
                    ScreenSections = new ScreenSection[]
                    {
                        new ScreenSection
                        {
                            Id = new ObjectId("5ca877044a73264e4c06dfc2"),
                            Title = "Entity",
                            InternalName = "Entity",
                            EntityId = new ObjectId("5ca876f24a73264e4c06def3"),
                            ScreenSectionType = ScreenSectionType.Form,
                            FormSection = new FormSection()
                            {
                                FormFields = new FormField[]
                                {
                                    new FormField
                                    {
                                        // Title
                                        EntityPropertyId = new ObjectId("5ca876f24a73264e4c06def5")
                                    },
                                    new FormField
                                    {
                                        // Internal Name
                                        EntityPropertyId = new ObjectId("5ca876f24a73264e4c06def6")
                                    },
                                    new FormField
                                    {
                                        // Entity Template
                                        EntityPropertyId = new ObjectId("5ca876f24a73264e4c06def7")
                                    },
                                    new FormField
                                    {
                                        // Has Seed Data
                                        EntityPropertyId = new ObjectId("5ca876f24a73264e4c06defa")
                                    }
                                }
                            }
                        },
                        new ScreenSection
                        {
                            Id = new ObjectId("5ca877044a73264e4c06dfc3"),
                            Title = "Properties",
                            InternalName = "Properties",
                            ScreenSectionTypeId = new ObjectId("5ca877044a73264e4c06dfc4"), // Search
                            EntityId = new ObjectId("5ca876f34a73264e4c06defc"),
                            NavigateToScreenId = new ObjectId("5ca877054a73264e4c06dfc5"),
                            MenuItems = new MenuItem[]
                            {
                                new MenuItem
                                {
                                    Id = new ObjectId("5ca877054a73264e4c06dfc6"),
                                    MenuItemType = MenuItemType.New,
                                    ScreenId = new ObjectId("5ca877054a73264e4c06dfc5"),
                                    Title = "New"
                                }
                            },
                            SearchSection = new SearchSection
                            {
                                SearchColumns = new SearchColumn[]
                                {
                                    new SearchColumn
                                    {
                                        // Title
                                        EntityPropertyId = new ObjectId("5ca876f34a73264e4c06df00")
                                    },
                                    new SearchColumn
                                    {
                                        // Internal Name
                                        EntityPropertyId = new ObjectId("5ca876f34a73264e4c06df03")
                                    },
                                    new SearchColumn
                                    {
                                        // Property Type
                                        EntityPropertyId = new ObjectId("5ca876f34a73264e4c06df06")
                                    }
                                }
                            }
                        },
                        new ScreenSection
                        {
                            Id = new ObjectId("5ca877054a73264e4c06dfc7"),
                            Title = "Seed",
                            InternalName = "Seed",
                            ScreenSectionType = ScreenSectionType.Form,
                            EntityId = new ObjectId("5ca876fa4a73264e4c06df4a"),
                            ParentScreenSectionId = new ObjectId("5ca877044a73264e4c06dfc2"),
                            VisibilityExpression = new Expression
                            {
                                PropertyId = new ObjectId("5ca876f24a73264e4c06defa"),
                                Operator = Request.Enumerations.ExpressionOperator.Equal,
                                BooleanValue = true
                            }
                        }
                    }
                },
                #endregion
                #region Screen Form Screen
                new Screen()
                {
                    Id = new ObjectId("5ca877034a73264e4c06dfb2"),
                    EntityId =new ObjectId("5ca876f14a73264e4c06deef"),
                    ScreenType = ScreenType.Form,
                    Title = "Screen",
                    Path = "screen",
                    ScreenSections = new ScreenSection[]
                    {
                        new ScreenSection
                        {
                            Id = new ObjectId("5ca877054a73264e4c06dfc8"),
                            Title = "Screen",
                            InternalName = "Screen",
                            ScreenSectionType = ScreenSectionType.Form,
                            EntityId = new ObjectId("5ca876f14a73264e4c06deef"),
                            FormSection = new FormSection
                            {
                                FormFields = new FormField[]
                                {
                                    new FormField
                                    {
                                        // Title
                                        EntityPropertyId = new ObjectId("5ca876f74a73264e4c06df2a")
                                    },
                                    new FormField
                                    {
                                        // Screen Type
                                        EntityPropertyId = new ObjectId("5ca876f74a73264e4c06df31")
                                    },
                                    new FormField
                                    {
                                        // Path
                                        EntityPropertyId = new ObjectId("5ca876f74a73264e4c06df2d")
                                    },
                                    new FormField
                                    {
                                        // Entity
                                        EntityPropertyId = new ObjectId("5ca876f74a73264e4c06df2e"),
                                        VisibilityExpression = new Expression
                                        {
                                            PropertyId = new ObjectId("5ca876f74a73264e4c06df31"),
                                            Operator = Request.Enumerations.ExpressionOperator.In,
                                            UniqueidentifierValues = new ObjectId[]
                                            {
                                                // Search
                                                new ObjectId("5ca877054a73264e4c06dfc9"),
                                                // Form
                                                new ObjectId("5ca877054a73264e4c06dfca"),
                                            }
                                        }
                                    },
                                    new FormField
                                    {
                                        // Template
                                        EntityPropertyId = new ObjectId("5ca876f74a73264e4c06df2f")
                                    },
                                    new FormField
                                    {
                                        // Has Default Object
                                        EntityPropertyId = new ObjectId("5ca876f84a73264e4c06df33"),
                                        VisibilityExpression = new Expression
                                        {
                                            PropertyId = new ObjectId("5ca876f74a73264e4c06df31"),
                                            Operator = Request.Enumerations.ExpressionOperator.In,
                                            UniqueidentifierValues = new ObjectId[]
                                            {
                                                // Search
                                                new ObjectId("5ca877054a73264e4c06dfc9"),
                                                // Form
                                                new ObjectId("5ca877054a73264e4c06dfca"),
                                            }
                                        }
                                    },
                                    new FormField
                                    {
                                        //Default Object Json Data
                                        EntityPropertyId = new ObjectId("5ca876f84a73264e4c06df35"),
                                        VisibilityExpression = new Expression
                                        {
                                            PropertyId = new ObjectId("5ca876f84a73264e4c06df33"),
                                            Operator = Request.Enumerations.ExpressionOperator.Equal,
                                            BooleanValue = true
                                        }
                                    }
                                }
                            }
                        },
                        new ScreenSection
                        {
                            Id = new ObjectId("5ca877054a73264e4c06dfcb"),
                            Title = "Screen Sections",
                            InternalName = "ScreenSections",
                            ScreenSectionType = ScreenSectionType.Search,
                            EntityId = new ObjectId("5ca876f84a73264e4c06df39"),
                            NavigateToScreenId = new ObjectId("5ca877054a73264e4c06dfcc"),
                            SearchSection = new SearchSection
                            {
                                SearchColumns = new SearchColumn[]
                                {
                                    new SearchColumn
                                    {
                                        // Title
                                        EntityPropertyId = new ObjectId("5ca876f84a73264e4c06df3b")
                                    },
                                    new SearchColumn
                                    {
                                        // Internal Name
                                        EntityPropertyId = new ObjectId("5ca876f84a73264e4c06df3e")
                                    },
                                    new SearchColumn
                                    {
                                        // Screen Section Type
                                        EntityPropertyId = new ObjectId("5ca876f94a73264e4c06df44")
                                    },
                                    new SearchColumn
                                    {
                                        // Entity
                                        EntityPropertyId = new ObjectId("5ca876f94a73264e4c06df47")
                                    }
                                }
                            },
                            MenuItems = new MenuItem[]
                            {
                                new MenuItem
                                {
                                    Id = new ObjectId("5ca877054a73264e4c06dfcd"),
                                    MenuItemType = MenuItemType.New,
                                    ScreenId = new ObjectId("5ca877054a73264e4c06dfcc"),
                                    Title = "New"
                                }
                            }
                        }
                    }
                },
                #endregion
                #region Screen Section Form Screen
                new Screen()
                {
                    Id = new ObjectId("5ca877054a73264e4c06dfcc"),
                    EntityId =new ObjectId("5ca876f84a73264e4c06df39"),
                    ScreenType = ScreenType.Form,
                    Title = "Screen Section",
                    Path = "screen-section",
                    ScreenSections = new ScreenSection[]
                    {
                        new ScreenSection
                        {
                            Id = new ObjectId("5ca877054a73264e4c06dfc8"),
                            Title = "Screen Section",
                            InternalName = "ScreenSection",
                            ScreenSectionType = ScreenSectionType.Form,
                            EntityId = new ObjectId("5ca876f84a73264e4c06df39"),
                            FormSection = new FormSection
                            {
                                FormFields = new FormField[]
                                {
                                    new FormField
                                    {
                                        // Screen
                                        EntityPropertyId = new ObjectId("5ca876f94a73264e4c06df41"),
                                        IsHiddenFromUi = true
                                    },
                                    new FormField
                                    {
                                        // Title
                                        EntityPropertyId = new ObjectId("5ca876f84a73264e4c06df3b")
                                    },
                                    new FormField
                                    {
                                        // Internal Name
                                        EntityPropertyId = new ObjectId("5ca876f84a73264e4c06df3e")
                                    },
                                    new FormField
                                    {
                                        // Screen Section Type
                                        EntityPropertyId = new ObjectId("5ca876f94a73264e4c06df44")
                                    },
                                    new FormField
                                    {
                                        // Entity
                                        EntityPropertyId = new ObjectId("5ca876f94a73264e4c06df47")
                                    },
                                    new FormField
                                    {
                                        // Navigate To Screen
                                        EntityPropertyId = new ObjectId("5ca876f94a73264e4c06df43")
                                    }
                                }
                            }
                        },
                        new ScreenSection
                        {
                            Id = new ObjectId("5ca877054a73264e4c06dfce"),
                            Title = "Html Screen Section",
                            InternalName = "HtmlScreenSection",
                            ScreenSectionType = ScreenSectionType.Form,
                            EntityId = new ObjectId("5ca876f84a73264e4c06df39"),
                            VisibilityExpression = new Expression
                            {
                                PropertyId = new ObjectId("5ca876f94a73264e4c06df44"),
                                Operator = Request.Enumerations.ExpressionOperator.Equal,
                                // Html
                                UniqueidentifierValue = new ObjectId("5ca877054a73264e4c06dfcf")
                            },
                            FormSection = new FormSection
                            {
                                FormFields = new FormField[]
                                {
                                    new FormField
                                    {
                                        // Html
                                        EntityPropertyId = new ObjectId("5ca876fa4a73264e4c06df49")
                                    }
                                }
                            }
                        },
                        new ScreenSection
                        {
                            Id = new ObjectId("5ca877064a73264e4c06dfd0"),
                            Title = "Form Section",
                            InternalName = "FormSection",
                            EntityId = new ObjectId("5ca876fd4a73264e4c06df76"),
                            ScreenSectionType = ScreenSectionType.Form,
                            ParentScreenSectionId = new ObjectId("5ca877054a73264e4c06dfc8"),
                            VisibilityExpression = new Expression
                            {
                                PropertyId = new ObjectId("5ca876f94a73264e4c06df44"),
                                Operator = Request.Enumerations.ExpressionOperator.Equal,
                                // Form
                                UniqueidentifierValue = new ObjectId("5ca876fe4a73264e4c06df7c")
                            },
                            FormSection = new FormSection
                            {
                                FormFields = new FormField[]
                                {
                                    new FormField
                                    {
                                        EntityPropertyId = new ObjectId("5ca876fe4a73264e4c06df7a")
                                    }
                                }
                            }
                        },
                        new ScreenSection
                        {
                            Id = new ObjectId("5ca877064a73264e4c06dfd1"),
                            Title = "Form Fields",
                            InternalName = "FormFields",
                            ScreenSectionType = ScreenSectionType.Search,
                            EntityId = new ObjectId("5ca876fd4a73264e4c06df71"),
                            ParentScreenSectionId = new ObjectId("5ca877064a73264e4c06dfd0"),
                            NavigateToScreenId = new ObjectId("5ca877064a73264e4c06dfd2"),
                            VisibilityExpression = new Expression
                            {
                                PropertyId = new ObjectId("5ca876f94a73264e4c06df44"),
                                Operator = Request.Enumerations.ExpressionOperator.Equal,
                                // Form
                                UniqueidentifierValue = new ObjectId("5ca876fe4a73264e4c06df7c")
                            },
                            SearchSection = new SearchSection
                            {
                                SearchColumns = new SearchColumn[]
                                {
                                    new SearchColumn
                                    {
                                        // Entity Property
                                        EntityPropertyId = new ObjectId("5ca876fe4a73264e4c06df77")
                                    },
                                    new SearchColumn
                                    {
                                        // Title
                                        EntityPropertyId = new ObjectId("5ca876fd4a73264e4c06df73")
                                    }
                                }
                            }
                        },
                        new ScreenSection
                        {
                            Id = new ObjectId("5ca877064a73264e4c06dfd3"),
                            Title = "Search Section",
                            InternalName = "SearchSection",
                            EntityId = new ObjectId("5ca876fe4a73264e4c06df7d"),
                            ScreenSectionType = ScreenSectionType.Form,
                            ParentScreenSectionId = new ObjectId("5ca877054a73264e4c06dfc8"),
                            VisibilityExpression = new Expression
                            {
                                PropertyId = new ObjectId("5ca876f94a73264e4c06df44"),
                                Operator = Request.Enumerations.ExpressionOperator.Equal,
                                // Search
                                UniqueidentifierValue = new ObjectId("5ca877044a73264e4c06dfc4")
                            },
                            FormSection = new FormSection
                            {
                                FormFields = new FormField[]
                                {
                                    new FormField
                                    {
                                        EntityPropertyId = new ObjectId("5ca876fe4a73264e4c06df80")
                                    }
                                }
                            }
                        },
                        new ScreenSection
                        {
                            Id = new ObjectId("5ca877064a73264e4c06dfd4"),
                            Title = "Search Columns",
                            InternalName = "SearchColumns",
                            ScreenSectionType = ScreenSectionType.Search,
                            EntityId = new ObjectId("5ca876fe4a73264e4c06df81"),
                            ParentScreenSectionId = new ObjectId("5ca877064a73264e4c06dfd3"),
                            NavigateToScreenId = new ObjectId("5ca877064a73264e4c06dfd5"),
                            VisibilityExpression = new Expression
                            {
                                PropertyId = new ObjectId("5ca876f94a73264e4c06df44"),
                                Operator = Request.Enumerations.ExpressionOperator.Equal,
                                // Search
                                UniqueidentifierValue = new ObjectId("5ca877044a73264e4c06dfc4")
                            },
                            SearchSection = new SearchSection
                            {
                                SearchColumns = new SearchColumn[]
                                {
                                    new SearchColumn
                                    {
                                        // Entity Property
                                        EntityPropertyId = new ObjectId("5ca876ff4a73264e4c06df87")
                                    },
                                    new SearchColumn
                                    {
                                        // Title
                                        EntityPropertyId = new ObjectId("5ca876ff4a73264e4c06df83")
                                    },
                                    new SearchColumn
                                    {
                                        EntityPropertyId = new ObjectId("5ca876ff4a73264e4c06df85")
                                    }
                                }
                            }
                        }
                    }
                },
                #endregion
                #region Form Field Screen
                new Screen
                {
                    Id = new ObjectId("5ca877064a73264e4c06dfd2"),
                    EntityId = new ObjectId("5ca876fd4a73264e4c06df71"),
                    Path = "form-field",
                    ScreenType = ScreenType.Form,
                    Title = "Form Field",
                    ScreenSections = new ScreenSection[]
                    {
                        new ScreenSection
                        {
                            Id = new ObjectId("5ca877064a73264e4c06dfd6"),
                            EntityId = new ObjectId("5ca876fd4a73264e4c06df71"),
                            ScreenSectionType = ScreenSectionType.Form,
                            Title = "Form Field",
                            InternalName = "FormField",
                            FormSection = new FormSection
                            {
                                FormFields = new FormField[]
                                {
                                    new FormField
                                    {
                                        // Title
                                        EntityPropertyId = new ObjectId("5ca876fd4a73264e4c06df73")
                                    },
                                    new FormField
                                    {
                                        // Entity Property
                                        EntityPropertyId = new ObjectId("5ca876fe4a73264e4c06df77")
                                    }
                                }
                            }
                        }
                    }
                },
                #endregion
                #region Search Column Screen
                new Screen
                {
                    Id = new ObjectId("5ca877064a73264e4c06dfd5"),
                    EntityId = new ObjectId("5ca876fe4a73264e4c06df81"),
                    Path = "search-column",
                    ScreenType = ScreenType.Form,
                    Title = "Search Column",
                    ScreenSections = new ScreenSection[]
                    {
                        new ScreenSection
                        {
                            Id = new ObjectId("5ca877064a73264e4c06dfd7"),
                            EntityId = new ObjectId("5ca876fe4a73264e4c06df81"),
                            ScreenSectionType = ScreenSectionType.Form,
                            Title = "Search Column",
                            InternalName = "SearchColumn"
                        }
                    }
                },
#endregion
                #region Menu Item
                new Screen()
                {
                    Id = new ObjectId("5ca877034a73264e4c06dfb5"),
                    EntityId = new ObjectId("5ca876f64a73264e4c06df1e"),
                    ScreenType = ScreenType.Form,
                    Title = "Menu Item",
                    Path = "menu-item",
                    ScreenSections = new ScreenSection[]
                    {
                        new ScreenSection
                        {
                            Id = new ObjectId("5ca877064a73264e4c06dfd8"),
                            Title = "Menu Item",
                            InternalName = "MenuItem",
                            ScreenSectionType = ScreenSectionType.Form,
                            EntityId = new ObjectId("5ca876f64a73264e4c06df1e")
                        }
                    }
                },
                #endregion
                #region Property Form Screen
                new Screen()
                {
                    Id = new ObjectId("5ca877054a73264e4c06dfc5"),
                    EntityId = new ObjectId("5ca876f34a73264e4c06defc"),
                    ScreenType = ScreenType.Form,
                    Title = "Property",
                    Path = "property",
                    ScreenSections = new ScreenSection[]
                    {
                        new ScreenSection
                        {
                            Id = new ObjectId("5ca877064a73264e4c06dfd9"),
                            Title = "Property",
                            InternalName = "Property",
                            ScreenSectionType = ScreenSectionType.Form,
                            FormSection = new FormSection
                            {
                                FormFields = new FormField[]
                                {
                                    new FormField
                                    {
                                        EntityPropertyId = new ObjectId("5ca876f34a73264e4c06df00")
                                    },
                                    new FormField
                                    {
                                        EntityPropertyId = new ObjectId("5ca876f34a73264e4c06df03")
                                    },
                                    new FormField
                                    {
                                        EntityPropertyId = new ObjectId("5ca876f34a73264e4c06df06")
                                    },
                                    new FormField
                                    {
                                        EntityPropertyId = new ObjectId("5ca876f44a73264e4c06df09")
                                    },
                                    new FormField
                                    {
                                        // Parent Entity
                                        EntityPropertyId = new ObjectId("5ca876f44a73264e4c06df0b"),
                                        VisibilityExpression = new Expression
                                        {
                                            PropertyId = new ObjectId("5ca876f34a73264e4c06df06"),
                                            Operator = Request.Enumerations.ExpressionOperator.In,
                                            UniqueidentifierValues = new ObjectId[]
                                            {
                                                // Parent Relationship One To Many
                                                new ObjectId("5ca86c626668b25914b67e78"),
                                                // Reference Relationship
                                                new ObjectId("5ca86c956668b25914b67e79"),
                                                // Parent Relationship One To One
                                                new ObjectId("5ca877064a73264e4c06dfda")
                                            }
                                        }
                                    },
                                    new FormField
                                    {
                                        EntityPropertyId = new ObjectId("5ca876f44a73264e4c06df0c"),
                                        Title = "Default Value", // Integer
                                        VisibilityExpression = new Expression
                                        {
                                            PropertyId = new ObjectId("5ca876f34a73264e4c06df06"),
                                            Operator = Request.Enumerations.ExpressionOperator.Equal,
                                            UniqueidentifierValue = new ObjectId("5ca86b506668b25914b67e76")
                                        }
                                    },
                                    new FormField
                                    {
                                        EntityPropertyId = new ObjectId("5ca876f44a73264e4c06df0d"),
                                        Title = "Default Value", // Double
                                        VisibilityExpression = new Expression
                                        {
                                            PropertyId = new ObjectId("5ca876f34a73264e4c06df06"),
                                            Operator = Request.Enumerations.ExpressionOperator.Equal,
                                            UniqueidentifierValue = new ObjectId("5ca86b656668b25914b67e77")
                                        }
                                    },
                                    new FormField
                                    {
                                        EntityPropertyId = new ObjectId("5ca876f44a73264e4c06df0e"),
                                        Title = "Default Value", // String
                                        VisibilityExpression = new Expression
                                        {
                                            PropertyId = new ObjectId("5ca876f34a73264e4c06df06"),
                                            Operator = Request.Enumerations.ExpressionOperator.Equal,
                                            UniqueidentifierValue = new ObjectId("5ca877044a73264e4c06dfc1")
                                        }
                                    },
                                    new FormField
                                    {
                                        EntityPropertyId = new ObjectId("5ca876f44a73264e4c06df0f"),
                                        Title = "Default Value", // Boolean
                                        VisibilityExpression = new Expression
                                        {
                                            PropertyId = new ObjectId("5ca876f34a73264e4c06df06"),
                                            Operator = Request.Enumerations.ExpressionOperator.Equal,
                                            UniqueidentifierValue = new ObjectId("5ca86b146668b25914b67e74")
                                        }
                                    },
                                }
                            }
                        },
                        new ScreenSection
                        {
                            Id = new ObjectId("5ca877074a73264e4c06dfdb"),
                            Title = "Validation",
                            InternalName = "Validation",
                            EntityId = new ObjectId("5ca876f44a73264e4c06df10"),
                            ScreenSectionType = ScreenSectionType.Search,
                            // Validation
                            NavigateToScreenId = new ObjectId("5ca877074a73264e4c06dfdc"),
                            SearchSection = new SearchSection
                            {
                                SearchColumns = new SearchColumn[]
                                {
                                    new SearchColumn
                                    {
                                        // Title
                                        EntityPropertyId = new ObjectId("5ca876f44a73264e4c06df12")
                                    },
                                    new SearchColumn
                                    {
                                        // Validation Type
                                        EntityPropertyId = new ObjectId("5ca876f54a73264e4c06df17")
                                    }
                                }
                            }
                        }
                    }
                },
                #endregion
                #region Validation Form Screen
                new Screen
                {
                    Id = new ObjectId("5ca877074a73264e4c06dfdc"),
                    Title = "Validation",
                    Path = "validation",
                    EntityId = new ObjectId("5ca876f44a73264e4c06df10"),
                    ScreenType = ScreenType.Form,
                    ScreenSections = new ScreenSection[]
                    {
                        new ScreenSection
                        {
                            Id = new ObjectId("5ca877074a73264e4c06dfdd"),
                            Title = "Validation",
                            InternalName = "Validation",
                            EntityId = new ObjectId("5ca876f44a73264e4c06df10"),
                            ScreenSectionType = ScreenSectionType.Form,
                            FormSection = new FormSection
                            {
                                FormFields = new FormField[]
                                {
                                    new FormField{
                                        // Title
                                        EntityPropertyId = new ObjectId("5ca876f44a73264e4c06df12")
                                    },
                                    new FormField
                                    {
                                        // Parent Relationship
                                        EntityPropertyId = new ObjectId("5ca876f54a73264e4c06df15")
                                    },
                                    new FormField
                                    {
                                        // Validation Type
                                        EntityPropertyId = new ObjectId("5ca876f54a73264e4c06df17")
                                    },
                                    new FormField
                                    {
                                        // Integer Value
                                        EntityPropertyId = new ObjectId("5ca876f54a73264e4c06df1a"),
                                        VisibilityExpression = new Expression
                                        {
                                            PropertyId = new ObjectId("5ca876f54a73264e4c06df17"),
                                            Operator = Request.Enumerations.ExpressionOperator.In,
                                            UniqueidentifierValues = new ObjectId[]
                                            {
                                                // Max Length
                                                new ObjectId("5ca877074a73264e4c06dfde"),
                                                // Max Value
                                                new ObjectId("5ca877074a73264e4c06dfdf"),
                                                // Min Length
                                                new ObjectId("5ca877074a73264e4c06dfe0"),
                                                // Min Value
                                                new ObjectId("5ca877074a73264e4c06dfe1")
                                            }
                                        }
                                    },
                                    new FormField
                                    {
                                        // Double Value
                                        EntityPropertyId = new ObjectId("5ca876f54a73264e4c06df1b"),
                                        VisibilityExpression = new Expression
                                        {
                                            PropertyId = new ObjectId("5ca876f54a73264e4c06df17"),
                                            Operator = Request.Enumerations.ExpressionOperator.In,
                                            UniqueidentifierValues = new ObjectId[]
                                            {
                                                // Max Value
                                                new ObjectId("5ca877074a73264e4c06dfdf"),
                                                // Min Value
                                                new ObjectId("5ca877074a73264e4c06dfe1")
                                            }
                                        }
                                    },
                                    new FormField
                                    {
                                        // String Value
                                        EntityPropertyId = new ObjectId("5ca876f54a73264e4c06df1c"),
                                        Title = "Regular Expression",
                                        VisibilityExpression  = new Expression
                                        {
                                            PropertyId = new ObjectId("5ca876f54a73264e4c06df17"),
                                            Operator = Request.Enumerations.ExpressionOperator.Equal,
                                            UniqueidentifierValue = new ObjectId("5ca877074a73264e4c06dfe2")
                                        }
                                    },
                                    new FormField
                                    {
                                        // Message
                                        EntityPropertyId = new ObjectId("5ca876f64a73264e4c06df1d"),
                                        Title = "Validation Failed Message (Optional)"
                                    }
                                }
                            }
                        }
                    }
                },
#endregion
                #region Service Form Screen
                new Screen()
                {
                    Id = new ObjectId("5ca877034a73264e4c06dfb8"),
                    EntityId = new ObjectId("5ca876fa4a73264e4c06df52"),
                    ScreenType = ScreenType.Form,
                    Title = "Service",
                    Path = "service",
                    ScreenSections = new ScreenSection[]
                    {
                        new ScreenSection
                        {
                            Id = new ObjectId("5ca877074a73264e4c06dfe3"),
                            Title = "Service",
                            InternalName = "Service",
                            ScreenSectionType = ScreenSectionType.Form
                        }
                    }
                },
                #endregion
                #region NuGet Import Screens
                new Screen
                {
                    Id = new ObjectId("5ca877044a73264e4c06dfbb"),
                    EntityId = new ObjectId("5ca876ff4a73264e4c06df89"),
                    ScreenType = ScreenType.Form,
                    Title = "NuGet Dependency",
                    Path = "nuget",
                    ScreenSections = new ScreenSection[]
                    {
                        new ScreenSection
                        {
                            Id = new ObjectId("5ca877074a73264e4c06dfe4"),
                            Title = "NuGet Dependency",
                            InternalName = "NuGetDependency",
                            ScreenSectionType = ScreenSectionType.Form,
                            OrderBy = "Include"
                        }
                    }
                },
                #endregion
                #region NuGet Package Source Screens
                new Screen
                {
                    Id = new ObjectId("5ca877044a73264e4c06dfbe"),
                    EntityId = new ObjectId("5ca877004a73264e4c06df98"),
                    ScreenType = ScreenType.Form,
                    Title = "NuGet Package Source",
                    Path = "nugetSource",
                    ScreenSections = new ScreenSection[]
                    {
                        new ScreenSection
                        {
                            Id = new ObjectId("5ca877074a73264e4c06dfe5"),
                            Title = "NuGet Package Source",
                            InternalName = "NuGetPackageSource",
                            ScreenSectionType = ScreenSectionType.Form,
                            OrderBy = "Key"
                        }
                    }
                },
                #endregion
            };

            #region MENU ITEMS
            project.MenuItems = new MenuItem[]
            {
                new MenuItem
                {
                    Title = "Projects",
                    ScreenId = new ObjectId("5ca876ed4a73264e4c06dedb"),
                    Icon = "glyphicon glyphicon-th-list"
                }
            };
            #endregion

            #region SERVICES
            project.Services = new Service[]
            {
                new Service(){
                    Id = new ObjectId("5ca877084a73264e4c06dfe6"),
                    Title = "Master Builder Api",
                    ServiceType = ServiceType.WebService,
                    WebService = new WebService
                    {
                        DefaultBaseEndpoint = "https://localhost:44398",
                        Operations = new WebServiceOperation[]
                        {
                            new WebServiceOperation
                            {
                                Id = new ObjectId("5ca877084a73264e4c06dfe7"),
                                Title = "Publish",
                                Verb = HttpVerb.Post,
                                RelativeRoute = "/api/builder/publish"
                            }
                        }
                    }
                }
            };
            #endregion

            var entities = new List<Entity>(project.Entities);
            entities.AddRange(ReferenceEntities());
            project.Entities = entities;

            Project = project;
        }

        private IEnumerable<Entity> ReferenceEntities()
        {
            return new Entity[]
            {
                Kitchen.ReferenceEntities.DefaultDateTimeType.GetEntity(),
                #region Validation Type Entity
                new Entity()
                {
                    Id = new ObjectId("5ca876f54a73264e4c06df18"),
                    InternalName = "ValidationType",
                    Title = "Validation Type",
                    EntityTemplate = Oven.Request.EntityTemplate.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca877084a73264e4c06dfe8"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca877084a73264e4c06dfe9"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            PropertyTemplate = PropertyTemplate.ReferenceTitle
                        }
                    },
                    Seed = new Seed
                    {
                        SeedType = SeedType.EnsureAllUpdated,
                        JsonData = JsonConvert.SerializeObject(new []
                        {
                            new {
                                Id = new ObjectId("5ca877084a73264e4c06dfea"),
                                Title = "Email"
                            },
                            new {
                                Id = new ObjectId("5ca877074a73264e4c06dfde"),
                                Title = "Maximum Length"
                            },
                            new {
                                Id = new ObjectId("5ca877074a73264e4c06dfdf"),
                                Title = "Maximum Value"
                            },
                            new {
                                Id = new ObjectId("5ca877074a73264e4c06dfe0"),
                                Title = "Minimum Length"
                            },
                            new {
                                Id = new ObjectId("5ca877074a73264e4c06dfe1"),
                                Title = "Minimum Value"
                            },
                            new {
                                Id = new ObjectId("5ca877074a73264e4c06dfe2"),
                                Title = "Pattern"
                            },
                            new {
                                Id = new ObjectId("5ca877084a73264e4c06dfeb"),
                                Title = "Required"
                            },
                            new {
                                Id = new ObjectId("5ca877084a73264e4c06dfec"),
                                Title = "Required True"
                            },
                            new {
                                Id = new ObjectId("5ca877084a73264e4c06dfed"),
                                Title = "Unique"
                            }
                        })
                    }
                },
                #endregion
                #region Feature Entity
                new Entity()
                {
                    Id = new ObjectId("5ca877084a73264e4c06dfee"),
                    InternalName = "Feature",
                    Title = "Feature",
                    EntityTemplate = Request.EntityTemplate.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca877084a73264e4c06dfef"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca877084a73264e4c06dff0"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            PropertyTemplate = PropertyTemplate.ReferenceTitle
                        }
                    },
                    Seed = new Seed
                    {
                        SeedType = SeedType.EnsureAllUpdated,
                        JsonData = JsonConvert.SerializeObject(new []
                        {
                            new {
                                Id = new ObjectId("5ca877094a73264e4c06dff1"),
                                Title = "New"
                            }
                        })
                    }
                },
                #endregion
                #region Menu Item Type Entity
                new Entity()
                {
                    Id = new ObjectId("5ca876f64a73264e4c06df26"),
                    InternalName = "MenuItemType",
                    Title = "Menu Item Type",
                    EntityTemplate = Request.EntityTemplate.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca877094a73264e4c06dff2"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca877094a73264e4c06dff3"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            PropertyTemplate = PropertyTemplate.ReferenceTitle
                        }
                    },
                    Seed = new Seed
                    {
                        SeedType = SeedType.EnsureAllUpdated,
                        JsonData = JsonConvert.SerializeObject(new []
                        {
                            new {
                                Id = new ObjectId("5ca86a896668b25914b67e70"),
                                Title = "Application Link"
                            },
                            new {
                                Id = new ObjectId("5ca86a9b6668b25914b67e71"),
                                Title = "New"
                            }
                        })
                    }
                },
                #endregion
                #region Property Template Entity
                new Entity()
                {
                    Id = new ObjectId("5ca876f44a73264e4c06df0a"),
                    InternalName = "PropertyTemplate",
                    Title = "Property Template",
                    EntityTemplate = Request.EntityTemplate.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca877094a73264e4c06dff4"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca877084a73264e4c06dff0"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            PropertyTemplate = PropertyTemplate.ReferenceTitle
                        }
                    },
                    Seed = new Seed
                    {
                        SeedType = SeedType.EnsureAllUpdated,
                        JsonData = JsonConvert.SerializeObject(new []
                        {
                            new {
                                Id = new ObjectId("5ca877054a73264e4c06dfc9"),
                                Title = "Primary Key"
                            },
                            new {
                                Id = new ObjectId("5ca877094a73264e4c06dff5"),
                                Title = "Reference Title"
                            }
                        })
                    }
                },
                #endregion
                #region Property Type Entity
                new Entity()
                {
                    Id = new ObjectId("5ca876f34a73264e4c06df07"),
                    InternalName = "PropertyType",
                    Title = "Property Type",
                    EntityTemplate = Request.EntityTemplate.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca877094a73264e4c06dff6"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca877094a73264e4c06dff7"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            PropertyTemplate = PropertyTemplate.ReferenceTitle
                        }
                    },
                    Seed = new Seed
                    {
                        SeedType = SeedType.EnsureAllUpdated,
                        JsonData = JsonConvert.SerializeObject(new []
                        {
                            new {
                                Id = new ObjectId("5ca877094a73264e4c06dff8"),
                                Title = "Uniqueidentifier"
                            },
                            new {
                                Id = new ObjectId("5ca877044a73264e4c06dfc1"),
                                Title = "String"
                            },
                            new {
                                Id = new ObjectId("5ca86b506668b25914b67e76"),
                                Title = "Integer"
                            },
                            new {
                                Id = new ObjectId("5ca86b266668b25914b67e75"),
                                Title = "Date Time"
                            },
                            new {
                                Id = new ObjectId("5ca86b146668b25914b67e74"),
                                Title = "Boolean"
                            },
                            new {
                                Id = new ObjectId("5ca86c626668b25914b67e78"),
                                Title = "Parent Relationship, One to Many"
                            },
                            new {
                                Id = new ObjectId("5ca86c956668b25914b67e79"),
                                Title = "Reference Relationship"
                            },
                            new {
                                Id = new ObjectId("5ca877064a73264e4c06dfda"),
                                Title = "Parent Relationship, One to One"
                            },
                            new {
                                Id = new ObjectId("5ca86b656668b25914b67e77"),
                                Title = "Double"
                            }
                        })
                    }
                },
                #endregion
                #region Screen Section Type Entity
                new Entity()
                {
                    Id = new ObjectId("5ca876f94a73264e4c06df45"),
                    InternalName = "ScreenSectionType",
                    Title = "Screen Section Type",
                    EntityTemplate = Request.EntityTemplate.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca877094a73264e4c06dff9"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca877094a73264e4c06dffa"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            PropertyTemplate = PropertyTemplate.ReferenceTitle
                        }
                    },
                    Seed = new Seed
                    {
                        SeedType = SeedType.EnsureAllUpdated,
                        JsonData = JsonConvert.SerializeObject(new []
                        {
                            new {
                                Id = new ObjectId("5ca876fe4a73264e4c06df7c"),
                                Title = "Form"
                            },
                            new {
                                Id = new ObjectId("5ca877044a73264e4c06dfc4"),
                                Title = "Search"
                            },
                            new {
                                Id = new ObjectId("5ca877094a73264e4c06dffb"),
                                Title = "List of Menu Items"
                            },
                            new {
                                Id = new ObjectId("5ca877054a73264e4c06dfcf"),
                                Title = "Html"
                            },
                        })
                    }
                },
#endregion
                #region Screen Type Entity
                new Entity()
                {
                    Id = new ObjectId("5ca876f74a73264e4c06df32"),
                    InternalName = "ScreenType",
                    Title = "Screen Type",
                    EntityTemplate = Request.EntityTemplate.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca8770a4a73264e4c06dffc"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca8770a4a73264e4c06dffd"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            PropertyTemplate = PropertyTemplate.ReferenceTitle
                        }
                    },
                    Seed = new Seed
                    {
                        SeedType = SeedType.EnsureAllUpdated,
                        JsonData = JsonConvert.SerializeObject(new []
                        {
                            new {
                                Id = new ObjectId("5ca877054a73264e4c06dfc9"),
                                Title = "Search"
                            },
                            new {
                                Id = new ObjectId("5ca877054a73264e4c06dfca"),
                                Title = "Edit"
                            },
                            new {
                                Id = new ObjectId("5ca8770a4a73264e4c06dffe"),
                                Title = "View"
                            },
                            new {
                                Id = new ObjectId("5ca8770a4a73264e4c06dfff"),
                                Title = "Html"
                            }
                        })
                    }
                },
                #endregion
                #region Screen Template Entity
                new Entity()
                {
                    Id = new ObjectId("5ca876f74a73264e4c06df30"),
                    InternalName = "ScreenTemplate",
                    Title = "Screen Template",
                    EntityTemplate = Request.EntityTemplate.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca8770a4a73264e4c06e000"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca8770a4a73264e4c06e001"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            PropertyTemplate = PropertyTemplate.ReferenceTitle
                        }
                    },
                    Seed = new Seed
                    {
                        SeedType = SeedType.EnsureAllUpdated,
                        JsonData = JsonConvert.SerializeObject(new []
                        {
                            new {
                                Id = new ObjectId("5ca8770a4a73264e4c06e002"),
                                Title = "Reference"
                            },
                            new {
                                Id = new ObjectId("5ca8770a4a73264e4c06e003"),
                                Title = "Home"
                            }
                        })
                    }
                },
                #endregion
                #region Seed Type Entity
                new Entity()
                {
                    Id = new ObjectId("5ca876fa4a73264e4c06df50"),
                    InternalName = "SeedType",
                    Title = "Seed Type",
                    EntityTemplate = Request.EntityTemplate.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca8770a4a73264e4c06e004"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca8770a4a73264e4c06e005"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            PropertyTemplate = PropertyTemplate.ReferenceTitle
                        }
                    },
                    Seed = new Seed
                    {
                        SeedType = SeedType.EnsureAllUpdated,
                        JsonData = JsonConvert.SerializeObject(new []
                        {
                            new {
                                Id = new ObjectId("5ca8770b4a73264e4c06e006"),
                                Title = "Add if none"
                            },
                            new {
                                Id = new ObjectId("5ca8770b4a73264e4c06e007"),
                                Title = "Ensure All Added"
                            },
                            new {
                                Id = new ObjectId("5ca8770b4a73264e4c06e008"),
                                Title = "Ensure All Updated"
                            }
                        })
                    }
                },
                #endregion
                #region Entity Template Entity
                new Entity()
                {
                    Id = new ObjectId("5ca876f24a73264e4c06def8"),
                    InternalName = "EntityTemplate",
                    Title = "Entity Template",
                    EntityTemplate = Request.EntityTemplate.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca8770b4a73264e4c06e009"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca8770b4a73264e4c06e00a"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            PropertyTemplate = PropertyTemplate.ReferenceTitle
                        }
                    },
                    Seed = new Seed
                    {
                        SeedType = SeedType.EnsureAllUpdated,
                        JsonData = JsonConvert.SerializeObject(new []
                        {
                            new {
                                Id = new ObjectId("5ca86a296668b25914b67e6f"),
                                Title = "Reference"
                            }
                        })
                    }
                },
#endregion
                #region Service Type Entity
                new Entity()
                {
                    Id = new ObjectId("5ca876fb4a73264e4c06df58"),
                    InternalName = "ServiceType",
                    Title = "Service Type",
                    EntityTemplate = Request.EntityTemplate.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca8770b4a73264e4c06e00b"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca8770b4a73264e4c06e00c"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            PropertyTemplate = PropertyTemplate.ReferenceTitle
                        }
                    },
                    Seed = new Seed
                    {
                        SeedType = SeedType.EnsureAllUpdated,
                        JsonData = JsonConvert.SerializeObject(new []
                        {
                            new {
                                Id = new ObjectId("5ca8770b4a73264e4c06e00d"),
                                Title = "Web Service"
                            },
                            new
                            {
                                Id = new ObjectId("5ca8770b4a73264e4c06e00e"),
                                Title = "Export"
                            }
                        })
                    }
                },
#endregion
                #region Http Verb
                new Entity()
                {
                    Id = new ObjectId("5ca8770b4a73264e4c06e00f"),
                    InternalName = "HttpVerb",
                    Title = "Http Verb",
                    EntityTemplate = Request.EntityTemplate.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca8770b4a73264e4c06e010"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca8770c4a73264e4c06e011"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            PropertyTemplate = PropertyTemplate.ReferenceTitle
                        }
                    },
                    Seed = new Seed
                    {
                        SeedType = SeedType.EnsureAllUpdated,
                        JsonData = JsonConvert.SerializeObject(new []
                        {
                            new {
                                Id = new ObjectId("5ca8770c4a73264e4c06e012"),
                                Title = "Get"
                            },
                            new
                            {
                                Id = new ObjectId("5ca8770c4a73264e4c06e013"),
                                Title = "Post"
                            },
                            new
                            {
                                Id = new ObjectId("5ca8770c4a73264e4c06e014"),
                                Title = "Put"
                            },
                            new
                            {
                                Id = new ObjectId("5ca8770c4a73264e4c06e015"),
                                Title = "Delete"
                            }
                        })
                    }
                }
#endregion
            };
        }

        private IEnumerable<Entity> DefaultEntities()
        {
            return new Entity[]
            {
                new Entity("User")
                {
                    Id = new ObjectId("5ca8770c4a73264e4c06e016"),
                    Title = "User",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca8770c4a73264e4c06e017"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca8770c4a73264e4c06e018"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId(""),
                                    ValidationType = ValidationType.Required
                                }
                            }
                        }

                    }
                },
                new Entity("UserRole")
                {
                    Id = new ObjectId("5ca8770c4a73264e4c06e019"),
                    Title = "User Role",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca8770c4a73264e4c06e01a"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca8770c4a73264e4c06e01b"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title"
                        }

                    }
                },
                new Entity("Role")
                {
                    Id = new ObjectId("5ca8770d4a73264e4c06e01c"),
                    Title = "Role",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca8770d4a73264e4c06e01d"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca8770d4a73264e4c06e01e"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new ObjectId("5ca8770d4a73264e4c06e01f"),
                                    ValidationType = ValidationType.Required
                                }
                            }
                        }

                    }
                },
                new Entity("Audit")
                {
                    Id = new ObjectId("5ca8770d4a73264e4c06e020"),
                    Title = "Audit",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca8770d4a73264e4c06e021"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca8770d4a73264e4c06e022"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title"
                        },
                        new Property("Action")
                        {
                            Id = new ObjectId("5ca8770d4a73264e4c06e023"),
                            Title = "Action",
                            PropertyType = PropertyType.ReferenceRelationship,
                            ReferenceEntityId = new ObjectId("5ca8770d4a73264e4c06e024")
                        }
                    }
                },
                new Entity("Action")
                {
                    Id = new ObjectId("5ca8770d4a73264e4c06e024"),
                    Title = "Action",
                    EntityTemplate = Request.EntityTemplate.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new ObjectId("5ca8770d4a73264e4c06e025"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new ObjectId("5ca8770d4a73264e4c06e026"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            PropertyTemplate = PropertyTemplate.ReferenceTitle
                        }
                    },
                    Seed = new Seed
                    {
                        SeedType = SeedType.EnsureAllUpdated,
                        JsonData = JsonConvert.SerializeObject(new []
                        {
                            new {
                                Id = new ObjectId("5ca8770d4a73264e4c06e027"),
                                Title = "Insert"
                            },
                            new
                            {
                                Id = new ObjectId("5ca8770e4a73264e4c06e028"),
                                Title = "Update"
                            },
                            new
                            {
                                Id = new ObjectId("5ca8770e4a73264e4c06e029"),
                                Title = "Delete"
                            }
                        })
                    }
                }
            };
        }
    }
}
