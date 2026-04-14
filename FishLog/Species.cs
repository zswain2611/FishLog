using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishLog
{
    public abstract class Species
    {
        protected string _commonName;
        protected Dictionary<FMZone, RegulationRule> _rule;

        public Species(string commonName)
        {
            _commonName = commonName;
            _rule = new Dictionary<FMZone, RegulationRule>();
        }

        // Abstract method: Each species implements its own validation logic
        public abstract ValidationResult ValidateCatch(Catch fish, License liscense, FMZone zone, int KeptSoFar);

        // Helper Methods
        public RegulationRule GetRule(FMZone zone)
        {
            return _rule[zone];
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
