// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// ConservationLicense.cs - Conservation fishing license implementation

using System;

namespace FishLog
{
    public class ConservationLicense : License
    {
        public ConservationLicense() : base(LicenseType.Conservation) { }

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
