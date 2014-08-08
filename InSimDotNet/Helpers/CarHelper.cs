using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using InSimDotNet.Packets;

namespace InSimDotNet.Helpers {
    /// <summary>
    /// Static class to help with car names.
    /// </summary>
    public static class CarHelper {
        private static readonly Dictionary<string, string> CarMap = new Dictionary<string, string>()
        {
            { "XFG", "XF GTI" },
            { "XRG", "XR GT" },
            { "FBM", "Formula BMW" },
            { "XRT", "XR GT Turbo" },
            { "RB4", "RB4 GT" },
            { "FXO", "FXO Turbo" },
            { "LX4", "LX4" },
            { "LX6", "LX6" },
            { "MRT", "MRT5" },
            { "UF1", "UF 1000" },
            { "RAC", "RaceAbout" },
            { "FZ5", "FZ50" },
            { "XFR", "XF GTR" },
            { "UFR", "UF GTR" },
            { "FOX", "Formula XR" },
            { "FO8", "Formula V8" },
            { "BF1", "BMW Sauber F1" },
            { "FXR", "FXO GTR" },
            { "XRR", "XR GTR" },
            { "FZR", "FZ50 GTR" },
            { "VWS", "VW Scirocco" },
        };

        /// <summary>
        /// Gets all full car names.
        /// </summary>
        public static ReadOnlyCollection<string> FullCarNames {
            get {
                return new ReadOnlyCollection<string>((from c in CarMap.Values
                                                       orderby c
                                                       select c).ToList());
            }
        }

        /// <summary>
        /// Gets all short car names;
        /// </summary>
        public static ReadOnlyCollection<string> ShortCarNames {
            get {
                return new ReadOnlyCollection<string>((from c in CarMap.Keys
                                                       orderby c
                                                       select c).ToList());
            }
        }

        /// <summary>
        /// Determines the full name of a car or null if the car does not exist.
        /// </summary>
        /// <param name="shortCarName">The cars short car name.</param>
        /// <returns>The full name.</returns>
        public static string GetFullCarName(string shortCarName) {
            if (shortCarName == null) {
                throw new ArgumentNullException("shortCarName");
            }

            shortCarName = shortCarName.ToUpper(CultureInfo.InvariantCulture);

            string car;
            if (CarMap.TryGetValue(shortCarName, out car)) {
                return car;
            }

            return null;
        }

        /// <summary>
        /// Tries to determine the full name of the specified car.
        /// </summary>
        /// <param name="shortCarName">The short name for the car.</param>
        /// <param name="fullCarName">The full name for the car.</param>
        /// <returns>True if the car exists.</returns>
        public static bool TryGetFullCarName(string shortCarName, out string fullCarName) {
            return !String.IsNullOrEmpty(fullCarName = GetFullCarName(shortCarName));
        }

        /// <summary>
        /// Determines if the specified car exists.
        /// </summary>
        /// <param name="shortCarName">The short name of the car.</param>
        /// <returns>True if the car exists.</returns>
        public static bool CarExists(string shortCarName) {
            return CarMap.ContainsKey(shortCarName);
        }
    }
}
