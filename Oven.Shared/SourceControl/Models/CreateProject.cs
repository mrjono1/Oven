namespace Oven.SourceControl.Models
{
    /// <summary>
    /// VSTS API object
    /// </summary>
    public class CreateProject
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CreateProject()
        {
            Capabilities = new Capability();
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Capabilities
        /// </summary>
        public Capability Capabilities { get; set; }

        /// <summary>
        /// Capability
        /// </summary>
        public class Capability
        {
            /// <summary>
            /// Constructor
            /// </summary>
            public Capability()
            {
                Versioncontrol = new VersionControl();
                ProcessTemplate = new ProcessTemplate();
            }

            /// <summary>
            /// Version Control
            /// </summary>
            public VersionControl Versioncontrol { get; set; }

            /// <summary>
            /// Process Template
            /// </summary>
            public ProcessTemplate ProcessTemplate { get; set; }
        }

        /// <summary>
        /// Version Control
        /// </summary>
        public class VersionControl
        {
            /// <summary>
            /// Constructor
            /// </summary>
            public VersionControl()
            {
                SourceControlType = "Git";
            }

            /// <summary>
            /// Source Control type
            /// </summary>
            public string SourceControlType { get; set; }
        }

        /// <summary>
        /// Process Template
        /// </summary>
        public class ProcessTemplate
        {
            /// <summary>
            /// Constructor
            /// </summary>
            public ProcessTemplate()
            {
                // Scrum - Defult as at 2017-12-27
                TemplateTypeId = "6b724908-ef14-45cf-84f8-768b5384da45";
            }

            /// <summary>
            /// Template Type Id (Guid)
            /// </summary>
            public string TemplateTypeId { get; set; }
        }
    }
}
