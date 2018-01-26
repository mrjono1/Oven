using MasterBuilder.Interfaces;

namespace MasterBuilder.Templates.ClientApp.App.Shared
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
                var formControlName = this.lowerCaseFirstLetter(errorKey);
                var formControl = this.formGroup.get(formControlName);
                
                this.serverErrorMessages[formControlName] = httpErrorResponse.error[errorKey].join(' ');

                formControl.setErrors({{
                    ""server"": true
                }});
            }}
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
