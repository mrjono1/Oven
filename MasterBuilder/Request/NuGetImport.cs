using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterBuilder.Request
{
    /// <summary>
    /// Item that specifies a NuGet dependency in the project. The Include property specifies the package ID.
    /// </summary>
    /// <remarks>
    /// The PrivateAssets/IncludeAssets/ExcludeAssets properties can have the following values:
    /// These attributes can contain one or more of the following items:
    /// <list type="bullet">
    /// <item>Compile – the contents of the lib folder are available to compile against.</item>
    /// <item>Runtime – the contents of the runtime folder are distributed.</item>
    /// <item>ContentFiles – the contents of the contentfiles folder are used.</item>
    /// <item>Build – the props/targets in the build folder are used.</item>
    /// <item>Native – the contents from native assets are copied to the output folder for runtime.</item>
    /// <item>Analyzers – the analyzers are used.</item>
    /// </list>
    /// Alternatively, the properties can contain:
    /// <list type="bullet">
    /// <item>None – none of the assets are used.</item>
    /// <item>All – all assets are used.</item>
    /// </list>
    /// </remarks>
    public partial class NuGetImport
    {
        /// <summary>
        /// Uniqueidentifier
        /// </summary>
        [Required]
        [NonDefault]
        public Guid Id { get; set; }

        /// <summary>
        /// The NuGet package ID to be included as a dependency e.g. Newtonsoft.Json
        /// </summary>
        [Required]
        public string Include { get; set; }

        /// <summary>
        /// Specifies the version of the package to restore. The attribute respects the rules of the NuGet versioning scheme. The default behavior is an exact version match. For example, specifying Version="1.2.3" is equivalent to NuGet notation [1.2.3] for the exact 1.2.3 version of the package.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Specifies what assets belonging to the package specified by <see cref="Include"/> should be consumed but that they should not flow to the next project.
        /// </summary>
        public string PrivateAssets { get; set; }

        /// <summary>
        /// Specifies what assets belonging to the package specified by <see cref="Include"/> should be consumed.
        /// </summary>
        public string IncludeAssets { get; set; }

        /// <summary>
        /// Specifies what assets belonging to the package specified by <see cref="Include"/> should not be consumed.
        /// </summary>
        public string ExcludeAssets { get; set; }
    }
}
