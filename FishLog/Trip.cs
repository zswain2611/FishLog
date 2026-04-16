// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// Trip.cs - Represents a fishing trip with catch logging

using System;
using System.Collections.Generic;


namespace FishLog
{
    /// <summary>
    /// Represents a fishing trip with zone, location, and catch records
    /// </summary>
    public class Trip
    {
        private DateTime _date;
        private FMZone _zone;
        private string _location;
        private List<Catch> _catches;

        /// <summary>
        /// Creates a new fishing trip in the specified zone and location
        /// </summary>
        /// <param name="zone">The fishing management zone</param>
        /// <param name="location">The location name (e.g., "Lake Nipissing")</param>
        public Trip(FMZone zone, string location)
        {
            _date = DateTime.Now;
            _zone = zone;
            _location = location;
            _catches = new List<Catch>();
        }

        /// <summary>
        /// Logs a catch to this trip's record
        /// </summary>
        /// <param name="fish">The catch to log</param>
        public void LogCatch(Catch fish)
        {
            _catches.Add(fish);
        }

        /// <summary>
        /// Gets the number of kept catches for a specific species on this trip
        /// </summary>
        /// <param name="species">The species name</param>
        /// <returns>Count of kept fish of that species</returns>
        public int GetKeptCount(string species)
        {
            int count = 0;
            foreach (Catch c in _catches)
            {
                if (c.GetSpecies().GetName().ToLower() == species.ToLower() &&
                    c.GetStatus() == CatchStatus.Kept)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Generates a formatted summary of this trip
        /// </summary>
        /// <returns>Trip summary with location, zone, totals, and catch list</returns>
        public string GetSummary()
        {
            int totalKept = 0;
            int totalReleased = 0;

            foreach (Catch c in _catches)
            {
                if (c.GetStatus() == CatchStatus.Kept)
                    totalKept++;
                else
                    totalReleased++;
            }
            string summary = "=== TRIP SUMMARY ===\n\n";
                 summary += $"{_location} | {_zone}\n\n";
            summary += $"Total catches: {_catches.Count} | Kept: {totalKept} | Released: {totalReleased}\n";

            foreach (Catch c in _catches)
            {
                summary += $"  {c.GetSummary()}\n";
            }

            return summary;
        }

        /// <summary>
        /// Gets the total number of kept catches on this trip
        /// </summary>
        /// <returns>Total catch count</returns>
        public int GetTotalCatches()
        {
            return _catches.Count;
        }

        /// <summary>
        /// Gets the total number of kept catches on this trip
        /// </summary>
        /// <returns>Kept catch count</returns>
        public int GetTotalKept()
        {
            int count = 0;
            foreach (Catch c in _catches)
            {
                if (c.GetStatus() == CatchStatus.Kept)
                    count++;
            }
            return count;
        }

        /// <summary>
        /// Gets the total number of released catches on this trip
        /// </summary>
        /// <returns>Released catch count</returns>
        public int GetTotalReleased()
        {
            int count = 0;
            foreach (Catch c in _catches)
            {
                if (c.GetStatus() == CatchStatus.Released)
                    count++;
            }
            return count;
        }

        /// <summary>
        /// Get all the catches from this trip
        /// </summary>
        /// <returns>List of all catch records</returns>
        public List<Catch> GetAllCatches()
        {
            return _catches;
        }
    }
}
