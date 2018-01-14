using MasterBuilder.Interfaces;

namespace MasterBuilder.Templates.ClientApp.Polyfills
{
    /// <summary>
    /// RX Imports Template
    /// </summary>
    public class RxImportsTemplate : ITemplate
    {
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "rx-imports.ts";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new[] { "ClientApp", "polyfills" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return @"/* -=- RxJs imports -=-
 * 
 * Here you can place any RxJs imports so you don't have to constantly 
 * import them throughout your App :)
 * 
 * This file is automatically imported into `polyfills.ts` (which is imported into browser/server modules)
 */

// General Operators
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/throttleTime';
import 'rxjs/add/operator/distinctUntilChanged';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/take';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/filter';
import 'rxjs/add/operator/mergeMap';
import 'rxjs/add/operator/concat';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/first';

// Observable operators
import 'rxjs/add/observable/fromEvent';
import 'rxjs/add/observable/interval';
import 'rxjs/add/observable/fromPromise';
import 'rxjs/add/observable/of';
import 'rxjs/add/observable/concat';";
        }
    }
}
