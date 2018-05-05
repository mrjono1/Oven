using MasterBuilder.Request;

namespace MasterBuilder.Templates.Api.Models
{
    /// <summary>
    /// Model Search Item Property Template
    /// </summary>
    public class ModelSearchItemPropertyTemplate
    {
        /// <summary>
        /// Evaluate
        /// </summary>
        public static string Evaluate(SearchColumn searchColumn)
        {
            return $@"        /// <summary>
        /// {searchColumn.TitleValue}
        /// </summary>
        [Display(Name = ""{searchColumn.TitleValue}"")]
        public {searchColumn.TypeCSharp} {searchColumn.InternalNameCSharp} {{ get; set; }}";
        }
    }
}
