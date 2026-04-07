// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// Trip.cs - Represents a fishing trip with catch logging

using System;
using System.Collections.Generic;


namespace FishLog
{
    public class Trip
    {
        private DateTime _date;
        private FMZone _zone;
        private string _location;
        private List<Catch> _catches;

        public Trip(FMZone zone, string location)
        {
            _date = DateTime.Now;
            _zone = zone;
            _location = location;
            _catches = new List<Catch>();
        }

        public void LogCatch(Catch fish)
        {
            _catches.Add(fish);
        }

        public int GetKeptCount(string species)
        {
            int count = 0;
            foreach (Catch c in _catches)
            {
                if (c.GetSpecies().ToLower() == species.ToLower() &&
                    c.GetStatus() == CatchStatus.Kept)
                {
                    count++;
                }
            }
            return count;
        }

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
            string summary = $"=== TRIP SUMMARY - {_location} | {_zone}\n";
            summary += $"Total catches: {_catches.Count} | Kept: {totalKept} | Released: {totalReleased}";

            foreach (Catch c in _catches)
            {
                summary += $"{c.GetSummary()}\n";
            }

            return summary;
        }
    }
}
