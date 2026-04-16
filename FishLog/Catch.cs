// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// Catch.cs - Represents a single fish catch

using System;

namespace FishLog
{
    /// <summary>
    /// Represents a single fish catch with species, size, and status information
    /// </summary>
    public class Catch
    {
        private Species _species;
        private double _lengthCm;
        private double _weightKg;
        private CatchStatus _status;
        private DateTime _time;

        /// <summary>
        /// Creates a new catch record with the specified details
        /// </summary>
        /// <param name="species">The species of fish caught</param>
        /// <param name="lengthCm">Length of the fish in centimeters</param>
        /// <param name="weightKg">Weight of the fish in kilograms</param>
        /// <param name="status">Whether the fish was kept or released</param>
        public Catch(Species species, double lengthCm, double weightKg, CatchStatus status)
        {
            _species = species;
            _lengthCm = lengthCm;
            _weightKg = weightKg;
            _status = status;
            _time = DateTime.Now;
        }

        /// <summary>
        /// Gets the species of this catch
        /// </summary>
        /// <returns>The species object</returns>
        public Species GetSpecies() => _species;

        /// <summary>
        /// Gets the length of this catch in centimeters
        /// </summary>
        /// <returns>Length in cm</returns>
        public double GetLength() => _lengthCm;

        /// <summary>
        /// Gets the weight of this catch in kilograms
        /// </summary>
        /// <returns>Weight in kg</returns>
        public double GetWeight()=> _weightKg;

        /// <summary>
        /// Gets the status of this catch (kept or released)
        /// </summary>
        /// <returns>Catch status</returns>
        public CatchStatus GetStatus() => _status;

        /// <summary>
        /// Gets a formatted summary of this catch
        /// </summary>
        /// <returns>Summary string (e.g., "Walleye 45cm Kept")</returns>
        public string GetSummary() => $"{_species.GetName()} {_lengthCm}cm {_status}";


    }
}
