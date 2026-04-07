// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// Enums.cs - Core enumeration types for the application

using System;

namespace FishLog
{

    public enum LicenseType
    {
        Sport,
        Conservation
    }

    public enum FMZone
    {
        FMZ10,
        FMZ11
    }

    public enum CatchStatus
    {
        Kept,
        Released
    }

    public enum ValidationResult
    {
        Legal,
        OutOfSeason,
        OverLimit,
        IllegalSize,
        MustRelease
    }
}
