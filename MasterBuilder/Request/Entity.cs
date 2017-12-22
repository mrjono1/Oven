using Humanizer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MasterBuilder.Request
{
    /// <summary>
    /// The configuration for an entity
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Register of all Entity Template Ids to Enum for easy use
        /// </summary>
        internal static readonly Dictionary<Guid, EntityTemplateEnum> EntityTemplateDictonary = new Dictionary<Guid, EntityTemplateEnum>
        {
            { Guid.Empty, EntityTemplateEnum.None },
            { new Guid("{B79D1C90-6320-4A07-9753-2A41110611C8}"), EntityTemplateEnum.Reference }
        };

        /// <summary>
        /// Uniqueidentifier of an Entity
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Title of and Entity
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Internal name of an Entity
        /// </summary>
        public string InternalName { get; set; }

        /// <summary>
        /// List of Properties
        /// </summary>
        public IEnumerable<Property> Properties { get; set; }

        /// <summary>
        /// Cached version of the internal name pluralised for use in collections
        /// </summary>
        private string _internalNamePlural = null;
        /// <summary>
        /// Pluralised version of the internal name for use in collections
        /// </summary>
        internal string InternalNamePlural
        {
            get
            {
                if (_internalNamePlural == null)
                {
                    _internalNamePlural = InternalName.Pluralize();
                }
                return _internalNamePlural;
            }
        }
        
        /// <summary>
        /// Optional: Contains seed data and settings
        /// </summary>
        public Seed Seed { get; set; }
        /// <summary>
        /// Optional: Entity Template Id, if specified it will define default behaviours for this entity
        /// </summary>
        public Guid? EntityTemplateId { get; set; }
        /// <summary>
        /// Entity Template
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public EntityTemplateEnum EntityTemplate
        {
            get
            {
                var id = EntityTemplateId ?? Guid.Empty;
                return EntityTemplateDictonary[id];
            }
            set
            {
                var id = EntityTemplateDictonary.SingleOrDefault(v => v.Value == value).Key;
                if (id == Guid.Empty)
                {
                    EntityTemplateId = null;
                }
                else
                {
                    EntityTemplateId = id;
                }
            }
        }

        /// <summary>
        /// Validate this object and reslove any issues if possible
        /// </summary>
        /// <param name="message">Issue messages</param>
        /// <returns>'true' for success or 'false' for failing to validate</returns>
        internal bool Validate(Project project, out string message)
        {
            var messageList = new List<string>();

            // Id must not be empty
            if (Id == Guid.Empty)
            {
                messageList.Add("Entity must have an Id");
            }
            
            // Title
            if (string.IsNullOrWhiteSpace(Title))
            {
                messageList.Add("Entity must have a title");
            }

            // Internal Name must be valid
            if (!Regex.IsMatch(InternalName, @"^[a-zA-Z]+$"))
            {
                messageList.Add("Entity Internal Name must only contain letters");
            }
            // Ensure Internal Name is standard caseing
            InternalName = InternalName.Pascalize();

            // TODO: do more validation in model validation

            if (Properties != null)
            {
                foreach (var property in Properties)
                {
                    if (!property.Validate(this, out string propertyMessage)) {
                        messageList.Add(propertyMessage);
                    }
                }
            }

            GenerateScreen(project);
            
            if (messageList.Any())
            {
                message = string.Join(", ", messageList);
                return false;
            }
            else
            {
                message = "Success";
                return true;
            }
        }

        private void GenerateScreen(Project project)
        {
            if (EntityTemplate == EntityTemplateEnum.Reference)
            {
                var screenFound = project.Screens.Where(s => s.EntityId == Id).Any();

                if (screenFound)
                {
                    return;
                }

                var editScreenId = Guid.NewGuid(); // TODO: The id should be reproduceable I don't like this
                var screens = new List<Screen>(project.Screens)
                {
                    new Screen()
                    {
                        Id = Id, // TODO: The id should be reproduceable I don't like this
                        EntityId = Id,
                        Title = Title.Pluralize(),
                        InternalName = InternalNamePlural,
                        ScreenType = ScreenTypeEnum.Search,
                        Path = InternalNamePlural.Kebaberize(),
                        NavigateToScreenId = editScreenId
                    },
                    new Screen()
                    {
                        Id = editScreenId,
                        EntityId = Id,
                        Title = Title,
                        InternalName = InternalName,
                        ScreenType = ScreenTypeEnum.Edit,
                        Path = InternalName.Kebaberize()
                    }
                };
                project.Screens = screens;

                var menus = new List<MenuItem>(project.MenuItems)
                {
                    new MenuItem()
                    {
                        ScreenId = Id,
                        Title = Title.Pluralize(),
                        Icon = "glyphicon glyphicon-th-list"
                    }
                };
                project.MenuItems = menus;
            }
        }
    }
}
