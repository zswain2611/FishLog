// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// Muskellunge.cs - Muskellunge species with strict size and license restrictions

using System;

namespace FishLog
{
    /// <summary>
    /// Represents a muskellunge with strict size requirements and conservation restrictions
    /// </summary>
    public class Muskellunge : Species
    {
        /// <summary>
        /// Initializes a new muskellunge with FMZ 10 and FMZ 11 regulation rules
        /// </summary>
        public Muskellunge() : base("Muskellunge")
        {
            // FMZ 10 Regulations
            _rules[FMZone.FMZ10] = new RegulationRule(
                seasonOpen: DateTime.MinValue,
                seasonClose: DateTime.MinValue,
                sportLimit: 1,
                conservLimit: 0,
                minSizeCm: 122
            );

            // FMZ 11 Regulations
            _rules[FMZone.FMZ11] = new RegulationRule(
                seasonOpen: DateTime.MinValue,
                seasonClose: DateTime.MinValue,
                sportLimit: 1,
                conservLimit: 0,
                minSizeCm: 122
            );
        }

        /// <summary>
        /// Validates a muskellunge catch against zone-specific regulations
        /// </summary>
        /// <param name="fish">The catch to validate</param>
        /// <param name="license">The angler's license type</param>
        /// <param name="zone">The fishing management zone</param>
        /// <param name="keptSoFar">Number of muskellunge already kept on this trip</param>
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

            // Conservation: none allowed
            if (license.GetLicenseType() == LicenseType.Conservation)
            {
                return ValidationResult.MustRelease;
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
