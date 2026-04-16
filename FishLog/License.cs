// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// License.cs - Abstract base class for Sport and Conservation licenses

using System;

namespace FishLog
{
    /// <summary>
    /// Abstract base class for all fishing license types
    /// </summary>
    public abstract class License
    {
        protected LicenseType _type;

        /// <summary>
        /// Creates a new license with the specified type
        /// </summary>
        /// <param name="type">The license type (Sport or Conservation)</param>
        public License(LicenseType type) 
        {
            _type = type;
        }

        /// <summary>
        /// Gets the daily catch limit for a specific species in a zone
        /// </summary>
        /// <param name="species">The species name</param>
        /// <param name="zone">The fishing management zone</param>
        /// <returns>Daily catch limit</returns>
        public abstract int GetLimit(string species, FMZone zone);

        /// <summary>
        /// Gets the type of this license
        /// </summary>
        /// <returns>License type (Sport or Conservation)</returns>
        public LicenseType GetLicenseType() => _type;
    }
}
