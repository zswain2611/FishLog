// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// Species.cs - Abstract base class for all fish species

using System;
using System.Collections.Generic;

namespace FishLog
{
    /// <summary>
    /// Abstract base class for all fish species with zone-specific regulation rules
    /// </summary>
    public abstract class Species
    {
        protected string _commonName;
        protected Dictionary<FMZone, RegulationRule> _rules;

        /// <summary>
        /// Creates a new species with the specified common name
        /// </summary>
        /// <param name="commonName">The common name of the species (e.g., "Walleye")</param>
        public Species(string commonName)
        {
            _commonName = commonName;
            _rules = new Dictionary<FMZone, RegulationRule>();
        }

        /// <summary>
        /// Validates a catch against zone-specific regulations
        /// </summary>
        /// <param name="fish">The catch to validate</param>
        /// <param name="license">The angler's license type</param>
        /// <param name="zone">The fishing management zone</param>
        /// <param name="keptSoFar">Number of this species already kept on the trip</param>
        /// <returns>Legal if catch meets all requirements, otherwise specific violation reason</returns>
        public abstract ValidationResult ValidateCatch(Catch fish, License license, FMZone zone, int keptSoFar);

        /// <summary>
        /// Gets the regulation rule for the specified zone
        /// </summary>
        /// <param name="zone">The fishing management zone</param>
        /// <returns>Regulation rule for that zone</returns>
        public RegulationRule GetRule(FMZone zone)
        {
            return _rules[zone];
        }

        /// <summary>
        /// Checks if the species is in season for the given zone and date
        /// </summary>
        /// <param name="zone">The fishing management zone</param>
        /// <param name="date">The date to check</param>
        /// <returns>True if in season, false if closed</returns>
        public bool IsInSeason(FMZone zone, DateTime date)
        {
            return GetRule(zone).IsOpen(date);
        }

        /// <summary>
        /// Gets the common name of the species
        /// </summary>
        /// <returns>Common name (e.g., "Walleye")</returns>
        public string GetName()
        {
            return _commonName;
        }
    }
}
