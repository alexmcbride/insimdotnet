using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace InSimDotNet.Helpers {
    /// <summary>
    /// Static class to help with track names.
    /// </summary>
    public static class TrackHelper {
        private static readonly Dictionary<string, string> TrackMap = new Dictionary<string, string>()
        {
            { "BL1", "Blackwood Grand Prix" },
            { "BL2", "Blackwood Rallycross" },
            { "BL3", "Blackwood Car Park" },
            { "SO1", "South City Classic" },
            { "SO2", "South City Sprint 1" },
            { "SO3", "South City Sprint 2" },
            { "SO4", "South City Long" },
            { "SO5", "South City Town" },
            { "SO6", "South City Chicane" },
            { "FE1", "Fern Bay Club" },
            { "FE2", "Fern Bay Green" },
            { "FE3", "Fern Bay Gold" },
            { "FE4", "Fern Bay Black" },
            { "FE5", "Fern Bay Rallycross" },
            { "FE6", "Fern Bay RallyX Green" },
            { "AU1", "Autocross" },
            { "AU2", "Skid Pad" },
            { "AU3", "Drag Strip" },
            { "AU4", "Eight Lane Drag" },
            { "KY1", "Kyoto Ring Oval" },
            { "KY2", "Kyoto Ring National" },
            { "KY3", "Kyoto Ring Grand Prix" },
            { "WE1", "Westhill National" },
            { "WE2", "Westhill International" },
            { "WE3", "Westhill Car Park" },
            { "WE4", "Westhill Karting" },
            { "WE5", "Westhill Karting National" },
            { "AS1", "Aston Cadet" },
            { "AS2", "Aston Club" },
            { "AS3", "Aston National" },
            { "AS4", "Aston Historic" },
            { "AS5", "Aston Grand Prix" },
            { "AS6", "Aston Grand Touring" },
            { "AS7", "Aston North" },
            { "RO1", "Rockingham ISSC" },
            { "RO2", "Rockingham National" },
            { "RO3", "Rockingham Oval" },
            { "RO4", "Rockingham ISSC Long" },
            { "RO5", "Rockingham Lake" },
            { "RO6", "Rockingham Handling" },
            { "RO7", "Rockingham International" },
            { "RO8", "Rockingham Historic" },
            { "RO9", "Rockingham Historic Short" },
            { "RO10", "Rockingham International Long" },
            { "RO11", "Rockingham Sportscar" },
        };

        /// <summary>
        /// Gets all full track names.
        /// </summary>
        public static ReadOnlyCollection<string> FullTrackNames {
            get {
                return new ReadOnlyCollection<string>((from t in TrackMap.Values
                                                       orderby t
                                                       select t).ToList());
            }
        }

        /// <summary>
        /// Gets all short track names.
        /// </summary>
        public static ReadOnlyCollection<string> ShortTrackNames {
            get {
                return new ReadOnlyCollection<string>((from t in TrackMap.Keys
                                                       orderby t
                                                       select t).ToList());
            }
        }

        /// <summary>
        /// Returns the full name of a track or null if the track does not exist.
        /// </summary>
        /// <param name="shortTrackName">The tracks short code name.</param>
        /// <returns>The tracks full name.</returns>
        public static string GetFullTrackName(string shortTrackName) {
            if (shortTrackName == null) {
                throw new ArgumentNullException("shortTrackName");
            }

            shortTrackName = shortTrackName.ToUpper();

            char lastChar = shortTrackName.LastOrDefault();
            if (lastChar == 'R' || lastChar == 'X' || lastChar == 'Y') {
                shortTrackName = shortTrackName.Substring(0, shortTrackName.Length - 1);
            }

            string track;
            if (TrackMap.TryGetValue(shortTrackName, out track)) {
                switch(lastChar) {
                    case 'R':
                        return String.Format("{0} Reversed", track);
                    case 'X':
                    case 'Y':
                        return String.Format("{0} Open", track);
                    default:
                        return track;
                }
            }

            return null;
        }

        /// <summary>
        /// Tries to determine the full name of the specified track.
        /// </summary>
        /// <param name="shortTrackName">The short name for the track.</param>
        /// <param name="fullTrackName">The full name of the track.</param>
        /// <returns>True if the track exists.</returns>
        public static bool TryGetFullTrackName(string shortTrackName, out string fullTrackName) {
            return !String.IsNullOrEmpty(fullTrackName = GetFullTrackName(shortTrackName));
        }

        /// <summary>
        /// Determines if the specified track exists.
        /// </summary>
        /// <param name="shortTrackName">The short name of the track.</param>
        /// <returns>True if the track exists.</returns>
        public static bool TrackExists(string shortTrackName) {
            if (shortTrackName == null) {
                throw new ArgumentNullException("shortTrackName");
            }

            shortTrackName = shortTrackName.ToUpper();

            char lastChar = shortTrackName.Last();
            if (lastChar == 'R' || lastChar == 'X' || lastChar == 'Y') {
                shortTrackName = shortTrackName.Substring(0, shortTrackName.Length - 1);
            }

            return TrackMap.ContainsKey(shortTrackName);
        }
    }
}
