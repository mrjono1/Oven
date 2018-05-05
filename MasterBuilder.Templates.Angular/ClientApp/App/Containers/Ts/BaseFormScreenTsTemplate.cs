using System;
using System.Collections.Generic;
using System.Linq;
using Humanizer;
using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.Angular.ClientApp.App.Containers.Ts
{
    /// <summary>
    /// Base Form Screen Ts Template
    /// </summary>
    public class BaseFormScreenTsTemplate : ITemplate
    {
        private readonly Project Project;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public BaseFormScreenTsTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "base.form.screen.ts";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "ClientApp", "app", "containers" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"import {{ FormGroup }} from ""@angular/forms"";

/**
 * This is the base class for Form Screens
 */
export abstract class BaseFormScreen {{

    /**
     * Is the record new.
     */
    public new: boolean;
    public serverErrorMessages: any = {{}};

    referenceCompare(referenceItem1: any, referenceItem2: any): boolean {{
        {{
            return referenceItem1 === referenceItem2;
        }}
    }}

    /**
     * Patches the value of a control.
     *
     * This function is functionally was copeid from the core patchValue code in FormGroup
     * This version skips groups that have a null value
     */
    patchValue(abstractControl: FormGroup, value: {{ [key: string]: any }}, options: {{ onlySelf?: boolean, emitEvent?: boolean }} = {{}}): void {{
        Object.keys(value).forEach(name => {{
            let control = abstractControl.controls[name];
            if (control){{
                if (control instanceof FormGroup) {{
                    if (value[name] !== null) {{
                        this.patchValue(control as FormGroup, value[name], {{ onlySelf: true, emitEvent: options.emitEvent }});
                    }}
                }} else {{
                    control.patchValue(value[name], {{ onlySelf: true, emitEvent: options.emitEvent }});
                }}
            }}
        }});
        abstractControl.updateValueAndValidity(options);
    }}
}}";
        }
    }
}
