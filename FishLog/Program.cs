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
            LogCatchFlow(trip, angler.GetLicense(), zone.Value);

            // Print trip summary
            Console.WriteLine("\n" + trip.GetSummary());
        }

        static void LogCatchFlow(Trip trip, License license, FMZone zone)
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

                Species species = speciesChoice switch
                {
                    "1" => new Walleye(),
                    "2" => new NorthernPike(),
                    "3" => new Bass(),
                    "4" => new YellowPerch(),
                    "5" => new Muskellunge(),
                    "6" => new LakeTrout(),
                    _ => null
                };

                if (species == null)
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

                CatchStatus? desiredStatus = null;
                while (desiredStatus == null)
                {
                    Console.WriteLine("\nKeep or release?");
                    Console.WriteLine("[1] Keep");
                    Console.WriteLine("[2] Release");
                    Console.Write("> ");
                    string statusChoice = Console.ReadLine();

                    desiredStatus = statusChoice switch
                    {
                        "1" => CatchStatus.Kept,
                        "2" => CatchStatus.Released,
                        _ => null
                    };

                    if (desiredStatus  == null)
                    {
                        Console.WriteLine("Invalid choice. Please enter [1] or [2].");
                    }
                }

                // Create a temporary catch for validation
                Catch tempCatch = new Catch(species, length, weight, desiredStatus.Value);

                // Get how many of a the current species has been caught so far
                int keptSoFar = trip.GetKeptCount(species.GetName());

                // Validate the catch
                ValidationResult result = species.ValidateCatch(tempCatch, license, zone, keptSoFar);

                // Handle validation result
                if (result == ValidationResult.Legal && desiredStatus.Value == CatchStatus.Kept)
                {
                    // Legal and user wants to keep it
                    trip.LogCatch(tempCatch);
                    Console.WriteLine($"\n> Logged: {tempCatch.GetSummary()}");
                }
                else if (result == ValidationResult.Legal && desiredStatus.Value == CatchStatus.Released)
                {
                    // Legal but user chose to release
                    trip.LogCatch(tempCatch);
                    Console.WriteLine($"> Logged: {tempCatch.GetSummary()}");
                }
                else
                {
                    // ILLEGAL and must release
                    Catch releasedCatch = new Catch(species, length, weight, CatchStatus.Released);
                    trip.LogCatch(releasedCatch);

                    Console.WriteLine($"\n> ERROR: {GetValidationMessage(result, species.GetName(), length)}");
                    Console.WriteLine("This fish has been recorded as RELEASED.\n");
                }
            }

            static string GetValidationMessage(ValidationResult result, string speciesName, double length)
            {
                return result switch
                {
                    ValidationResult.OutOfSeason => $"{speciesName} season is currently closed.",
                    ValidationResult.OverLimit => $"Daily limit reached for {speciesName}.",
                    ValidationResult.IllegalSize => $"{speciesName} ({length}cm) does not meet size requirements.",
                    ValidationResult.MustRelease => $"Conservation license holders must release all {speciesName}.",
                    _ => "Unknown validation error."
                };
            }
        }
    }
}
