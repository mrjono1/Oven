using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Request
{
    public partial class Screen
    {
        /// <summary>
        /// Validate this object and reslove any issues if possible
        /// </summary>
        internal bool Validate(Project project, out string message)
        {
            var messageList = new List<string>();

            
            if (messageList.Any())
            {
                message = string.Join(Environment.NewLine, messageList);
                return false;
            }
            else
            {
                message = "Success";
                return true;
            }
        }
    }
}
