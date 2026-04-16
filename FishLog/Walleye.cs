// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// Walleye.cs - Walleye species with validation

using System;

namespace FishLog
{
    /// <summary>
    /// Represents a walleye with zone-specific size and slot restrictions
    /// </summary>
    public class Walleye : Species
    {
        /// <summary>
        /// Initializes a new walleye with FMZ 10 and FMZ 11 regulation rules
        /// </summary>
        public Walleye() : base("Walleye")
        {
            // FMZ 10 Regulations
            _rules[FMZone.FMZ10] = new RegulationRule(
                seasonOpen: DateTime.MinValue,
                seasonClose: DateTime.MinValue,
                sportLimit: 4,
                conservLimit: 2,
                maxSizeCm: 46
            );

            // FMZ 11 Regulations
            _rules[FMZone.FMZ11] = new RegulationRule(
                seasonOpen: DateTime.MinValue,
                seasonClose: DateTime.MinValue,
                sportLimit: 4,
                conservLimit: 2,
                slotMinCm: 43,
                slotMaxCm: 60,
                maxOverSizeCount: 1

            );
        }

        /// <summary>
        /// Validates a walleye catch against zone-specific regulations
        /// </summary>
        /// <param name="fish">The catch to validate</param>
        /// <param name="license">The angler's license type</param>
        /// <param name="zone">The fishing management zone</param>
        /// <param name="keptSoFar">Number of walleye already kept on this trip</param>
        /// <returns>Legal if catch meets all requirements, otherwise specific violation reason</returns>
        public override ValidationResult ValidateCatch(Catch fish, License license, FMZone zone, int keptSoFar)
        {
            RegulationRule rule = GetRule(zone);

            // Check if season is open
            if (!IsInSeason(zone, DateTime.Now))
            {
                return ValidationResult.OutOfSeason;
            }

            // Check size restriction
            if (!rule.PassesSizeCheck(fish.GetLength()))
            {
                return ValidationResult.IllegalSize;
            }

            // Check daily limit
            int dailyLimit = rule.GetLimit(license.GetLicenseType());
            if (keptSoFar >= dailyLimit)
            {
                return ValidationResult.OverLimit;
            }

            return ValidationResult.Legal;
        }
    }
}
