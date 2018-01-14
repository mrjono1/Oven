using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Request
{
    partial class Project
    {
        /// <summary>
        /// Validate a whole project, also may resolve some issues or perform upgrades
        /// </summary>
        /// <param name="message">returns "Success" or details an issue found</param>
        protected bool Validate(out string message)
        {
            var errors = new List<string>();

            // Validate Entities
            if (Entities.Select(i => i.Id).Distinct().Count() != Entities.Count())
            {
                errors.Add("Duplicate entity ids defined");
            }
            if (Entities.Select(i => i.InternalName).Distinct().Count() != Entities.Count())
            {
                errors.Add("Duplicate entity Internal Name defined");
            }
            foreach (var entity in Entities)
            {
                if (!entity.Validate(this, out string entityMessage))
                {
                    errors.Add(entityMessage);
                }
            }

            // Validate Screens
            if (Screens.Select(i => i.Id).Distinct().Count() != Screens.Count())
            {
                errors.Add("Duplicate screen ids defined");
            }
            foreach (var screen in Screens)
            {
                if (!screen.Validate(out string screenMessage))
                {
                    errors.Add(screenMessage);
                }
            }

            foreach (var menuItem in MenuItems)
            {
                if (!menuItem.Validate(this, out string screenMessage))
                {
                    errors.Add(screenMessage);
                }
            }
            
            if (errors.Any())
            {
                message = string.Join(Environment.NewLine, errors);
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
