using MasterBuilder.Helpers;

namespace MasterBuilder.Templates.ClientApp.Polyfills
{
    /// <summary>
    /// Polyfills Template
    /// </summary>
    public class PolyfillsTemplate : ITemplate
    {
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "polyfills.ts";
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
            return @"/***************************************************************************************************
 * BROWSER POLYFILLS
 */

/** IE9, IE10 and IE11 requires all of the following polyfills. **/
 import 'core-js/es6/symbol';
 import 'core-js/es6/object';
 import 'core-js/es6/function';
 import 'core-js/es6/parse-int';
 import 'core-js/es6/parse-float';
 import 'core-js/es6/number';
 import 'core-js/es6/math';
 import 'core-js/es6/string';
 import 'core-js/es6/date';
 import 'core-js/es6/array';
 import 'core-js/es6/regexp';
 import 'core-js/es6/map';
 import 'core-js/es6/set';

/** IE10 and IE11 requires the following for NgClass support on SVG elements */
// import 'classlist.js';  // Run `npm install --save classlist.js`.

// import 'web-animations-js';  // Run `npm install --save web-animations-js`.

/** Evergreen browsers require these. **/
import 'core-js/es6/reflect';
import 'core-js/es7/reflect';

/**
 * Date, currency, decimal and percent pipes.
 * Needed for: All but Chrome, Firefox, Edge, IE11 and Safari 10
 */
// import 'intl';  // Run `npm install --save intl`.

import './rx-imports';";
        }
    }
}
