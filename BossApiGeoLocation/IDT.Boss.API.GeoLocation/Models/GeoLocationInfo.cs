// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeoLocationInfo.cs" company="">
//
// </copyright>
// <summary>
//   The GEO location info.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IDT.Boss.API.GeoLocation.Models
{
    /// <summary>
    /// Class to handle Geo location info.
    /// </summary>
    public sealed class GeoLocationInfo
    {
        /// <summary>
        /// The city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        ///The IP address.
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// The state.
        /// </summary>
        public string State { get; set; }
    }
}