// FishLog - Ontario Fishing Trip Logger
// Zach Swain
// RegulationRule.cs - Stores fishing regulation data for a species in a specific zone


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishLog
{
    /// <summary>
    /// Stores fishing regulation data for a specific species in a specific FMZ
    /// </summary>
    public class RegulationRule
    {
        private DateTime _seasonOpen;
        private DateTime _seasonClose;
        private int _sportLimit;
        private int _conservLimit;
        private double? _minSizeCm;
        private double? _maxSizeCm;
        private double? _slotMinCm;
        private double? _slotMaxCm;
        private int? _maxOverSizeCount;

        /// <summary>
        /// Creates a new regulation rule with season dates, limits and size restrictions
        /// </summary>
        /// <param name="seasonOpen">Season opening date (MinValue for year-round)</param>
        /// <param name="seasonClose">Season closing date (MinValue for no close)</param>
        /// <param name="sportLimit">Daily limit for Sport License</param>
        /// <param name="conservLimit">Daily limit for Conservation License</param>
        /// <param name="minSizeCm">Minimum legal size in cm (null if none)</param>
        /// <param name="maxSizeCm">Maximum legal size in cm (null if none)</param>
        /// <param name="slotMinCm">Protected slot minimum size (null if no slot)</param>
        /// <param name="slotMaxCm">Protected slot maximum size (null if no slot)</param>
        /// <param name="maxOverSizeCount">Maximum number of oversize fish allowed (null if no limit)</param>
        public RegulationRule(
            DateTime seasonOpen,
            DateTime seasonClose,
            int sportLimit,
            int conservLimit,
            double? minSizeCm = null,
            double? maxSizeCm = null,
            double? slotMinCm = null,
            double? slotMaxCm = null,
            int? maxOverSizeCount = null
            )
        {
            _seasonOpen = seasonOpen;
            _seasonClose = seasonClose;
            _sportLimit = sportLimit;
            _conservLimit = conservLimit;
            _minSizeCm = minSizeCm;
            _maxSizeCm = maxSizeCm;
            _slotMinCm = slotMinCm;
            _slotMaxCm = slotMaxCm;
            _maxOverSizeCount = maxOverSizeCount;
        }

        
        /// <summary>
        /// Checks if the fishing season is open for the given date
        /// </summary>
        /// <param name="date">The date to check</param>
        /// <returns>True if season is open, false if closed</returns>
        public bool IsOpen(DateTime date)
        {
            // Open all year if both are default
            if (_seasonOpen == DateTime.MinValue && _seasonClose == DateTime.MinValue)
                return true;

            // Open year-round after a specific opening date
            if (_seasonClose == DateTime.MinValue && _seasonOpen != DateTime.MinValue)
                return date >= _seasonOpen;

            // Check if date is within season
            return date >= _seasonOpen && date <= _seasonClose;
        }

        /// <summary>
        /// Validates if a fish's length meets size requirements
        /// </summary>
        /// <param name="lengthCm">Length of the fish in cm</param>
        /// <returns>True if size is legal, false if not</returns>
        public bool PassesSizeCheck(double lengthCm)
        {
            // Check min size
            if (_minSizeCm.HasValue && lengthCm < _minSizeCm.Value)
                return false;

            // Check max size
            if (_maxSizeCm.HasValue && lengthCm > _maxSizeCm.Value)
                return false;

            // Check slot (fish INSIDE slot are illegal and must be released
            if (_slotMinCm.HasValue && _slotMaxCm.HasValue)
            {
                if (lengthCm >= _slotMinCm.Value && lengthCm <= _slotMaxCm.Value)
                    return false;
            }

            return true; // Passed all checks
        }

        /// <summary>
        /// Gets the daily catch limit for the specified license type
        /// </summary>
        /// <param name="type">The license type (sport or Conservation)</param>
        /// <returns>Daily catch limit for that license</returns>
        public int GetLimit(LicenseType type)
        {
            return type == LicenseType.Sport ? _sportLimit : _conservLimit;
        }
    }
}
