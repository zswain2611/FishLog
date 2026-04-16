// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// Angler.cs - Represents the user profile

using System;
using System.Collections.Generic;

namespace FishLog
{
    /// <summary>
    /// Represents an angler with license information and trip history
    /// </summary>
    public class Angler
    {
        private string _name;
        private License _license;
        private List<Trip> _trips;

        /// <summary>
        /// Creates a new angler with the specified name and license
        /// </summary>
        /// <param name="name">The angler's name</param>
        /// <param name="license">The angler's fishing license</param>
        public Angler(string name, License license)
        {
            _name = name;
            _license = license;
            _trips = new List<Trip>();
        }

        /// <summary>
        /// Starts a new fishing trip in the specified zone and location
        /// </summary>
        /// <param name="zone">The fishing management zone</param>
        /// <param name="location">The location name (e.g., "Lake Nipissing")</param>
        /// <returns>The newly created trip</returns>
        public Trip StartTrip(FMZone zone, string location)
        {
            Trip newTrip = new Trip(zone, location);
            _trips.Add(newTrip);
            return newTrip;
        }

        /// <summary>
        /// Gets the angler's name
        /// </summary>
        /// <returns>The angler's name</returns>
        public string GetName() => _name;

        /// <summary>
        /// Gets the angler's fishing license
        /// </summary>
        /// <returns>The license object</returns>
        public License GetLicense() => _license;

        /// <summary>
        /// Displays statistics for this angler including species breakdown across all trips
        /// </summary>
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
