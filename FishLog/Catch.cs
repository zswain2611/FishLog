// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// Catch.cs - Represents a single fish catch

using System;

namespace FishLog
{
    public class Catch
    {
        private Species _species;
        private double _lengthCm;
        private double _weightKg;
        private CatchStatus _status;
        private DateTime _time;

        public Catch(Species species, double lengthCm, double weightKg, CatchStatus status)
        {
            _species = species;
            _lengthCm = lengthCm;
            _weightKg = weightKg;
            _status = status;
            _time = DateTime.Now;
        }

        public Species GetSpecies() => _species;
        public double GetLength() => _lengthCm;
        public double GetWeight()=> _weightKg;
        public CatchStatus GetStatus() => _status;
        public string GetSummary() => $"{_species.GetName()} {_lengthCm}cm {_status}";


    }
}
