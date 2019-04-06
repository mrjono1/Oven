using Humanizer;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Oven.Request
{
    /// <summary>
    /// Web Service Operation
    /// </summary>
    public class WebServiceOperation
    {
        /// <summary>
        /// Register of all Screen Type Ids to Enum for easy use
        /// </summary>
        internal static readonly Dictionary<ObjectId, HttpVerb> HttpVerbDictonary = new Dictionary<ObjectId, HttpVerb>
        {
            { ObjectId.Empty, HttpVerb.None},
            { new ObjectId("5ca8770e4a73264e4c06e033"), HttpVerb.Get },
            { new ObjectId("5ca8770f4a73264e4c06e034"), HttpVerb.Post },
            { new ObjectId("5ca8770f4a73264e4c06e035"), HttpVerb.Put },
            { new ObjectId("5ca8770f4a73264e4c06e036"), HttpVerb.Delete }
        };

        /// <summary>
        /// Identifier
        /// </summary>
        [Required]
        [NonDefault]
        public ObjectId Id { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        private string internalName;
        /// <summary>
        /// Calculated Internal Name
        /// </summary>
        [JsonIgnore]
        public string InternalName
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
        public ObjectId? VerbId { get; set; }
        /// <summary>
        /// Http verb eg. GET, POST, PUT, PATCH
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public HttpVerb Verb
        {
            get
            {
                var id = VerbId ?? ObjectId.Empty;
                return HttpVerbDictonary[id];
            }
            set
            {
                var id = HttpVerbDictonary.SingleOrDefault(v => v.Value == value).Key;
                if (id == ObjectId.Empty)
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
