// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// License.cs - Abstract base class for Sport and Conservation licenses

using System;

namespace FishLog
{
    public abstract class License
    {
        protected LicenseType _type;

        public License(LicenseType type) 
        {
            _type = type;
        }

        public abstract int GetLimit(string species, FMZone zone);

        public LicenseType GetLicenseType() => _type;
    }
}
