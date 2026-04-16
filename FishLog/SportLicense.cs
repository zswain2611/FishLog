// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// SportLicense.cs - Sport fishing license implementation

using System;

namespace FishLog
{
    /// <summary>
    /// Represents a Sport fishing license with higher catch limits
    /// </summary>
    public class SportLicense : License
    {
        /// <summary>
        /// Initializes a new Sport fishing license
        /// </summary>
        public SportLicense() : base(LicenseType.Sport) { }

        /// <summary>
        /// Gets the daily catch limit for Sport license
        /// </summary>
        /// <returns>Daily catch limit</returns>
        public override int GetLimit(string species, FMZone zone)
        {
            return species.ToLower() switch
            {
                "walleye" => 4,
                "pike" => 6,
                "bass" => 6,
                "perch" => 50,
                "laketrout" => 2,
                "muskie" => 1,
                _ => 0
            };
        }
    }
}
