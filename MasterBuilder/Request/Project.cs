using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MasterBuilder.Request
{
    /// <summary>
    /// Project
    /// </summary>
    public partial class Project
    {
        internal static readonly Guid MasterBuilderId = new Guid("{D1CB7777-6E61-486B-B15E-05B97B57D0FC}");
        /// <summary>
        /// Constructor sets default values
        /// </summary>
        public Project()
        {
            ImutableDatabase = false;
            AllowDestructiveDatabaseChanges = true;
            ServerSideRendering = false;
            UsePutForUpdate = true;
            
            CleanDirectoryIgnoreDirectories = new string[]
            {
                "bin",
                "obj",
                "node_modules",
                "wwwroot",
                "Properties",
                "dist"
            };
        }
        /// <summary>
        /// Uniqueidentifier
        /// </summary>
        [RequiredNonDefault]
        public Guid Id { get; set; }
        /// <summary>
        /// Internal Name
        /// </summary>
        [MaxLength(100)]
        [MinLength(5)]
        [PascalString]
        public string InternalName { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        [Required]
        [MinLength(5)]
        [MaxLength(200)]
        public string Title { get; set; }
        /// <summary>
        /// Major version
        /// </summary>
        [Required]
        [Range(0, 999999)]
        public int MajorVersion { get; set; }
        /// <summary>
        /// Minor Version
        /// </summary>
        [Required]
        [Range(0, 999999)]
        public int MinorVersion { get; set; }
        /// <summary>
        /// Build Version
        /// </summary>
        [Required]
        [Range(0, 99999999)]
        public int BuildVersion { get; set; }

        /// <summary>
        /// Semantic Version
        /// </summary>
        internal string Version
        {
            get
            {
                return $"{MajorVersion}.{MinorVersion}.{BuildVersion}";
            }
        }
        /// <summary>
        /// Entities
        /// </summary>
        [MustHaveOneElement]
        public IEnumerable<Entity> Entities { get; set; }
        /// <summary>
        /// Screens
        /// </summary>
        [MustHaveOneElement]
        public IEnumerable<Screen> Screens { get; set; }
        /// <summary>
        /// Menu Items
        /// </summary>
        [MustHaveOneElement]
        public IEnumerable<MenuItem> MenuItems { get; set; }
        /// <summary>
        /// Service configurations
        /// </summary>
        public IEnumerable<Service> Services { get; set; }
        /// <summary>
        /// Default Screen to load
        /// </summary>
        public Guid? DefaultScreenId { get; set; }

        #region Internal Settings
        /// <summary>
        /// Use MySql instead of MS Sql (not fully implemented explicit migrations needed)
        /// </summary>
        internal bool UseMySql { get; set; }
        /// <summary>
        /// Allow Destructive Database Change, things like dropping columns, tables, keys
        /// </summary>
        internal bool AllowDestructiveDatabaseChanges { get; set; }
        /// <summary>
        /// Folders to ignore when cleaning out the directory on build
        /// </summary>
        internal string[] CleanDirectoryIgnoreDirectories { get; set; }
        /// <summary>
        /// If true database tables and columns are all uniqueidentifiers making database 
        /// hard to use but less chance of needing to change database columns which can result in data loss
        /// </summary>
        internal bool? ImutableDatabase { get; set; }
        /// <summary>
        /// Enable Server side rendering
        /// </summary>
        internal bool ServerSideRendering { get; set; }
        /// <summary>
        /// Use Put for update instead of patch
        /// </summary>
        internal bool UsePutForUpdate { get; set; }
        #endregion
        /// <summary>
        /// Validate and Resolve this and child objects
        /// </summary>
        /// <param name="message">Error messages</param>
        /// <returns>true for no errors</returns>
        internal bool ValidateAndResolve(out string message)
        {
            var errors = new List<string>();

            if (!Validate(out string validateError))
            {
                errors.Add(validateError);
            }

            if (!Resolve(out string resolveError))
            {
                errors.Add(resolveError);
            }
            
            if (errors.Any())
            {
                message = string.Join(Environment.NewLine, errors);
                return false;
            }
            else
            {
                message = null;
                return true;
            }
        }
    }
}
