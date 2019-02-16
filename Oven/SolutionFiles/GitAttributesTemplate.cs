using Oven.Interfaces;

namespace Oven.Templates.SolutionFiles
{
    /// <summary>
    /// Git ignore
    /// </summary>
    public class GitAttributesTemplate :ITemplate
    {
        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileName()
        {
            return ".gitattributes";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        /// <returns></returns>
        public string GetFileContent()
        {
            return @"# Auto detect text files and perform LF normalization
* text=auto

# Custom for Visual Studio
*.cs diff=csharp
*.sln -text merge=union";
        }
    }
}
