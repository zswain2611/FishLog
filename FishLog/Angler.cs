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
    }
}
