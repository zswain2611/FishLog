// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// NorthernPike.cs - Northern Pike species with validation

using System;

namespace FishLog
{
    /// <summary>
    /// Represents a northern pike with zone-specific size and season restrictions
    /// </summary>
    public class NorthernPike : Species
    {
        /// <summary>
        /// Initializes a new northern pike with FMZ 10 and FMZ 11 regulation rules
        /// </summary>
        public NorthernPike() : base("Northern Pike")
        {
            // FMZ 10 Regulations
            _rules[FMZone.FMZ10] = new RegulationRule(
                seasonOpen: DateTime.MinValue,
                seasonClose: DateTime.MinValue,
                sportLimit: 6,
                conservLimit: 2,
                maxSizeCm: 86,
                maxOverSizeCount: 1
            );

            // FMZ 11 Regulations
            _rules[FMZone.FMZ11] = new RegulationRule(
                seasonOpen: new DateTime(2026, 6, 1),
                seasonClose: new DateTime(2027, 3, 31),
                sportLimit: 6,
                conservLimit: 2,
                maxSizeCm: 86,
                maxOverSizeCount: 1

            );
        }

        /// <summary>
        /// Validates a northern pike catch against zone-specific regulations
        /// </summary>
        /// <param name="fish">The catch to validate</param>
        /// <param name="license">The angler's license type</param>
        /// <param name="zone">The fishing management zone</param>
        /// <param name="keptSoFar">Number of northern pike already kept on this trip</param>
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

            // Conservation: none over 61cm allowed
            if (license.GetLicenseType() == LicenseType.Conservation && fish.GetLength() > 61)
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
