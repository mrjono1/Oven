using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Oven.Request
{
    /// <summary>
    /// Specifies a NuGet package source location to be included in the NuGet.config file.
    /// </summary>
    public partial class NuGetPackageSource
    {
        /// <summary>
        /// Uniqueidentifier
        /// </summary>
        [Required]
        [NonDefault]
        public ObjectId Id { get; set; }

        /// <summary>
        /// The local name of the package source.
        /// </summary>
        [Required]
        public string Key { get; set; }

        /// <summary>
        /// The URI of the package source
        /// </summary>
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// Use an ApiKey for sources that use an Api key as set with the nuget setapikey command. Only required if pushing to this <see cref="NuGetPackageSource"/> and not using a password.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Use a username for package source, only required if pushing to this <see cref="NuGetPackageSource"/>.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Use a clear text password for package source, only required if pushing to this <see cref="NuGetPackageSource"/> and not using <see cref="ApiKey"/>.
        /// </summary>
        public string ClearTextPassword { get; set; }

    }
}
