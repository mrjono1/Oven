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
                        Id = new Guid("{EAC32A99-BEAF-4DE0-AA00-8B9B1093E8F6}"),
                        Title = "Role",
                        Properties = new Property[]
                        {
                            new Property
                            {
                                Id = new Guid("{0F990335-4732-4218-BC0F-9657FC9E7F8D}"),
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
                        Id = new Guid("{53AB894D-3289-4729-A730-276C7523F40D}"),
                        Title = "Group Role",
                        Properties = new Property[]
                        {
                            new Property
                            {
                                Id = new Guid("{DF39C127-C64B-4AF6-9EC8-510BF48B5126}"),
                                Title = "Role",
                                InternalName = "Role",
                                PropertyType = PropertyType.ParentRelationshipOneToMany,
                                ReferenceEntityId = new Guid("{EAC32A99-BEAF-4DE0-AA00-8B9B1093E8F6}"),
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
                                Id = new Guid("{0D3A0625-FC16-414E-A4FC-6892D59DD92F}"),
                                Title = "Group",
                                InternalName = "Group",
                                PropertyType = PropertyType.ParentRelationshipOneToMany,
                                ReferenceEntityId = new Guid("{2C57831F-B4D4-474F-A094-DA3D307D18CF}"),
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
                        Id = new Guid("{2C57831F-B4D4-474F-A094-DA3D307D18CF}"),
                        Title = "Group",
                        Properties = new Property[]
                        {
                            new Property
                            {
                                Id = new Guid("{DA0AFAD5-72FE-4C90-9444-4FF2896015DB}"),
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
                        Id = new Guid("{8CF3342E-1775-4E21-9B61-1101A0B7FA2A}"),
                        Title = "User Group",
                        Properties = new Property[]
                        {
                            new Property
                            {
                                Id = new Guid("{0D3A0625-FC16-414E-A4FC-6892D59DD92F}"),
                                Title = "Group",
                                InternalName = "Group",
                                PropertyType = PropertyType.ParentRelationshipOneToMany,
                                ReferenceEntityId = new Guid("{2C57831F-B4D4-474F-A094-DA3D307D18CF}"),
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
                                Id = new Guid("{63ED25A3-A746-4905-AAEF-6C8AF1C6CB6E}"),
                                Title = "User",
                                InternalName = "User",
                                PropertyType = PropertyType.ParentRelationshipOneToMany,
                                ReferenceEntityId = new Guid("{0206FF03-BD07-41D7-977D-0472EEBC8908}"),
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
                        Id = new Guid("{0206FF03-BD07-41D7-977D-0472EEBC8908}"),
                        Title = "User",
                        InternalName = "User",
                        Properties = new Property[]
                        {
                            new Property
                            {
                                Id = new Guid("{2DDB7C83-D511-4D8E-B1EC-54BD314D1DC9}"),
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
                        Id = new Guid("{CF87A2EF-FB2E-4197-98C9-6FBC41F959E3}"),
                        Title = "Log",
                        Properties = new Property[]
                        {
                            new Property
                            {
                                Id = new Guid("{F04BE588-11D6-4B5B-BEFE-00973541BAA7}"),
                                Title = "Title",
                                InternalName = "Title",
                                PropertyType = PropertyType.String
                            }
                        }
                    },
                    new Entity("Audit")
                    {
                        Id = new Guid("{66967E0D-EC96-41C7-A08C-C54059A31042}"),
                        Title = "Audit",
                        Properties = new Property[]
                        {
                            new Property("Id")
                            {
                                Id = new Guid("{EC9C88E4-5E24-43F7-9581-1485B78074AC}"),
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
