// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// Angler.cs - Represents the user profile

using System;
using System.Collections.Generic;

namespace FishLog
{
    public class Angler
    {
        private string _name;
        private License _license;
        private List<Trip> _trips;

        public Angler(string name, License license)
        {
            _name = name;
            _license = license;
            _trips = new List<Trip>();
        }

        public Trip StartTrip(FMZone zone, string location)
        {
            Trip newTrip = new Trip(zone, location);
            _trips.Add(newTrip);
            return newTrip;
        }

        public string GetName() => _name;
        public License GetLicense() => _license;

        public void ViewStats()
        {
            Console.WriteLine("\n=== ANGLER STATS ===");
            Console.WriteLine($"Name: {_name}");
            Console.WriteLine($"License: {_license.GetLicenseType()}");

            Console.WriteLine($"\nTotal trips: {_trips.Count}");

            // Aggregate catches across all trips
            int totalCatches = 0;
            int totalKept = 0;
            int totalReleased = 0;

            // Dictionary to track species
            Dictionary<string, (int caught, int kept, int released)> speciesStats =
                new Dictionary<string, (int, int, int)>();

            foreach (Trip trip in _trips)
            {
                totalCatches += trip.GetTotalCatches();
                totalKept += trip.GetTotalKept();
                totalReleased += trip.GetTotalReleased();

                // Count each species
                foreach (Catch c in trip.GetAllCatches())
                {
                    string speciesName = c.GetSpecies().GetName();

                    if (!speciesStats.ContainsKey(speciesName))
                    {
                        speciesStats[speciesName] = (0, 0, 0);
                    }

                    var current = speciesStats[speciesName];
                    int caught = current.caught + 1;
                    int kept = current.kept + (c.GetStatus() == CatchStatus.Kept ? 1 : 0);
                    int released = current.released + (c.GetStatus() == CatchStatus.Released ? 1 : 0);

                    speciesStats[speciesName] = (caught, kept, released);
                }
            }
            Console.WriteLine($"Total catches: {totalCatches}");
            
            // Display species breakdown
            if (speciesStats.Count > 0)
            {
                Console.WriteLine("\nSpecies breakdown:");
                foreach (var species in speciesStats)
                {
                    Console.WriteLine($"   {species.Key}: {species.Value.caught} caught "  +
                                    $"({species.Value.kept} kept, {species.Value.released} released.)");
                }
            }
            else
            {
                Console.WriteLine("\nNo catches logged yet. Start a trip to begin tracking!");
            }
            
        }
    }
}
