// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// Program.cs - Main console application entry point


using System;

namespace FishLog
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("     FishLog - Ontario Fishing Trip Logger");
            Console.WriteLine("                2026 Regulations");
            Console.WriteLine("=================================================\n");

            // Setup angler profile
            Angler angler = SetupAngler();

            Console.WriteLine($"\nWelcome, {angler.GetName()}!");
            Console.WriteLine($"License: {angler.GetLicense().GetLicenseType()}");

            //Main menu loop
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n=== MAIN MENU ===");
                Console.WriteLine("[1] Start new trip");
                Console.WriteLine("[2] View stats (Feature coming soon!)");
                Console.WriteLine("[3] Quit");
                Console.Write("> ");

                string choice = Console.ReadLine();
                    
                switch (choice)
                {
                    case "1":
                        StartTripFlow(angler);
                        break;
                    case "2":
                        Console.WriteLine("\n Stats feature coming soon!");
                        break;
                    case "3":
                        Console.WriteLine("\nThanks for using FishLog!");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid choice. Please try again.");
                        break;
                }
            }
        }

        static Angler SetupAngler()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            License license = null;
            while (license == null)
            {
                Console.WriteLine("\nSelect license type:");
                Console.WriteLine("[1] Sport");
                Console.WriteLine("[2] Conservation");
                Console.Write("> ");
                string licenseChoice = Console.ReadLine();

                license = licenseChoice switch
                {
                    "1" => new SportLicense(),
                    "2" => new ConservationLicense(),
                    _ => null
                };

                if (license == null)
                {
                    Console.WriteLine("Invalid license choice. Please try again.");
                }
            }

            return new Angler(name, license);
        }

        static void StartTripFlow(Angler angler)
        {
            Console.WriteLine("\n=== START NEW TRIP ===");

            FMZone? zone = null;
            while (zone == null)
            {
                Console.WriteLine("Select zone:");
                Console.WriteLine("[1] FMZ 10");
                Console.WriteLine("[2] FMZ 11");
                Console.Write("> ");
                string zoneChoice = Console.ReadLine();

                zone = zoneChoice switch
                {
                    "1" => FMZone.FMZ10,
                    "2" => FMZone.FMZ11,
                    _ => null
                };

                if (zone == null)
                {
                    Console.WriteLine("Invalid zone choice. Please try again.");
                }
            }

            Console.WriteLine("Enter location (e.g., Lake Nipissing): ");
            string location = Console.ReadLine();

            Console.WriteLine($"\nNOTE: {zone} is part of the Northeast Bait Management Zone.");
            Console.WriteLine("Live or dead baitfish and leeches may not be transported in or out.\n");

            // Start the trip
            Trip trip = angler.StartTrip(zone.Value, location);

            // Catch logging loop
            LogCatchFlow(trip, angler.GetLicense());

            // Print trip summary
            Console.WriteLine("\n" + trip.GetSummary());
        }

        static void LogCatchFlow(Trip trip, License license)
        {
            bool loggingCatches = true;

            while (loggingCatches)
            {
                Console.WriteLine("\n=== LOG A CATCH ===");
                Console.WriteLine("[1] Walleye");
                Console.WriteLine("[2] Northern Pike");
                Console.WriteLine("[3] Bass (Largemouth/Smallmouth)");
                Console.WriteLine("[4] Yellow Perch");
                Console.WriteLine("[5] Muskellunge");
                Console.WriteLine("[6] Lake Trout");
                Console.WriteLine("[7] End trip");
                Console.Write("> ");

                string speciesChoice = Console.ReadLine();

                if (speciesChoice == "7")
                { 
                    loggingCatches = false;
                    continue;
                }

                string species = speciesChoice switch
                {
                    "1" => "Walleye",
                    "2" => "Pike",
                    "3" => "Bass",
                    "4" => "Perch",
                    "5" => "Muskie",
                    "6" => "LakeTrout",
                    _ => ""
                };

                if (species == "")
                {
                    Console.WriteLine("Invalid species choice. Please try again.");
                    continue;
                }

                Console.Write("Length (cm): ");
                if (!double.TryParse(Console.ReadLine(), out double length))
                {
                    Console.WriteLine("Invalid length. Please enter a number.");
                    continue;
                }

                Console.Write("Weight (kg, optional - press Enter to skip): ");
                string weightInput = Console.ReadLine();
                double weight = 0;
                if (!string.IsNullOrEmpty(weightInput))
                {
                    double.TryParse(weightInput, out weight);
                }

                Console.WriteLine("\nKeep or release?");
                Console.WriteLine("[1] Keep");
                Console.WriteLine("[2] Release");
                Console.Write("> ");
                string statusChoice = Console.ReadLine();

                CatchStatus status = statusChoice switch
                {
                    "1" => CatchStatus.Kept,
                    "2" => CatchStatus.Released,
                    _ => CatchStatus.Released
                };

                // Create and log the catch
                Catch newCatch = new Catch(species , length, weight, status);
                trip.LogCatch(newCatch);

                Console.WriteLine($"\n Logged: {newCatch.GetSummary()}");
            }
        }
    }
}
