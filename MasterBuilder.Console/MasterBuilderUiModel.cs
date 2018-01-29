using MasterBuilder.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MasterBuilder
{
    public class MasterBuilderUiModel
    {
        public Project Project { get; set; }
        public MasterBuilderUiModel()
        {
            var project = new Project
            {
                Id = new Guid("{D1CB7777-6E61-486B-B15E-05B97B57D0FC}"),
                InternalName = "MasterBuilderUi",
                BuildVersion = 1,
                Title = "Master Builder",
                DefaultScreenId = new Guid("{C59B48E0-73B1-4393-8D6E-64CFE06304B2}")
            };
            project.Entities = new Entity[]
            {
                #region Project Entity
                new Entity()
                {
                    Id = new Guid("{89920EA4-9099-487A-AEBB-390E401FEC26}"),
                    InternalName = "Project",
                    Title = "Project",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{C3E14B66-FF43-478A-95D0-39524F6555B5}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{CB6D802C-1F26-4D23-9272-6396E6268D72}"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{8D5A78EB-B24D-4789-A498-1D37B57BF63D}"),
                                    ValidationType = ValidationType.Unique
                                },
                                new Validation
                                {
                                    Id = new Guid("{CDE9AA00-9880-4689-993A-3C6C37D7FC0E}"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 100
                                },
                                new Validation{
                                    Id = new Guid("{5657C193-061A-430E-BB29-183A510DD92E}"),
                                    ValidationType = ValidationType.Required
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{883DB867-2098-4CA0-AE63-87DE09FDEF76}"),
                            InternalName = "InternalName",
                            PropertyType = PropertyType.String,
                            Title = "Internal Name",
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{F60F66AC-F6AD-45E2-9850-9A474CC43C4E}"),
                                    ValidationType = ValidationType.Unique
                                },
                                new Validation
                                {
                                    Id = new Guid("{FF3EF7FD-7A8F-45A5-ACB0-C0A2F97C816B}"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 100
                                },
                                new Validation
                                {
                                    Id = new Guid("{2BE86AB2-5542-4E67-AF78-17E7A95B7B4E}"),
                                    ValidationType = ValidationType.Required
                                },
                                new Validation{
                                    Id = new Guid("{E1FA289D-B336-4C79-983E-77A1D733B078}"),
                                    ValidationType = ValidationType.Pattern,
                                    StringValue = "^[a-zA-Z]+$"
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{F1EEF37B-F40D-44D4-832F-ACEC4B63D147}"),
                            InternalName = "MajorVersion",
                            PropertyType = PropertyType.Integer,
                            Title = "Major Version",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{09ED3A71-3F3A-46BB-B24B-D2796E796A39}"),
                                    ValidationType = ValidationType.Required
                                }
                            },
                            DefaultIntegerValue = 1
                        },
                        new Property()
                        {
                            Id = new Guid("{97E822CB-7A95-4C14-BED4-6CE19602FEE8}"),
                            InternalName = "MinorVersion",
                            PropertyType = PropertyType.Integer,
                            Title = "Minor Version",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{18954CB6-7C4B-42E7-AF4D-F242461D16C2}"),
                                    ValidationType = ValidationType.Required
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{D7E750B0-159D-464C-A88C-6E67673CFAF2}"),
                            InternalName = "BuildVersion",
                            PropertyType = PropertyType.Integer,
                            Title = "Build Version",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{B3784D79-72C0-4DC6-86C3-4ECD256821D1}"),
                                    ValidationType = ValidationType.Required
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{0FBD3AA3-E6DD-4CCB-9FAA-E8AA8C8009F8}"),
                            InternalName = "CreatedDate",
                            PropertyType = PropertyType.DateTime,
                            Title = "Created Date",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{E9625AE5-85A7-42FF-95D1-D39265F35628}"),
                                    ValidationType = ValidationType.Required
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{6C22EF44-E5C0-4E3F-B315-A03703AB9D97}"),
                            InternalName = "DefaultScreen",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Default Screen",
                            ParentEntityId = new Guid("{604D9354-FAA6-4EC1-AC50-02DA79BD4526}")
                        }
                    }
                },
                #endregion
                #region Entity Entity
                new Entity()
                {
                    Id = new Guid("{149F1936-1EE1-481F-9038-A6B766B85BF3}"),
                    InternalName = "Entity",
                    Title = "Entity",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{4B5077DA-1516-44C4-81CC-D0CE25BBBCF0}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{6D09DE04-2FDA-4091-A4DA-B3448ABA1A52}"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{CDE9AA00-9880-4689-993A-3C6C37D7FC0E}"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 100
                                },
                                new Validation{
                                    Id = new Guid("{5657C193-061A-430E-BB29-183A510DD92E}"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{1CB8E873-07C7-461A-8859-672CE66C8513}"),
                            InternalName = "InternalName",
                            PropertyType = PropertyType.String,
                            Title = "Internal Name",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{FF3EF7FD-7A8F-45A5-ACB0-C0A2F97C816B}"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 100
                                },
                                new Validation{
                                    Id = new Guid("{2BE86AB2-5542-4E67-AF78-17E7A95B7B4E}"),
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
                            Id = new Guid("{E6F6876A-80C3-4856-8489-30FBEB260AA2}"),
                            InternalName = "EntityTemplate",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Entity Template",
                            ParentEntityId = new Guid("{E20337EA-37F3-48D1-96F7-3CF2A40A7F52}")
                        },
                        new Property()
                        {
                            Id = new Guid("{E6C4C4D9-A3E8-45B6-8B71-F33E6E159483}"),
                            InternalName = "Project",
                            PropertyType = PropertyType.ParentRelationship,
                            Title = "Project",
                            ParentEntityId = new Guid("{89920EA4-9099-487A-AEBB-390E401FEC26}"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{5657C193-061A-430E-BB29-183A510DD92E}"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{E6F6876A-80C3-4856-8489-30FBEB260AA2}"),
                            InternalName = "Seed",
                            PropertyType = PropertyType.OneToOneRelationship,
                            Title = "Seed",
                            ParentEntityId = new Guid("{84F8D049-967E-4BF0-BC6B-9D73151FAA84}")
                        },
                    }
                },
                #endregion
                #region Property Entity
                new Entity()
                {
                    Id = new Guid("{DE9790AD-6FC3-4CE3-B63B-EEAA1DF7CFCB}"),
                    InternalName = "Property",
                    Title = "Property",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{D34BF79B-F052-4090-BBDD-8FAE69A256C4}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{863F7481-3190-42AF-879C-53535BD468E6}"),
                            InternalName = "Entity",
                            PropertyType = PropertyType.ParentRelationship,
                            Title = "Entity",
                            ParentEntityId = new Guid("{149F1936-1EE1-481F-9038-A6B766B85BF3}"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{D69A337F-7171-4CAE-9E9D-492FE9578D89}"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{6F7F0BBE-B6E2-4766-BA5D-2A9F6540D4E0}"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{ACB68BF7-5842-42C7-B77A-88AC71CA22B1}"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new Guid("{1EACE07B-FD49-4B18-AA3F-36DF87D63B6E}"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{C52F7B8E-CAD0-40FF-8E89-B313A290A96E}"),
                            InternalName = "InternalName",
                            PropertyType = PropertyType.String,
                            Title = "Internal Name",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{74789541-1939-4F0F-9098-EBDF0AE70A1C}"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new Guid("{035339EF-C09F-4412-8779-BD2088387757}"),
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
                            Id = new Guid("{81D18B38-6B6B-4A0F-932E-7EB55F6E44E8}"),
                            InternalName = "PropertyType",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Property Type",
                            ParentEntityId = new Guid("{0B543B54-60AB-4FEA-BBD7-320AD50F3A06}"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{674C76ED-B47D-425B-BF6D-9C606FE58217}"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{53AD68E2-5C5C-405C-8407-67EA58862B0D}"),
                            InternalName = "PropertyTemplate",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Property Template",
                            ParentEntityId = new Guid("{08A8E760-8620-44A9-9A15-646B6A53C881}")
                        },
                        new Property()
                        {
                            Id = new Guid("{A84BD0B2-656D-4301-A722-7FE3ABC837C5}"),
                            InternalName = "ParentEntity",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "ParentEntity",
                            ParentEntityId = new Guid("{149F1936-1EE1-481F-9038-A6B766B85BF3}")
                        },
                        new Property()
                        {
                            Id = new Guid("{62FE9985-98A3-4D40-BC68-8AF641FDABFC}"),
                            InternalName = "DefaultIntegerValue",
                            PropertyType = PropertyType.Integer,
                            Title = "Default Integer Value"
                        },
                        new Property()
                        {
                            Id = new Guid("{61862CC1-DFD4-4578-BF6E-FFA195D6F6A1}"),
                            InternalName = "DefaultDoubleValue",
                            PropertyType = PropertyType.Double,
                            Title = "Default Double Value"
                        },
                        new Property()
                        {
                            Id = new Guid("{63935FCA-CC40-4A78-A21C-F76FD761701B}"),
                            InternalName = "DefaultStringValue",
                            PropertyType = PropertyType.String,
                            Title = "Default String Value"
                        },
                        new Property()
                        {
                            Id = new Guid("{836DA351-CCCF-4269-89CA-98E7CD67E77E}"),
                            InternalName = "DefaultBooleanValue",
                            PropertyType = PropertyType.Boolean,
                            Title = "Default Boolean Value",
                            DefaultBooleanValue = false
                        }
                    }
                },
                #endregion
                #region Validation Entity
                new Entity()
                {
                    Id = new Guid("{FF2103AE-FE34-410C-BC03-042AF7449D2D}"),
                    InternalName = "Validation",
                    Title = "Validation",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{B9E9AC32-5942-4DFF-8AFD-DEDC26795824}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{3690A6AE-0573-40F3-8680-0BCE13931EE3}"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{D7C58A33-816E-45D7-8E8A-E7FCC44C270E}"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new Guid("{07CC8E0F-E69A-4B93-892A-2795B8E34C48}"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{BA0702F8-6F26-4A25-9300-31B44F14B3A8}"),
                            InternalName = "Property",
                            PropertyType = PropertyType.ParentRelationship,
                            Title = "Property",
                            ParentEntityId = new Guid("{DE9790AD-6FC3-4CE3-B63B-EEAA1DF7CFCB}"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{FC73C9C5-953A-414F-9C20-4B5C57F4F709}"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{89E340FC-A7BB-44D4-A783-45801941B752}"),
                            InternalName = "ValidationType",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Validation Type",
                            ParentEntityId = new Guid("{91104448-B314-41C3-8573-2BDF7CCBB701}"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{707BD4C0-7ED4-4865-9168-176B4E843E72}"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{37E3DBC5-1B98-446D-B46B-1BD530CAD376}"),
                            InternalName = "IntegerValue",
                            PropertyType = PropertyType.Integer,
                            Title = "Integer Value"
                        },
                        new Property()
                        {
                            Id = new Guid("{8E35139C-E60C-4432-B1F7-62CEA1451593}"),
                            InternalName = "DoubleValue",
                            PropertyType = PropertyType.Double,
                            Title = "Double Value"
                        },
                        new Property()
                        {
                            Id = new Guid("{E2F53C95-BA38-47E1-93CD-38F4B04F44D0}"),
                            InternalName = "StringValue",
                            PropertyType = PropertyType.String,
                            Title = "String Value"
                        },
                        new Property()
                        {
                            Id = new Guid("{640B7178-5C6D-4265-9ADE-EDC606FC3362}"),
                            InternalName = "Message",
                            PropertyType = PropertyType.String,
                            Title = "Message"
                        },
                    }
                },
                #endregion

                new Entity()
                {
                    Id = new Guid("{D4F9AFCF-66A4-4E23-9C13-4F5873B51FDC}"),
                    InternalName = "MenuItem",
                    Title = "Menu Item",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{3A849E52-28FA-4CAF-908D-BD2D65C0FAFA}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{51B87800-E2AC-48E6-917F-36B9E46F209E}"),
                            InternalName = "Project",
                            PropertyType = PropertyType.ParentRelationship,
                            Title = "Project",
                            ParentEntityId = new Guid("{89920EA4-9099-487A-AEBB-390E401FEC26}"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{768E1FAE-3617-4641-AC0F-B8AB3A52BF85}"),
                                    ValidationType = ValidationType.Required
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{72267B8D-5004-4065-B4DC-882706185284}"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{D049B0D9-B488-4F18-A832-8549348FDA56}"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new Guid("{6D39BDE7-2E3C-4A1A-82D6-FD7750245B67}"),
                                    ValidationType = ValidationType.Required
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{62ECF628-B217-4800-A8AF-AC3E62F01203}"),
                            InternalName = "MenuItemType",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Type",
                            ParentEntityId = new Guid("{092F60B1-EE1E-4451-A771-013376C93E65}")
                        },
                        new Property()
                        {
                            Id = new Guid("{94202D7E-D704-4C89-B4B1-AB720C976762}"),
                            InternalName = "Screen",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Screen",
                            ParentEntityId = new Guid("{604D9354-FAA6-4EC1-AC50-02DA79BD4526}")
                        },
                    }
                },
                #region Screen Entity
                new Entity()
                {
                    Id = new Guid("{604D9354-FAA6-4EC1-AC50-02DA79BD4526}"),
                    InternalName = "Screen",
                    Title = "Screen",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{CCDFE624-33F8-4B38-9871-E051A301B73B}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{5AF2A706-9A35-4E99-9492-BCCE8A9CCBCD}"),
                            InternalName = "Project",
                            PropertyType = PropertyType.ParentRelationship,
                            Title = "Project",
                            ParentEntityId = new Guid("{89920EA4-9099-487A-AEBB-390E401FEC26}"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{253608F3-0E63-4F67-B7C4-940D2472D3F7}"),
                                    ValidationType = ValidationType.Required
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{34B85ED2-3B10-4974-9451-7B87203F1E0F}"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{5E707736-477F-46FF-977C-3030A2FC45EC}"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new Guid("{5F6980CE-0E4B-4FCB-AC99-228FD14CDB0E}"),
                                    ValidationType = ValidationType.Required
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{B2F3E579-DEA2-4E82-A69D-F7CD247E1765}"),
                            InternalName = "Path",
                            PropertyType = PropertyType.String,
                            Title = "Path/Slug"
                        },
                        new Property()
                        {
                            Id = new Guid("{A02115B5-4E6A-4D7B-AB80-9E58516B1E3A}"),
                            InternalName = "Entity",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Entity",
                            ParentEntityId = new Guid("{149F1936-1EE1-481F-9038-A6B766B85BF3}")
                        },
                        new Property()
                        {
                            Id = new Guid("{92AD26A8-FECE-4AE0-809B-8CBE42AA7E6A}"),
                            InternalName = "Template",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Template",
                            ParentEntityId = new Guid("{564E3EC4-59E0-4EEB-A9E5-F1367E1FEB29}")
                        },
                        new Property()
                        {
                            Id = new Guid("{84614E32-DD65-4CE5-9FE2-1AA4FD6A0C38}"),
                            InternalName = "ScreenType",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Screen Type",
                            ParentEntityId = new Guid("{C04282DB-CB85-445D-BB4B-AEBB3801DAC7}")
                        },
                        new Property()
                        {
                            Id = new Guid("{B9A65C24-AE1B-41E6-8E84-973908051DE2}"),
                            InternalName = "NavigateToScreen",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Navigate to screen",
                            ParentEntityId = new Guid("{604D9354-FAA6-4EC1-AC50-02DA79BD4526}")
                        }
                    }
                },
#endregion
                new Entity()
                {
                    Id = new Guid("{F6C0ADDF-CBE3-48F1-8B9E-51C91EEC534F}"),
                    InternalName = "ScreenFeature",
                    Title = "Screen Feature",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{2D145AAC-C180-462E-8837-FD769BE7E5FE}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                    }
                },
                #region Screen Section Entity
                new Entity()
                {
                    Id = new Guid("{1379E266-2600-426F-AEBB-790D008A46AB}"),
                    InternalName = "ScreenSection",
                    Title = "Screen Section",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{0727AAE6-EA99-4456-95C1-07E9115A54EC}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{61E1AAE0-315E-4BBB-B622-0E3D91B9959A}"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{E95C3F08-F572-42BB-9FD9-F7BD2663D514}"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new Guid("{A83F2125-A810-43CC-8FFD-C6E91BF3DD13}"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{17FAC43C-9ED4-4D2B-ADA6-B59DA4D10C61}"),
                            InternalName = "InternalName",
                            PropertyType = PropertyType.String,
                            Title = "Internal Name",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{D377A570-990A-471D-B3BE-F6D46C590210}"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new Guid("{D5C76B55-96B8-41DD-9055-485A8614E34F}"),
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
                            Id = new Guid("{C3CE72B7-DE38-4CEE-B4BD-C950308D261B}"),
                            InternalName = "Screen",
                            PropertyType = PropertyType.ParentRelationship,
                            Title = "Screen",
                            ParentEntityId = new Guid("{604D9354-FAA6-4EC1-AC50-02DA79BD4526}"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{78C27422-D703-404C-8446-F46E655EAF01}"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{3EDDA39A-A700-486D-ABD8-7D9E14C6F550}"),
                            InternalName = "NavigateToScreen",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Navigate to screen",
                            ParentEntityId = new Guid("{604D9354-FAA6-4EC1-AC50-02DA79BD4526}")
                        },
                        new Property()
                        {
                            Id = new Guid("{2A1E69D0-46F8-43BB-8ECB-80067D70C24C}"),
                            InternalName = "ScreenSectionType",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Screen Section Type",
                            ParentEntityId = new Guid("{6B3442DE-02EA-4A89-BBC9-3C7E698C94EF}"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{83F70DAD-02EB-42CB-8002-6671E33A0B49}"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{D6BB1A68-8C2A-4251-8EA0-B3AC6C9362AD}"),
                            InternalName = "Entity",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Entity",
                            ParentEntityId = new Guid("{149F1936-1EE1-481F-9038-A6B766B85BF3}"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{872BB43B-C432-4EFA-AABE-4908E96EA1FB}"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{B92AFD77-FFAC-4F87-B7A7-CDFA98AFD59E}"),
                            InternalName = "Html",
                            PropertyType = PropertyType.String,
                            Title = "Html"
                        },
                        new Property()
                        {
                            Id = new Guid("{381E1C63-004D-4AAB-B41D-86D62D39A7F9}"),
                            InternalName = "FormSection",
                            PropertyType = PropertyType.OneToOneRelationship,
                            Title = "Form Section",
                            ParentEntityId = new Guid("{114176DC-3440-4E3A-A929-4A243A188B4F}")
                        },
                        new Property()
                        {
                            Id = new Guid("{4923CC53-3325-4070-BEE5-B8B07CBDC751}"),
                            InternalName = "SearchSection",
                            PropertyType = PropertyType.OneToOneRelationship,
                            Title = "Search Section",
                            ParentEntityId = new Guid("{64903354-DB1A-46F5-AD6A-30973F4CA30D}")
                        }
                    }
                },
                #endregion
                #region Seed Entity
                new Entity()
                {
                    Id = new Guid("{84F8D049-967E-4BF0-BC6B-9D73151FAA84}"),
                    InternalName = "Seed",
                    Title = "Seed",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{AC0AF908-F0A7-44B4-AFBB-DCB8CEABD782}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{503A6F65-87A0-453A-B0CC-880F171D65F8}"),
                            InternalName = "JsonData",
                            PropertyType = PropertyType.String,
                            Title = "Json Data",
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{3BC3CC2D-42D1-4CCD-BD94-22CFD28078FC}"),
                                    ValidationType = ValidationType.Required
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{445099D8-864E-447F-A60C-13F501A02C46}"),
                            InternalName = "SeedType",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Seed Type",
                            ParentEntityId = new Guid("{EA6A9786-573A-4821-824C-3FB5322D2A51}"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{1CDD43FA-D63E-441A-A4FB-6B25A47D30AE}"),
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
                    Id = new Guid("{0D30E5F1-7C29-4BB6-9C7E-39BC51884684}"),
                    InternalName = "Service",
                    Title = "Service",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{19152CE0-B2AC-4267-A5E1-D06B1D7DFC01}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{6E11DECD-0526-4BC1-91C8-A7F1AA338635}"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{7688B9DF-576E-40C5-B752-D0F31A5660F7}"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new Guid("{AAD3E008-8EAD-4A22-84E8-E15A9157603C}"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{01C880DB-2D97-425A-905D-D7CC571A4709}"),
                            InternalName = "ServiceType",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Service Type",
                            ParentEntityId = new Guid("{65D65D11-D1F0-421E-811E-A8CBB84CBB0D}"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{D6D616EB-E922-4782-BC9D-D68F1F07DB60}"),
                            InternalName = "Project",
                            PropertyType = PropertyType.ParentRelationship,
                            Title = "Project",
                            ParentEntityId = new Guid("{89920EA4-9099-487A-AEBB-390E401FEC26}"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{1EF431AF-E3D0-4510-93E0-58FD49D602D0}"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{ACD4EA5B-644F-4DFD-9082-A0E58402B11D}"),
                            InternalName = "WebService",
                            PropertyType = PropertyType.OneToOneRelationship,
                            Title = "WebService",
                            ParentEntityId = new Guid("{BBC97BD0-9FF6-4FF0-95E4-979B91F61B9D}")
                        },
                        new Property()
                        {
                            Id = new Guid("{3E70E9E0-707D-4441-B4F0-199B70B31A64}"),
                            InternalName = "ExportService",
                            PropertyType = PropertyType.OneToOneRelationship,
                            Title = "Export Service",
                            ParentEntityId = new Guid("{2B77156B-E550-4E46-8472-FAFA3D04966E}")
                        }
                    }
                },
#endregion
                #region Export Service Entity
                new Entity()
                {
                    Id = new Guid("{2B77156B-E550-4E46-8472-FAFA3D04966E}"),
                    InternalName = "ExportService",
                    Title = "Export Service",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{4B576F42-685D-4683-B48A-5B6FF5E450CA}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{4430E6C1-70EA-41E9-AA58-67176D9310E4}"),
                            InternalName = "Entity",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Root Entity",
                            ParentEntityId = new Guid("{149F1936-1EE1-481F-9038-A6B766B85BF3}"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{FB010C63-85C0-445C-8880-FF989F908F33}"),
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
                    Id = new Guid("{BBC97BD0-9FF6-4FF0-95E4-979B91F61B9D}"),
                    InternalName = "WebService",
                    Title = "Web Service",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{A13AA1B4-127A-4316-B05B-BFFCCB8B7BF6}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{CB83CA7F-B62D-48E4-9833-54B1B07A7E70}"),
                            InternalName = "DefaultBaseEndpoint",
                            PropertyType = PropertyType.String,
                            Title = "Default Base Endpoint",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{41363BA5-4AE5-4EAE-9608-1398836D23F1}"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new Guid("{66B86E99-9AA5-4890-81BA-1FBF9CADBCC6}"),
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
                    Id = new Guid("{9878D52D-9962-4131-BE6C-1CED4004C8C4}"),
                    InternalName = "WebServiceOperation",
                    Title = "Web Service Operation",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{B9CCBA2A-7AD2-42F0-9F86-37C8D086EA1D}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{9FB3FA12-C979-4015-847C-EB8E52C8FBD0}"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{A710F453-B259-42AB-8A2A-BAF31144C1AF}"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new Guid("{00D2823B-ABB9-4912-9712-434EB63B92A5}"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{EE1E832A-A9A1-4152-B7B5-A4D23914F6A7}"),
                            InternalName = "RelativeRoute",
                            PropertyType = PropertyType.String,
                            Title = "Relative Route",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{56755225-7577-45E1-8AC0-E97A2654F355}"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                }
                            }
                        },
                        // TODO: Convert verb to reference
                        new Property()
                        {
                            Id = new Guid("{E32BF54A-E604-4E97-81B9-CECF9BA5D32D}"),
                            InternalName = "Verb",
                            PropertyType = PropertyType.String,
                            Title = "Verb",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{15722367-9BD8-4E35-B9F0-7DA5C3182452}"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{A619FE2D-DEDA-430F-BBF4-E1F7A919F95F}"),
                            InternalName = "WebService",
                            PropertyType = PropertyType.ParentRelationship,
                            Title = "Web Service",
                            ParentEntityId = new Guid("{BBC97BD0-9FF6-4FF0-95E4-979B91F61B9D}"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{80F8E11E-925F-4A0A-83BD-9232EF939916}"),
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
                    Id = new Guid("{8D68FDDE-6621-472B-9F2C-04ADE443E51C}"),
                    InternalName = "FormField",
                    Title = "Form Field",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{941173BD-D4B0-4216-8CCE-DC3DF81456A5}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{218A6A13-5A20-49D0-8770-ECD570D94599}"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{35982768-3C3A-41E1-9080-F25218529A5B}"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{FB5EC954-8935-48B2-BCEB-FCE643098C63}"),
                            InternalName = "FormSection",
                            PropertyType = PropertyType.ParentRelationship,
                            Title = "Form Section",
                            ParentEntityId = new Guid("{114176DC-3440-4E3A-A929-4A243A188B4F}")
                        },
                        new Property()
                        {
                            Id = new Guid("{307F022C-B5ED-4F1F-8015-9DF223F95599}"),
                            InternalName = "EntityProperty",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Entity Property",
                            ParentEntityId = new Guid("{DE9790AD-6FC3-4CE3-B63B-EEAA1DF7CFCB}"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{42B9A2C7-DDB4-4B83-AFB1-0A282DC72839}"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        }
                    }
                },
#endregion
                #region Form Section Entity
                new Entity()
                {
                    Id = new Guid("{114176DC-3440-4E3A-A929-4A243A188B4F}"),
                    InternalName = "FormSection",
                    Title = "Form Section",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{3DEF9251-17DA-4BFA-B941-CA0E14C298B7}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        }
                        // At the moment it only has a collection of form Fields
                    }
                },
#endregion
                #region Search Section Entity
                new Entity()
                {
                    Id = new Guid("{64903354-DB1A-46F5-AD6A-30973F4CA30D}"),
                    InternalName = "SearchSection",
                    Title = "Search Section",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{621D9C10-93FB-4178-9670-28CEBABF5C7A}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        }
                        // At the moment it only has a collection of Search Columns
                    }
                },
#endregion
                #region Search Column Entity
                new Entity()
                {
                    Id = new Guid("{559E8929-2395-4BFD-AF37-DCA314368DA5}"),
                    InternalName = "SearchColumn",
                    Title = "Search Column",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{16650A1E-9786-482F-814E-331698DF389C}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{7334EF78-E75A-45A1-BFB6-122CDDC8281B}"),
                            InternalName = "Title",
                            PropertyType = PropertyType.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{A67BA3F9-3DB5-4A51-B5CF-F4CBD0125483}"),
                                    ValidationType = ValidationType.MaximumLength,
                                    IntegerValue = 200
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{483D2C5E-1C04-4C2E-8468-4753D3F88996}"),
                            InternalName = "Ordinal",
                            PropertyType = PropertyType.Integer,
                            Title = "Ordinal",
                            DefaultIntegerValue = 1,
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{A67BA3F9-3DB5-4A51-B5CF-F4CBD0125483}"),
                                    ValidationType = ValidationType.Required
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{DCC45498-8A8A-4A47-9414-B46E6EDFC921}"),
                            InternalName = "SearchSection",
                            PropertyType = PropertyType.ParentRelationship,
                            Title = "Search Section",
                            ParentEntityId = new Guid("{64903354-DB1A-46F5-AD6A-30973F4CA30D}")
                        },
                        new Property()
                        {
                            Id = new Guid("{8623E412-9601-4A64-9B99-A9439D725B37}"),
                            InternalName = "EntityProperty",
                            PropertyType = PropertyType.ReferenceRelationship,
                            Title = "Entity Property",
                            ParentEntityId = new Guid("{DE9790AD-6FC3-4CE3-B63B-EEAA1DF7CFCB}"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{C7DD9CDB-2A42-49F1-9EFC-EB2E78516518}"),
                                    ValidationType = ValidationType.Required
                                },
                            }
                        }
                    }
                },
#endregion
            };

            project.Screens = new Screen[]
            {
                #region Home Screen
                new Screen()
                {
                    Id = new Guid("{C59B48E0-73B1-4393-8D6E-64CFE06304B2}"),
                    ScreenType = ScreenType.Html,
                    Title = "Home",
                    Path = "home",
                    Template = ScreenTemplate.Home,
                    ScreenSections = new ScreenSection[]
                    {
                        new ScreenSection
                        {
                            Id = new Guid("{532605D4-DCD3-4B5B-AC63-7CC6E90F6792}"),
                            Title = "Home",
                            InternalName = "Home",
                            ScreenSectionType = ScreenSectionType.Html,
                            Html = @"
<p>Welcome to Master Builder an application builder that you configure and we handle the rest</p>
<ul>
    <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>
    <li><a href='https://angular.io/'>Angular</a> and <a href='http://www.typescriptlang.org/'>TypeScript</a> for client-side code</li>
    <li><a href='https://webpack.github.io/'>Webpack</a> for building and bundling client-side resources</li>
    <li><a href='https://material.angular.io/'>Angular Material</a></li>
</ul>"
                        }
                    }
                },
                #endregion
                #region Projects Screen
                new Screen()
                {
                    Id = new Guid("{EAA8BF91-1F76-473F-8A0D-AB3DF8BD4B93}"),
                    EntityId = new Guid("{89920EA4-9099-487A-AEBB-390E401FEC26}"),
                    ScreenType = ScreenType.Search,
                    Title = "Projects",
                    Path = "projects",
                    NavigateToScreenId = new Guid("{835D26D3-2349-4914-AB85-2195756A5DAA}"),
                    ScreenFeatures = new ScreenFeature[]
                    {
                        new ScreenFeature{
                            Id = new Guid("{00CAB28C-F1CA-4FDC-9184-AC7DDA7FD3C5}"),
                            Feature = Feature.New
                        }
                    }
                },
#endregion
                new Screen()
                {
                    Id = new Guid("{835D26D3-2349-4914-AB85-2195756A5DAA}"),
                    EntityId = new Guid("{89920EA4-9099-487A-AEBB-390E401FEC26}"),
                    ScreenType = ScreenType.Edit,
                    Title = "Project",
                    Path = "project",
                    ScreenSections = new ScreenSection[]
                    {
                        new ScreenSection
                        {
                            Id = new Guid("{BCB84DAB-B0AD-45B9-975D-5C07DE4F8990}"),
                            Title = "Project",
                            InternalName = "Project",
                            ScreenSectionType = ScreenSectionType.Form
                        },
                        new ScreenSection
                        {
                            Id = new Guid("{903FEB8F-DC3C-49AA-AB1F-BB5C82F20DC5}"),
                            Title = "Entities",
                            InternalName = "Entities",
                            ScreenSectionType = ScreenSectionType.Search,
                            EntityId = new Guid("{149F1936-1EE1-481F-9038-A6B766B85BF3}"),
                            NavigateToScreenId = new Guid("{B1CE9862-EA2F-4EBC-95FF-D6FB87F21EE7}"),
                            MenuItems = new MenuItem[]
                            {
                                new MenuItem
                                {
                                    Id = new Guid("{AE228BBC-8A38-4D7D-B3B0-7964A281B7A5}"),
                                    MenuItemType = MenuItemType.New,
                                    ScreenId = new Guid("{B1CE9862-EA2F-4EBC-95FF-D6FB87F21EE7}"),
                                    Title = "New"
                                }
                            }
                        },
                        new ScreenSection
                        {
                            Id = new Guid("{DC2C45E5-1965-4D44-8B66-0A72E45BB720}"),
                            Title = "Screens",
                            InternalName = "Screens",
                            ScreenSectionType = ScreenSectionType.Search,
                            EntityId = new Guid("{604D9354-FAA6-4EC1-AC50-02DA79BD4526}"),
                            NavigateToScreenId = new Guid("{1A844688-994A-4BEC-8AAC-8C498529E451}"),
                            MenuItems = new MenuItem[]
                            {
                                new MenuItem
                                {
                                    Id = new Guid("{1B08D6F7-28E7-4FBD-86A6-E1BAAE655585}"),
                                    MenuItemType = MenuItemType.New,
                                    ScreenId = new Guid("{1A844688-994A-4BEC-8AAC-8C498529E451}"),
                                    Title = "New"
                                }
                            }
                        },
                        new ScreenSection
                        {
                            Id = new Guid("{09D4859E-BAC7-4BEA-9F4C-14690AEE01F6}"),
                            Title = "Menu Items",
                            InternalName = "MenuItems",
                            ScreenSectionType = ScreenSectionType.Search,
                            EntityId = new Guid("{D4F9AFCF-66A4-4E23-9C13-4F5873B51FDC}"),
                            NavigateToScreenId = new Guid("{005F52E5-9AF5-4155-AAD2-82FF2E6AC5B1}"),
                            MenuItems = new MenuItem[]
                            {
                                new MenuItem
                                {
                                    Id = new Guid("{CE3A30D9-6091-4D01-BCBB-ACE9EAD5D3F5}"),
                                    MenuItemType = MenuItemType.New,
                                    ScreenId = new Guid("{005F52E5-9AF5-4155-AAD2-82FF2E6AC5B1}"),
                                    Title = "New"
                                }
                            }
                        }
                    },
                    MenuItems = new MenuItem[]
                    {
                        new MenuItem
                        {
                            Id = new Guid("{FCB3AFDB-5CEB-41E7-AE2B-6122ECCB966D}"),
                            Title = "Publish",
                            Icon = "glyphicon glyphicon-cloud-upload",
                            MenuItemType = MenuItemType.ServerFunction,
                            ServerCode = $@"var data = await _exportService.ExportProjectAsync(id);
            var result = await _masterBuilderApiService.PublishAsync(data);
            return Ok(result.Content);"
                        }
                    }
                },
                new Screen()
                {
                    Id = new Guid("{B1CE9862-EA2F-4EBC-95FF-D6FB87F21EE7}"),
                    EntityId = new Guid("{149F1936-1EE1-481F-9038-A6B766B85BF3}"),
                    ScreenType = ScreenType.Edit,
                    Title = "Entity",
                    Path = "entity",
                    ScreenSections = new ScreenSection[]
                    {
                        new ScreenSection
                        {
                            Id = new Guid("{8130C0F4-F8F7-4A7A-9793-85B8939589EB}"),
                            Title = "Entity",
                            InternalName = "Entity",
                            ScreenSectionType = ScreenSectionType.Form
                        },
                        new ScreenSection
                        {
                            Id = new Guid("{C2A19FAF-5C62-43D2-8FA4-2293B4B68569}"),
                            Title = "Properties",
                            InternalName = "Properties",
                            ScreenSectionTypeId = new Guid("{0637300C-B76E-45E2-926A-055BB335129F}"), // Search
                            EntityId = new Guid("{DE9790AD-6FC3-4CE3-B63B-EEAA1DF7CFCB}"),
                            NavigateToScreenId = new Guid("{064AB31A-E92A-4647-A517-2A1BAC54EE73}"),
                            MenuItems = new MenuItem[]
                            {
                                new MenuItem
                                {
                                    Id = new Guid("{C236C6DC-A21A-44DF-B125-FFA46E1810FC}"),
                                    MenuItemType = MenuItemType.New,
                                    ScreenId = new Guid("{064AB31A-E92A-4647-A517-2A1BAC54EE73}"),
                                    Title = "New"
                                }
                            }
                        }
                    }
                },
                new Screen()
                {
                    Id = new Guid("{1A844688-994A-4BEC-8AAC-8C498529E451}"),
                    EntityId =new Guid("{604D9354-FAA6-4EC1-AC50-02DA79BD4526}"),
                    ScreenType = ScreenType.Edit,
                    Title = "Screen",
                    Path = "screen",
                    ScreenSections = new ScreenSection[]
                    {
                        new ScreenSection
                        {
                            Id = new Guid("{94718B2B-01EA-41A1-95BD-88C76B55A0EC}"),
                            Title = "Screen",
                            InternalName = "Screen",
                            ScreenSectionType = ScreenSectionType.Form,
                            EntityId = new Guid("{604D9354-FAA6-4EC1-AC50-02DA79BD4526}")
                        },
                        new ScreenSection
                        {
                            Id = new Guid("{CDC681CF-6298-43D7-8A14-B360D12E287D}"),
                            Title = "Screen Sections",
                            InternalName = "ScreenSections",
                            ScreenSectionType = ScreenSectionType.Search,
                            EntityId = new Guid("{1379E266-2600-426F-AEBB-790D008A46AB}"),
                            NavigateToScreenId = new Guid("{2A87A77C-6A99-4B3E-BB61-2BEC93ECFC7B}"),
                            MenuItems = new MenuItem[]
                            {
                                new MenuItem
                                {
                                    Id = new Guid("{9F917C0A-D82E-40C7-AB5A-A81772AF8A70}"),
                                    MenuItemType = MenuItemType.New,
                                    ScreenId = new Guid("{2A87A77C-6A99-4B3E-BB61-2BEC93ECFC7B}"),
                                    Title = "New"
                                }
                            }
                        }
                    }
                },
                new Screen()
                {
                    Id = new Guid("{2A87A77C-6A99-4B3E-BB61-2BEC93ECFC7B}"),
                    EntityId =new Guid("{1379E266-2600-426F-AEBB-790D008A46AB}"),
                    ScreenType = ScreenType.Edit,
                    Title = "Screen Section",
                    Path = "screen-section",
                    ScreenSections = new ScreenSection[]
                    {
                        new ScreenSection
                        {
                            Id = new Guid("{94718B2B-01EA-41A1-95BD-88C76B55A0EC}"),
                            Title = "Screen Section",
                            InternalName = "ScreenSection",
                            ScreenSectionType = ScreenSectionType.Form,
                            EntityId = new Guid("{1379E266-2600-426F-AEBB-790D008A46AB}")
                        }
                    }
                },
                new Screen()
                {
                    Id = new Guid("{005F52E5-9AF5-4155-AAD2-82FF2E6AC5B1}"),
                    EntityId =new Guid("{D4F9AFCF-66A4-4E23-9C13-4F5873B51FDC}"),
                    ScreenType = ScreenType.Edit,
                    Title = "Menu Item",
                    Path = "menu-item",
                    ScreenSections = new ScreenSection[]
                    {
                        new ScreenSection
                        {
                            Id = new Guid("{DDD8CB45-FCDD-4C24-AEB5-2CBB80836AC2}"),
                            Title = "Menu Item",
                            InternalName = "MenuItem",
                            ScreenSectionType = ScreenSectionType.Form,
                            EntityId = new Guid("{D4F9AFCF-66A4-4E23-9C13-4F5873B51FDC}")
                        }
                    }
                },
                new Screen()
                {
                    Id = new Guid("{064AB31A-E92A-4647-A517-2A1BAC54EE73}"),
                    EntityId = new Guid("{DE9790AD-6FC3-4CE3-B63B-EEAA1DF7CFCB}"),
                    ScreenType = ScreenType.Edit,
                    Title = "Property",
                    Path = "property",
                    ScreenSections = new ScreenSection[]
                    {
                        new ScreenSection
                        {
                            Id = new Guid("{03934004-41EF-40A7-B317-723019001DCB}"),
                            Title = "Property",
                            InternalName = "Property",
                            ScreenSectionTypeId = new Guid("{DC1169A8-8F49-45E9-9969-B64BEF4D0F42}") // Form
                        }
                    }
                },
                new Screen()
                {
                    Id = new Guid("{8E1A9AB1-799E-46B4-8364-85FB087C643E}"),
                    EntityId = new Guid("{91104448-B314-41C3-8573-2BDF7CCBB701}"),
                    ScreenType = ScreenType.Search,
                    Title = "Validation Types",
                    Path = "validation-types",
                    NavigateToScreenId = new Guid("{5D9BA697-C64B-40EE-9DF4-F88BD683713F}"),
                    ScreenFeatures = new ScreenFeature[]
                    {
                        new ScreenFeature{
                            Id = new Guid("{DF498A46-08B7-450E-8CE4-3BCC1B711A0D}"),
                            FeatureId = new Guid("{6114120E-BD93-4CE4-A673-7DC295F93CFE}") // New
                        }
                    }
                },
                new Screen()
                {
                    Id = new Guid("{5D9BA697-C64B-40EE-9DF4-F88BD683713F}"),
                    EntityId = new Guid("{91104448-B314-41C3-8573-2BDF7CCBB701}"),
                    ScreenType = ScreenType.Edit,
                    Title = "Validation Type",
                    Path = "validation-type",
                    ScreenSections = new ScreenSection[]
                    {
                        new ScreenSection
                        {
                            Id = new Guid("{459E59DD-D382-4061-94EF-3E73B47BFCB2}"),
                            Title = "Validation Type",
                            InternalName = "ValidationType",
                            ScreenSectionTypeId = new Guid("{DC1169A8-8F49-45E9-9969-B64BEF4D0F42}") // Form
                        }
                    }
                },
            };

            project.MenuItems = new MenuItem[]
            {
                new MenuItem
                {
                    Title = "Projects",
                    ScreenId = new Guid("{EAA8BF91-1F76-473F-8A0D-AB3DF8BD4B93}"),
                    Icon = "glyphicon glyphicon-th-list"
                }
            };

            project.Services = new Service[]
            {
                new Service(){
                    Id = new Guid("{359525A8-CCA2-4AC1-9348-23057D616A75}"),
                    Title = "Master Builder Api",
                    ServiceType = ServiceType.WebService,
                    WebService = new WebService
                    {
                        DefaultBaseEndpoint = "https://localhost:44398",
                        Operations = new WebServiceOperation[]
                        {
                            new WebServiceOperation
                            {
                                Id = new Guid("{99B9358B-2266-47BC-957A-DC6EF459D4A1}"),
                                Title = "Publish",
                                Verb = HttpVerb.Post,
                                RelativeRoute = "/api/builder/publish"
                            }
                        }
                    }
                }
            };

            var entities = new List<Entity>(project.Entities);
            entities.AddRange(ReferenceEntities());
            project.Entities = entities;

            Project = project;
        }

        private IEnumerable<Entity> ReferenceEntities()
        {
            return new Entity[]
            {
                #region Validation Type Entity
                new Entity()
                {
                    Id = new Guid("{91104448-B314-41C3-8573-2BDF7CCBB701}"),
                    InternalName = "ValidationType",
                    Title = "Validation Type",
                    EntityTemplate = Request.EntityTemplate.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{03020776-6F7E-41CD-9475-6E2CA72E92B4}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{771B57F0-F3D8-4E12-AA93-C801447BDB83}"),
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
                                Id = new Guid("{17CC19D3-8E91-432E-98F7-4D9368DE3C44}"),
                                Title = "Email"
                            },
                            new {
                                Id = new Guid("{F7788E3D-7753-4491-98B1-AE78E16CDD0E}"),
                                Title = "Maximum Length"
                            },
                            new {
                                Id = new Guid("{0046F484-17EB-4665-AE59-45189BB203A9}"),
                                Title = "Maximum Value"
                            },
                            new {
                                Id = new Guid("{35D78EB6-F5DE-4E7B-AE79-B69A1D3DC7C9}"),
                                Title = "Minimum Length"
                            },
                            new {
                                Id = new Guid("{A679CB09-DE53-42F7-BB89-7E29947B51A1}"),
                                Title = "Minimum Value"
                            },
                            new {
                                Id = new Guid("{C0A88F1A-AAA8-47DA-A75B-94490915616C}"),
                                Title = "Pattern"
                            },
                            new {
                                Id = new Guid("{BD110234-F05D-42AB-BF2E-382B83093D0C}"),
                                Title = "Required"
                            },
                            new {
                                Id = new Guid("{CB9A60D3-42B3-411F-8FCE-2FC36C812A16}"),
                                Title = "Required True"
                            },
                            new {
                                Id = new Guid("{890C7A9E-09AE-4BB8-970E-85C564F753F1}"),
                                Title = "Unique"
                            }
                        })
                    }
                },
                #endregion
                #region Feature Entity
                new Entity()
                {
                    Id = new Guid("{D0E141A6-42CE-4AD3-A95E-24D40537342F}"),
                    InternalName = "Feature",
                    Title = "Feature",
                    EntityTemplate = Request.EntityTemplate.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{087C5441-AC68-4DE0-A1DE-03A59B6C58B5}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{732EA747-96A8-4437-A18A-B1BB990160A5}"),
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
                                Id = new Guid("{6114120E-BD93-4CE4-A673-7DC295F93CFE}"),
                                Title = "New"
                            }
                        })
                    }
                },
                #endregion
                #region Menu Item Type Entity
                new Entity()
                {
                    Id = new Guid("{092F60B1-EE1E-4451-A771-013376C93E65}"),
                    InternalName = "MenuItemType",
                    Title = "Menu Item Type",
                    EntityTemplate = Request.EntityTemplate.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{543BCF58-4929-4386-8B01-FBF4C1680430}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{7D223CCB-41AF-4888-B441-61B3BDBBE56B}"),
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
                                Id = new Guid("{51810E74-59E6-44AF-B6D3-1B8ECF82EE54}"),
                                Title = "Application Link"
                            },
                            new {
                                Id = new Guid("{A7B39F29-3887-4744-A4E3-926607DB15D2}"),
                                Title = "New"
                            }
                        })
                    }
                },
                #endregion
                #region Property Template Entity
                new Entity()
                {
                    Id = new Guid("{08A8E760-8620-44A9-9A15-646B6A53C881}"),
                    InternalName = "PropertyTemplate",
                    Title = "Property Template",
                    EntityTemplate = Request.EntityTemplate.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{ACEF7F12-2CF4-46AC-BFCC-60EA3E017E9F}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{732EA747-96A8-4437-A18A-B1BB990160A5}"),
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
                                Id = new Guid("{03CD1D4E-CA2B-4466-8016-D96C2DABEB0D}"),
                                Title = "Primary Key"
                            },
                            new {
                                Id = new Guid("{1B966A14-45B9-4E34-92BB-E2D46D97C5C3}"),
                                Title = "Reference Title"
                            }
                        })
                    }
                },
                #endregion
                #region Property Type Entity
                new Entity()
                {
                    Id = new Guid("{0B543B54-60AB-4FEA-BBD7-320AD50F3A06}"),
                    InternalName = "PropertyType",
                    Title = "Property Type",
                    EntityTemplate = Request.EntityTemplate.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{2E259BAE-F1E8-4B10-8672-8FDEC3061C80}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{8B61C0F2-9800-483C-A33E-0EFEDA6482BB}"),
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
                                Id = new Guid("{4247CAB3-DA47-4921-81B4-1DFF78909859}"),
                                Title = "Uniqueidentifier"
                            },
                            new {
                                Id = new Guid("{A05F5788-04C3-487D-92F1-A755C73230D4}"),
                                Title = "String"
                            },
                            new {
                                Id = new Guid("{F126388B-8A6E-41DB-A98A-A0E511016441}"),
                                Title = "Integer"
                            },
                            new {
                                Id = new Guid("{25E3A798-5F63-4A1E-93B3-A0BCE69836BC}"),
                                Title = "Date Time"
                            },
                            new {
                                Id = new Guid("{2C1D2E2A-3531-41D9-90D3-3632C368B12A}"),
                                Title = "Boolean"
                            },
                            new {
                                Id = new Guid("{8BB0B472-E8C4-4DCF-9EF4-FFA088B5A175}"),
                                Title = "Parent Relationship"
                            },
                            new {
                                Id = new Guid("{B42A437F-3DED-4B5F-A573-1CCEC1B2D58E}"),
                                Title = "Reference Relationship"
                            }
                        })
                    }
                },
                #endregion
                #region Screen Section Type Entity
                new Entity()
                {
                    Id = new Guid("{6B3442DE-02EA-4A89-BBC9-3C7E698C94EF}"),
                    InternalName = "ScreenSectionType",
                    Title = "Screen Section Type",
                    EntityTemplate = Request.EntityTemplate.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{4BBA98F7-4C6F-4E60-BE3F-06180D8A6141}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{4A67B6BF-99A0-4446-A61C-AF3EB857099C}"),
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
                                Id = new Guid("{DC1169A8-8F49-45E9-9969-B64BEF4D0F42}"),
                                Title = "Form"
                            },
                            new {
                                Id = new Guid("{0637300C-B76E-45E2-926A-055BB335129F}"),
                                Title = "Search"
                            },
                            new {
                                Id = new Guid("{4270A420-64CB-4A2C-B718-2C645DB2B57B}"),
                                Title = "List of Menu Items"
                            },
                            new {
                                Id = new Guid("{38EF9B44-A993-479B-91EC-1FE436E91556}"),
                                Title = "Html"
                            },
                        })
                    }
                },
#endregion
                #region Screen Type Entity
                new Entity()
                {
                    Id = new Guid("{C04282DB-CB85-445D-BB4B-AEBB3801DAC7}"),
                    InternalName = "ScreenType",
                    Title = "Screen Type",
                    EntityTemplate = Request.EntityTemplate.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{C9DBA086-ACC8-4BD9-8EEB-2B2AAF38CC78}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{749ACD4D-1873-4314-B0A1-E61EED934D3C}"),
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
                                Id = new Guid("{03CD1D4E-CA2B-4466-8016-D96C2DABEB0D}"),
                                Title = "Search"
                            },
                            new {
                                Id = new Guid("{9B422DE1-FACE-4A63-9A46-0BD1AF3D47F4}"),
                                Title = "Edit"
                            },
                            new {
                                Id = new Guid("{ACE5A965-7005-4E34-9C66-AF0F0CD15AE9}"),
                                Title = "View"
                            },
                            new {
                                Id = new Guid("{7A37305E-C518-4A16-91AE-BCF2AE032A9C}"),
                                Title = "Html"
                            }
                        })
                    }
                },
                #endregion
                #region Screen Template Entity
                new Entity()
                {
                    Id = new Guid("{564E3EC4-59E0-4EEB-A9E5-F1367E1FEB29}"),
                    InternalName = "ScreenTemplate",
                    Title = "Screen Template",
                    EntityTemplate = Request.EntityTemplate.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{C76EBB7F-2830-43B5-8A02-3F27825BD1C1}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{9B383F17-C149-470A-BF58-47C8057B74FF}"),
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
                                Id = new Guid("{142B82E8-471B-47E5-A13F-158D2B06874B}"),
                                Title = "Reference"
                            },
                            new {
                                Id = new Guid("{79FEFA81-D6F7-4168-BCAF-FE6494DC3D72}"),
                                Title = "Home"
                            }
                        })
                    }
                },
                #endregion
                #region Seed Type Entity
                new Entity()
                {
                    Id = new Guid("{EA6A9786-573A-4821-824C-3FB5322D2A51}"),
                    InternalName = "SeedType",
                    Title = "Seed Type",
                    EntityTemplate = Request.EntityTemplate.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{623C2ADB-9FAD-43F6-8DDD-970B21D49CCE}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{E67EC40A-C1B6-4465-BA91-D40A488317BC}"),
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
                                Id = new Guid("{8A07A94D-4A5F-420F-B02A-4B2223B1213B}"),
                                Title = "Add if none"
                            },
                            new {
                                Id = new Guid("{2729F45B-269F-42B1-BBA9-3E76DC9D1CBB}"),
                                Title = "Ensure All Added"
                            },
                            new {
                                Id = new Guid("{6989AE9F-D5BD-4861-ABE6-0142EDDE6130}"),
                                Title = "Ensure All Updated"
                            }
                        })
                    }
                },
                #endregion
                #region Entity Template Entity
                new Entity()
                {
                    Id = new Guid("{E20337EA-37F3-48D1-96F7-3CF2A40A7F52}"),
                    InternalName = "EntityTemplate",
                    Title = "Entity Template",
                    EntityTemplate = Request.EntityTemplate.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{5D0BEB0A-4B91-4B40-83F0-8EAE7426CCF6}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{83CFE967-1FEF-44F1-A3DC-C03FC2F0B167}"),
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
                                Id = new Guid("{B79D1C90-6320-4A07-9753-2A41110611C8}"),
                                Title = "Reference"
                            }
                        })
                    }
                },
#endregion
                #region Service Type Entity
                new Entity()
                {
                    Id = new Guid("{65D65D11-D1F0-421E-811E-A8CBB84CBB0D}"),
                    InternalName = "ServiceType",
                    Title = "Service Type",
                    EntityTemplate = Request.EntityTemplate.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{83B85CF2-1D09-473B-9212-9602E9CD7F5D}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{372DD3D3-F71D-4A25-91E6-7F8DC7D5AA44}"),
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
                                Id = new Guid("{87D5B4C5-9862-4282-8BB2-E2707A17036A}"),
                                Title = "Web Service"
                            },
                            new
                            {
                                Id = new Guid("{DD6A4434-16E1-41BF-B75B-C78E3A8D46BF}"),
                                Title = "Export"
                            }
                        })
                    }
                },
#endregion
                #region Http Verb
                new Entity()
                {
                    Id = new Guid("{384874D5-107B-48A3-980C-8F299BEADAE1}"),
                    InternalName = "HttpVerb",
                    Title = "Http Verb",
                    EntityTemplate = Request.EntityTemplate.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{431DFED1-4CD6-4D26-8BEF-54109AA9FC43}"),
                            InternalName = "Id",
                            PropertyType = PropertyType.PrimaryKey,
                            Title = "Id"
                        },
                        new Property()
                        {
                            Id = new Guid("{37BEB522-5AA5-46B2-A2B3-5111F9596A50}"),
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
                                Id = new Guid("{EF85B946-2F8D-49D6-AFE7-924D53CBA328}"),
                                Title = "Get"
                            },
                            new
                            {
                                Id = new Guid("{B5EFB225-77A7-4BC7-8062-BC4B296CEFAB}"),
                                Title = "Post"
                            },
                            new
                            {
                                Id = new Guid("{AC3CA150-F44C-4F6C-A26F-09573DB433CF}"),
                                Title = "Put"
                            },
                            new
                            {
                                Id = new Guid("{B6FBA91F-753D-4B80-9624-BDDA8F0E2868}"),
                                Title = "Delete"
                            }
                        })
                    }
                }
#endregion
            };
        }
    }
}
