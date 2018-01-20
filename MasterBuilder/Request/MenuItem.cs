using Humanizer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MasterBuilder.Request
{
    /// <summary>
    /// Menu Item
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// Register of all Menu Item Type Ids to Enum for easy use
        /// </summary>
        internal static readonly Dictionary<Guid, MenuItemType> MenuItemTypeDictonary = new Dictionary<Guid, MenuItemType>
        {
            { new Guid("{51810E74-59E6-44AF-B6D3-1B8ECF82EE54}"), MenuItemType.ApplicationLink },
            { new Guid("{A7B39F29-3887-4744-A4E3-926607DB15D2}"), MenuItemType.New },
            { new Guid("{2BB3BFAC-FBA8-4786-A854-193EC73A01AF}"), MenuItemType.ServerFunction }
        };

        /// <summary>
        /// Uniqueidentifier
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        private string internalName;
        /// <summary>
        /// Calculated Internal Name
        /// </summary>
        internal string InternalName
        {
            get
            {
                if (internalName == null)
                {
                    internalName = Title.Pascalize();
                }
                return internalName;
            }
        }
        /// <summary>
        /// Path
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Navigate to Screen Id
        /// </summary>
        public Guid? ScreenId { get; set; }
        /// <summary>
        /// Icon css class
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// Menu Item Type Id
        /// </summary>
        public Guid MenuItemTypeId { get; set; }
        /// <summary>
        /// Menu Item Type
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public MenuItemType MenuItemType
        {
            get
            {
                return MenuItemTypeDictonary[MenuItemTypeId];
            }
            set
            {
                MenuItemTypeId = MenuItemTypeDictonary.SingleOrDefault(v => v.Value == value).Key;
            }
        }
        /// <summary>
        /// Server side C# code
        /// </summary>
        public string ServerCode { get; set; }

        /// <summary>
        /// Validate Menu Item
        /// </summary>
        internal bool Validate(Project project, out string message)
        {
            var messageList = new List<string>();
            
            // Title
            if (string.IsNullOrWhiteSpace(Title))
            {
                messageList.Add("Menu must have a title");
            }

            if (ScreenId.HasValue)
            {
                var screen = project.Screens.SingleOrDefault(a => a.Id == ScreenId.Value);
                if (screen == null)
                {
                    messageList.Add($"Menu item {Title}'s has navigates to a screen that no longer exists");
                }
            }

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
    }
}