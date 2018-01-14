using System;
using System.Linq;
using System.Collections.Generic;

namespace MasterBuilder.Request
{
    partial class Entity
    {
        /// <summary>
        /// Validate this object and reslove any issues if possible
        /// </summary>
        internal bool Validate(Project project, out string message)
        {
            var messageList = new List<string>();

            var parentPropertyCount = Properties.Count(p => p.PropertyType == PropertyTypeEnum.ParentRelationship);
            if (parentPropertyCount > 1)
            {
                messageList.Add($"Entity:{Title} can only contain one parent properties it contains {parentPropertyCount}");
            }

            foreach (var property in Properties)
            {
                if (!property.Validate(project, this, out string propertyMessage))
                {
                    messageList.Add(propertyMessage);
                }
            }

            if (messageList.Any())
            {
                message = string.Join(Environment.NewLine, messageList);
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
