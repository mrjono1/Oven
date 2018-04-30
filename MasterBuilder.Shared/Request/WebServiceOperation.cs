using Humanizer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MasterBuilder.Request
{
    /// <summary>
    /// Web Service Operation
    /// </summary>
    public class WebServiceOperation
    {
        /// <summary>
        /// Register of all Screen Type Ids to Enum for easy use
        /// </summary>
        internal static readonly Dictionary<Guid, HttpVerb> HttpVerbDictonary = new Dictionary<Guid, HttpVerb>
        {
            { Guid.Empty, HttpVerb.None},
            { new Guid("{E057D18F-9E74-4C0E-A2A0-58029354AD66}"), HttpVerb.Get },
            { new Guid("{A6DCEFE6-4DA6-4952-96DC-6B1D7EFE2FF1}"), HttpVerb.Post },
            { new Guid("{DDD557C8-3E4C-4E9E-8F83-88BEE5A67CA6}"), HttpVerb.Put },
            { new Guid("{A60BE34E-7505-483D-8888-69C406E82F80}"), HttpVerb.Delete }
        };

        /// <summary>
        /// Identifier
        /// </summary>
        [Required]
        [NonDefault]
        public Guid Id { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        private string internalName;
        /// <summary>
        /// Calculated Internal Name
        /// </summary>
        internal string InternalName
        {
            get
            {
                if (internalName == null)
                {
                    internalName = Title.Dehumanize();
                }
                return internalName;
            }
        }
        /// <summary>
        /// Relative Route
        /// </summary>
        public string RelativeRoute { get; set; }
        /// <summary>
        /// Http verb eg. GET, POST, PUT, PATCH
        /// </summary>
        public Guid? VerbId { get; set; }
        /// <summary>
        /// Http verb eg. GET, POST, PUT, PATCH
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public HttpVerb Verb
        {
            get
            {
                var id = VerbId ?? Guid.Empty;
                return HttpVerbDictonary[id];
            }
            set
            {
                var id = HttpVerbDictonary.SingleOrDefault(v => v.Value == value).Key;
                if (id == Guid.Empty)
                {
                    VerbId = null;
                }
                else
                {
                    VerbId = id;
                }
            }
        }
    }
}
