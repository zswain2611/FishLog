// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// LakeTrout.cs - Lake Trout species with different season openers per zone

using System;


namespace FishLog
{
    /// <summary>
    /// Represents a lake trout with zone-specific opening dates and oversize restrictions
    /// </summary>
    public class LakeTrout : Species
    {
        /// <summary>
        /// Initializes a new lake trout with FMZ 10 and FMZ 11 regulation rules
        /// </summary>
        public LakeTrout() : base("Lake Trout")
        {
            // FMZ 10 Regulations
            _rules[FMZone.FMZ10] = new RegulationRule(
                seasonOpen: new DateTime(2026, 1, 1),
                seasonClose: DateTime.MinValue,
                sportLimit: 2,
                conservLimit: 1,
                maxOverSizeCount: 1
            // No size limit
            );

            // FMZ 11 Regulations
            _rules[FMZone.FMZ11] = new RegulationRule(
                seasonOpen: new DateTime(2026, 2, 15),
                seasonClose: DateTime.MinValue,
                sportLimit: 2,
                conservLimit: 1,
                maxOverSizeCount: 1
            );
        }

        /// <summary>
        /// Validates a lake trout catch against zone-specific regulations
        /// </summary>
        /// <param name="fish">The catch to validate</param>
        /// <param name="license">The angler's license type</param>
        /// <param name="zone">The fishing management zone</param>
        /// <param name="keptSoFar">Number of lake trout already kept on this trip</param>
        /// <returns>Legal if catch meets all requirements, otherwise specific violation reason</returns>
        public override ValidationResult ValidateCatch(Catch fish, License license, FMZone zone, int keptSoFar)
        {
            RegulationRule rule = GetRule(zone);

            // Check if season is open
            if (!IsInSeason(zone, DateTime.Now))
            {
                return ValidationResult.OutOfSeason;
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
