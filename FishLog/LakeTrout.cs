// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// LakeTrout.cs - Lake Trout species with different season openers per zone

using System;


namespace FishLog
{
    public class LakeTrout : Species
    {
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
