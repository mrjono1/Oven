namespace MasterBuilder.SourceControl.Models
{
    public class CreateProject
    {
        public CreateProject()
        {
            Capabilities = new Capability();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public Capability Capabilities { get; set; }

        public class Capability
        {
            public Capability()
            {
                Versioncontrol = new VersionControl();
                ProcessTemplate = new ProcessTemplate();
            }
            public VersionControl Versioncontrol { get; set; }
            public ProcessTemplate ProcessTemplate { get; set; }
        }
        public class VersionControl
        {
            public VersionControl()
            {
                SourceControlType = "Git";
            }
            public string SourceControlType { get; set; }
        }
        public class ProcessTemplate
        {
            public ProcessTemplate()
            {
                // Scrum - Defult as at 2017-12-27
                TemplateTypeId = "6b724908-ef14-45cf-84f8-768b5384da45";
            }

            public string TemplateTypeId { get; set; }
        }
    }
}
