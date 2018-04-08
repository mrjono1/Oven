using MasterBuilder.Request;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System;

namespace MasterBuilder.Helpers
{
    internal static class RequestTransforms
    {
        /// <summary>
        /// Get Screen Section Entity Fields
        /// </summary>
        internal static IEnumerable<ScreenSectionEntityFormFields> GetScreenSectionEntityFields(Screen screen, Guid? entityId = null)
        {
            var result = new List<ScreenSectionEntityFormFields>();

            var groupedFormScreenSections = (from ss in screen.ScreenSections
                                             where ss.ScreenSectionType == ScreenSectionType.Form
                                             select ss)
                                   .GroupBy(ss => new
                                   {
                                       ParentSection = ss.ParentScreenSection,
                                       ss.Entity
                                   })
                                   .Select(a => new { a.Key.ParentSection, Values = a.ToArray() });

            foreach (var group in groupedFormScreenSections)
            {
                var children = new List<ScreenSection>();

                foreach (var item in group.Values)
                {
                    var childSections = (from child in groupedFormScreenSections
                                        where child.ParentSection == item
                                        select child.Values).ToList();
                    if (childSections.Any())
                    {
                        childSections.ForEach(cs => children.AddRange(cs));
                        break;
                    }
                }

                var effe = new ScreenSectionEntityFormFields
                {
                    Entity = group.Values.First().Entity,
                    FormFields = new List<FormField>(),
                    ChildEntities = new List<Entity>()
                };

                foreach (var ssGroup in (from formSection in @group.Values
                                         from ff in formSection.FormSection.FormFields
                                         select ff).GroupBy(ff => ff.EntityPropertyId))
                {
                    effe.FormFields.Add(ssGroup.First());
                }

                foreach (var item in children)
                {
                    effe.ChildEntities.Add(item.Entity);
                }
                result.Add(effe);

            }

            if (!entityId.HasValue)
            {
                return result;
            }
            else
            {
                return result.Where(a => a.Entity != null && a.Entity.Id == entityId.Value);
            }
        }
    }
}
