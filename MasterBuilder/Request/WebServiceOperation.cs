using Humanizer;
using System;

namespace MasterBuilder.Request
{
    /// <summary>
    /// Web Service Operation
    /// </summary>
    public class WebServiceOperation
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        private string _internalName;
        /// <summary>
        /// Calculated Internal Name
        /// </summary>
        internal string InternalName
        {
            get
            {
                if (_internalName == null)
                {
                    _internalName = Title.Dehumanize();
                }
                return _internalName;
            }
        }
        /// <summary>
        /// Relative Route
        /// </summary>
        public string RelativeRoute { get; set; }
        // TODO: convert to relationship
        /// <summary>
        /// Http verb eg. GET, POST, PUT, PATCH
        /// </summary>
        public string Verb { get; set; }
    }
}
