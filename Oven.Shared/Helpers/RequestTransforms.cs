using Oven.Request;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System;

namespace Oven.Helpers
{
    public static class RequestTransforms
    {
        public static ScreenItem GetScreenFieldsNested(Screen screen, ScreenItem screenItem = null)
        {
            if (screenItem == null)
            {
                screenItem = new ScreenItem
                {
                    Entity = screen.Entity
                };
            }

            var screenSectionId = (from ss in screen.ScreenSections
                                       where ss.ScreenSectionType == ScreenSectionType.Form &&
                                       ss.EntityId == screenItem.Entity.Id &&
                                       !ss.ParentScreenSectionId.HasValue
                                       select ss.Id).FirstOrDefault();

            // TODO: if null log error

            screenItem.FormFields = (from ss in screen.ScreenSections
                                     where ss.ScreenSectionType == ScreenSectionType.Form &&
                                     ss.EntityId == screenItem.Entity.Id
                                     from ff in ss.FormSection.FormFields
                                     select ff).Distinct().ToList();

            screenItem.ChildScreenItems = (from ss in screen.ScreenSections
                                           where ss.ScreenSectionType == ScreenSectionType.Form &&
                                           ss.ParentScreenSectionId == screenSectionId
                                           select ss)
                                           .GroupBy(ss => new
                                           {
                                               ss.Entity
                                           }).Select(a =>
                                           new ScreenItem
                                           {
                                               Entity = a.Key.Entity,
                                               ParentScreenSectionIds = a.Where(v => v.ParentScreenSectionId.HasValue).Select(v => v.ParentScreenSectionId.Value).ToArray()
                                           }).Distinct().ToList();

            foreach (var childScreenItem in screenItem.ChildScreenItems)
            {
                GetScreenFieldsNested(screen, childScreenItem);
            }

            return screenItem;
        }

        /// <summary>
        /// Get Screen Section Entity Fields
        /// </summary>
        public static IEnumerable<ScreenSectionEntityFormFields> GetScreenSectionEntityFields(Screen screen, Guid? entityId = null)
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
