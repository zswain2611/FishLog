# FishLog

A text-based C# console application for Ontario anglers.

FishLog lets users log fishing trips and catches, and automatically validates whether each catch is legal under the 2026 Ontario Fishing Regulations before recording it.

**Zones covered:** FMZ 10, FMZ 11  
**Species:** Walleye, Northern Pike, Bass, Yellow Perch, Lake Trout, Muskellunge  
**License types:** Sport and Conservation

---

## Features

- **Real-time catch validation** against Ontario fishing regulations
- **Season enforcement** - closed seasons automatically reject catches
- **Size restrictions** - minimum/maximum sizes and protected slots
- **Daily catch limits** - tracks kept fish and prevents over-limit violations
- **Auto-release system** - illegal catches are automatically recorded as released with explanations
- **Statistics tracking** - view total catches by species with keep/release breakdown
- **Color-coded interface** - green for success, red for errors, yellow for warnings

---

## Regulations Enforced

### Season Closures
- Northern Pike: Closed April 1 - May 15 in FMZ 11

### Size Restrictions
- **Walleye FMZ 10**: Maximum 46cm (none over)
- **Walleye FMZ 11**: Protected slot 43-60cm (must release), max 1 fish over 60cm allowed
- **Northern Pike**: Maximum 86cm (both zones)
- **Muskellunge**: Minimum 122cm (Sport only)

### Catch Limits (Sport / Conservation)
- Walleye: 4 / 2
- Northern Pike: 6 / 2
- Bass (Largemouth/Smallmouth combined): 6 / 3 (FMZ 10) or 6 / 2 (FMZ 11)
- Yellow Perch: 50 / 25
- Lake Trout: 2 / 1
- Muskellunge: 1 / 0 (Conservation must release all)

---

## Author

**Zach Swain**  
Cambrian College - Computer Programming and IoT  
Semester 2 OOP Final Project - April 2026

---

## Data Sources

Fishing regulations sourced from Ontario Fishing Regulations Summary 2026 and Ontario Ministry of Natural Resources and Forestry.
