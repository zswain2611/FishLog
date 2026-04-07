// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// SportLicense.cs - Sport fishing license implementation

using System;

namespace FishLog
{
    public class SportLicense : License
    {
        public SportLicense(DateTime expiry) : base(LicenseType.Sport, expiry) { }

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
