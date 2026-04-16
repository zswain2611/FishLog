// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// ConservationLicense.cs - Conservation fishing license implementation

using System;

namespace FishLog
{
    /// <summary>
    /// Represents a Conservation fishing license with reduced catch limits
    /// </summary>
    public class ConservationLicense : License
    {
        /// <summary>
        /// Initializes a new Conservation fishing license
        /// </summary>
        public ConservationLicense() : base(LicenseType.Conservation) { }

        /// <summary>
        /// Gets the daily catch limit for Conservation license for a specific species
        /// </summary>
        /// <param name="species">The species name</param>
        /// <param name="zone">The fishing management zone</param>
        /// <returns>Daily catch limit for that species (0 for restricted species like muskellunge)</returns>
        public override int GetLimit(string species, FMZone zone)
        {
            return species.ToLower() switch
            {
                "walleye" => 2,
                "pike" => 2,
                "bass" => (zone == FMZone.FMZ10) ? 3 : 2,
                "perch" => 25,
                "laketrout" => 1,
                "muskie" => 0,
                _ => 0
            };
        }
    }
}
