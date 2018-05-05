using MasterBuilder.Interfaces;

namespace MasterBuilder.Templates.Angular.ClientApp.App.Shared
{
    /// <summary>
    /// Http Error service template
    /// </summary>
    public class HttpErrorServiceTemplate : ITemplate
    {
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "httperror.service.ts";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new[] { "ClientApp", "app", "shared" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"import {{ Injectable }} from '@angular/core';
import {{ FormControl, FormGroup }} from '@angular/forms';
import {{ HttpErrorResponse }} from '@angular/common/http';

@Injectable()
export class HttpErrorService {{
    formGroup: FormGroup;
    serverErrorMessages: any;

    lowerCaseFirstLetter(value: string) {{
        return value.charAt(0).toLowerCase() + value.slice(1);
    }}
    
    setModelStateErrors(httpErrorResponse: HttpErrorResponse) {{
        if (httpErrorResponse.error) {{
            for (let errorKey in httpErrorResponse.error) {{
                var formControl = this.getControl(this.formGroup, errorKey);

                this.serverErrorMessages[this.lowerCaseFirstLetter(errorKey)] = httpErrorResponse.error[errorKey].join(' ');

                formControl.setErrors({{
                    'server': true
                }});
            }}
        }}
    }}

    getControl(formGroup: FormGroup, key: string): FormControl {{
        let currentKey = key;
        let remainingKey = null;
        if (key.indexOf('.') !== -1) {{
            currentKey = key.split('.')[0];
            remainingKey = key.slice(currentKey.length + 1);
        }}

        let formControlName = this.lowerCaseFirstLetter(currentKey);
        let abstractControl = formGroup.get(formControlName);

        if (remainingKey !== null) {{
            return this.getControl(abstractControl as FormGroup, remainingKey);
        }} else {{
            return abstractControl as FormControl;
        }}
    }}

    handleError(formGroup: FormGroup, serverErrorMessages: any, error: HttpErrorResponse) {{
        this.formGroup = formGroup;
        this.serverErrorMessages = serverErrorMessages;
        this.setModelStateErrors(error);
    }}
}}";
        }
    }
}
