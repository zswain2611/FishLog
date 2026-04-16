// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// YellowPerch.cs - Yellow Perch species with validation

using System;

namespace FishLog
{
    /// <summary>
    /// Represents a yellow perch with open season and high catch limits
    /// </summary>
    public class YellowPerch : Species
    {
        /// <summary>
        /// Initializes a new yellow perch with FMZ 10 and FMZ 11 regulation rules
        /// </summary>
        public YellowPerch() : base("Yellow Perch")
        {
            // FMZ 10 Regulations
            _rules[FMZone.FMZ10] = new RegulationRule(
                seasonOpen: DateTime.MinValue,
                seasonClose: DateTime.MinValue,
                sportLimit: 50,
                conservLimit: 25
                // No size limit
            );

            // FMZ 11 Regulations
            _rules[FMZone.FMZ11] = new RegulationRule(
                seasonOpen: DateTime.MinValue,
                seasonClose: DateTime.MinValue,
                sportLimit: 50,
                conservLimit: 25
            );
        }

        /// <summary>
        /// Validates a yellow perch catch against zone-specific regulations
        /// </summary>
        /// <param name="fish">The catch to validate</param>
        /// <param name="license">The angler's license type</param>
        /// <param name="zone">The fishing management zone</param>
        /// <param name="keptSoFar">Number of yellow perch already kept on this trip</param>
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
