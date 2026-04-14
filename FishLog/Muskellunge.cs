// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// Muskellunge.cs - Muskellunge species with strict size and license restrictions

using System;

namespace FishLog
{
    public class Muskellunge : Species
    {
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
