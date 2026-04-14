// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// Walleye.cs - Walleye species with validation

using System;

namespace FishLog
{
    public class Walleye : Species
    {
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
