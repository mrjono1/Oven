using MasterBuilder.Request;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Helpers
{
    internal static class RequestTransforms
    {
        /// <summary>
        /// Get Screen Section Entity Fields
        /// </summary>
        internal static IEnumerable<ScreenSectionEntityFormFields> GetScreenSectionEntityFields(Screen screen)
        {
            var result = new List<ScreenSectionEntityFormFields>();
            var defaultScreenSection = new ScreenSection();

            var groupedFormScreenSections = (from ss in screen.ScreenSections
                                             where ss.ScreenSectionType == ScreenSectionType.Form
                                             select ss)
                                   .GroupBy(ss => ss.ParentScreenSection ?? defaultScreenSection)
                                   .Select(a => new { a.Key, Values = a.ToArray() }).ToDictionary(t => t.Key, t => t.Values);

            foreach (var group in groupedFormScreenSections)
            {
                var children = new List<ScreenSection>();

                foreach (var item in group.Value)
                {
                    if (groupedFormScreenSections.ContainsKey(item))
                    {
                        children.AddRange(groupedFormScreenSections[item]);
                        break;
                    }
                }

                var effe = new ScreenSectionEntityFormFields
                {
                    Entity = group.Value.First().Entity,
                    FormFields = new List<FormField>(),
                    ChildEntities = new List<Entity>()
                };

                foreach (var ssGroup in (from formSection in @group.Value
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

            return result;
        }
    }
}
