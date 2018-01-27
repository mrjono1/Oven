namespace MasterBuilder
{
    /// <summary>
    /// Builder Settings
    /// </summary>
    public class BuilderSettings
    {
        /// <summary>
        /// Base project output directory
        /// </summary>
        public string OutputDirectory { get; set; }
        /// <summary>
        /// Git User Name
        /// </summary>
        public string GitUserName { get; set; }
        /// <summary>
        /// Git Email
        /// </summary>
        public string GitEmail { get; set; }
        /// <summary>
        /// Visual Studio Team Services Personal Access Token
        /// </summary>
        public string VstsPersonalAccessToken { get; set; }
    }
}
