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

        
        public bool IsOpen(DateTime date)
        {
            // Open all year if both are default
            if (_seasonOpen == DateTime.MinValue && _seasonClose == DateTime.MinValue)
                return true;

            // Check if date is within season
            return date >= _seasonOpen && date <= _seasonClose;
        }

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

        public int GetLimit(LicenseType type)
        {
            return type == LicenseType.Sport ? _sportLimit : _conservLimit;
        }
    }
}
