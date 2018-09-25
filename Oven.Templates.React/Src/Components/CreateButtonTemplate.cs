using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.React.Src.Components
{
    /// <summary>
    /// CreateButton.jsx Template
    /// </summary>
    public class CreateButtonTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public CreateButtonTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "CreateButton.jsx";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "src", "components" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"import React from 'react';
import Button from '@material-ui/core/Button';
import {{ Link }} from 'react-router-dom';

const CreateButton = ({{ record, reference, target, title = 'Create', defaultValues = {{}} }}) => (
    <Button
        component={{Link}}
        to={{{{
            pathname: `/${{reference}}/create`,
            state: {{ record: {{ ...defaultValues, [target]: record ? record.id : null }} }}
        }}}}
    >
        {{title}}
    </Button>
);

export default CreateButton;";
        }
    }
}