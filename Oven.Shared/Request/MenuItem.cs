using Humanizer;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Oven.Request
{
    /// <summary>
    /// Menu Item
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// Register of all Menu Item Type Ids to Enum for easy use
        /// </summary>
        internal static readonly Dictionary<ObjectId, MenuItemType> MenuItemTypeDictonary = new Dictionary<ObjectId, MenuItemType>
        {
            { new ObjectId("5ca86a896668b25914b67e70"), MenuItemType.ApplicationLink },
            { new ObjectId("5ca86a9b6668b25914b67e71"), MenuItemType.New },
            { new ObjectId("5ca86aad6668b25914b67e72"), MenuItemType.ServerFunction }
        };

        /// <summary>
        /// Uniqueidentifier
        /// </summary>
        [Required]
        [NonDefault]
        public ObjectId Id { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Path
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Navigate to Screen Id
        /// </summary>
        [NonDefault]
        public ObjectId? ScreenId { get; set; }
        /// <summary>
        /// Icon css class
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// Menu Item Type Id
        /// </summary>
        [Required]
        [NonDefault]
        public ObjectId MenuItemTypeId { get; set; }
        /// <summary>
        /// Server side C# code
        /// </summary>
        public string ServerCode { get; set; }

        #region Helpers
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
        private string internalName;
        /// <summary>
        /// Calculated Internal Name
        /// </summary>
        [JsonIgnore]
        public string InternalName
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
        #endregion

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