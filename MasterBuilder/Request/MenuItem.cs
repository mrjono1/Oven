using System;

namespace MasterBuilder.Request
{
    public class MenuItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public Guid? ScreenId { get; set; }
        public string Icon { get; set; }
        public MenuItemTypeEnum MenuItemType { get; set; }
    }
}