using MasterBuilder.Request;

namespace MasterBuilder.Templates.Utilities
{
    /// <summary>
    /// Entity Framework Context Extensions Template
    /// </summary>
    public class EntityFrameworkContextExtensionsTemplate
    {
        /// <summary>
        /// File name
        /// </summary>
        public static string FileName()
        {
            return "EntityFrameworkContextExtensions.cs";
        }

        /// <summary>
        /// Evaluate
        /// </summary>
        public static string Evaluate(Project project)
        {
            return $@"
        private static readonly Dictionary<int, string> _sqlErrorTextDict =
        new Dictionary<int, string>
    {{
        {{547,
         ""This operation failed because another data entry uses this entry.""}},
        {{ 2601,
         ""One of the properties is marked as Unique index and there is already an entry with that value.""}}
    }};

        /// <summary>
        /// This decodes the DbUpdateException. If there are any errors it can
        /// handle then it returns a list of errors. Otherwise it returns null
        /// which means rethrow the error as it has not been handled
        /// </summary>
        /// <param id=""ex""></param>
        /// <returns>null if cannot handle errors, otherwise a list of errors</returns>
        IEnumerable<ValidationResult> TryDecodeDbUpdateException(DbUpdateException ex)
        {{
            if (!(ex.InnerException is System.Data.SqlClient.SqlException))
                return null;
            var sqlException =
                (System.Data.SqlClient.SqlException)ex.InnerException;
            var result = new List<ValidationResult>();
            for (int i = 0; i < sqlException.Errors.Count; i++)
            {{
                var errorNum = sqlException.Errors[i].Number;
                string errorText;
                if (_sqlErrorTextDict.TryGetValue(errorNum, out errorText))
                    result.Add(new ValidationResult(errorText));
            }}
            return result.Any() ? result : null;
        }}";
        }
    }
}
