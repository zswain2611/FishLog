// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// Bass.cs - Bass species (Largemouth and Smallmouth combined limit)

using System;


namespace FishLog
{
    /// <summary>
    /// Represents a bass (largemouth/smallmouth) with combined catch limits
    /// </summary>
    public class Bass : Species
    {
        /// <summary>
        /// Initializes a new bass with FMZ 10 and FMZ 11 regulation rules
        /// </summary>
        public Bass() : base("Bass")
        {
            // FMZ 10 Regulations
            _rules[FMZone.FMZ10] = new RegulationRule(
                seasonOpen: DateTime.MinValue,
                seasonClose: DateTime.MinValue,
                sportLimit: 6,
                conservLimit: 3
            // No size limit
            );

            // FMZ 11 Regulations
            _rules[FMZone.FMZ11] = new RegulationRule(
                seasonOpen: DateTime.MinValue,
                seasonClose: DateTime.MinValue,
                sportLimit: 6,
                conservLimit: 2
            );
        }

        /// <summary>
        /// Validates a bass catch against zone-specific regulations
        /// </summary>
        /// <param name="fish">The catch to validate</param>
        /// <param name="license">The angler's license type</param>
        /// <param name="zone">The fishing management zone</param>
        /// <param name="keptSoFar">Number of bass already kept on this trip</param>
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
