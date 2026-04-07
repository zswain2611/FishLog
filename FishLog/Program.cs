// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// Program.cs - Main console application entry point


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    Console.WriteLine("Invalid choice. Please try again.");
                } 
            }

            return new Angler(name, license);
        }
    }
}
