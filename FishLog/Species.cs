// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// Species.cs - Abstract base class for all fish species

using System;
using System.Collections.Generic;

namespace FishLog
{
    public abstract class Species
    {
        protected string _commonName;
        protected Dictionary<FMZone, RegulationRule> _rules;

        public Species(string commonName)
        {
            _commonName = commonName;
            _rules = new Dictionary<FMZone, RegulationRule>();
        }

        // Abstract method: Each species implements its own validation logic
        public abstract ValidationResult ValidateCatch(Catch fish, License license, FMZone zone, int keptSoFar);

        // Helper Methods
        public RegulationRule GetRule(FMZone zone)
        {
            return _rules[zone];
        }

        public bool IsInSeason(FMZone zone, DateTime date)
        {
            return GetRule(zone).IsOpen(date);
        }

        public string GetName()
        {
            return _commonName;
        }
    }
}
