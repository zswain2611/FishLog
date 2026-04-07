// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// License.cs - Abstract base class for Sport and Conservation licenses

using System;

namespace FishLog
{
    public abstract class License
    {
        protected LicenseType _type;
        protected DateTime _expiry;

        public License(LicenseType type, DateTime expiry) 
        {
            _type = type;
            _expiry = expiry;
        }

        public abstract int GetLimit(string species, FMZone zone);

        public LicenseType GetLicenseType() => _type;
        public bool IsValid() => _expiry > DateTime.Now;
    }
}
