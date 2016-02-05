using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace InSimDotNet.Helpers {
    /// <summary>
    /// Static class to help with track names.
    /// </summary>
    public static class TrackHelper {
        private class Track {
            public string FullTrackName { get; private set; }
            public bool HasReverse { get; private set; }

            public Track(string fullTrackName) : this(fullTrackName, false) { }

            public Track(string fullTrackName, bool hasReverse) {
                FullTrackName = fullTrackName;
                HasReverse = hasReverse;
            }
        }

        private static readonly Dictionary<string, Track> TrackMap = new Dictionary<string, Track>()
        {
            { "BL1", new Track("Blackwood Grand Prix", true) },
            { "BL2", new Track("Blackwood Rallycross", true) },
            { "BL3", new Track("Blackwood Car Park") },
            { "SO1", new Track("South City Classic", true) },
            { "SO2", new Track("South City Sprint 1", true) },
            { "SO3", new Track("South City Sprint 2", true) },
            { "SO4", new Track("South City Long", true) },
            { "SO5", new Track("South City Town", true) },
            { "SO6", new Track("South City Chicane", true) },
            { "FE1", new Track("Fern Bay Club", true) },
            { "FE2", new Track("Fern Bay Green", true) },
            { "FE3", new Track("Fern Bay Gold", true) },
            { "FE4", new Track("Fern Bay Black", true) },
            { "FE5", new Track("Fern Bay Rallycross", true) },
            { "FE6", new Track("Fern Bay RallyX Green", true) },
            { "AU1", new Track("Autocross") },
            { "AU2", new Track("Skid Pad") },
            { "AU3", new Track("Drag Strip") },
            { "AU4", new Track("Eight Lane Drag") },
            { "KY1", new Track("Kyoto Ring Oval", true) },
            { "KY2", new Track("Kyoto Ring National", true) },
            { "KY3", new Track("Kyoto Ring Grand Prix", true) },
            { "WE1", new Track("Westhill National", true) },
            { "WE2", new Track("Westhill International", true) },
            { "WE3", new Track("Westhill Car Park") },
            { "WE4", new Track("Westhill Karting", true) },
            { "WE5", new Track("Westhill Karting National", true) },
            { "AS1", new Track("Aston Cadet", true) },
            { "AS2", new Track("Aston Club", true) },
            { "AS3", new Track("Aston National", true) },
            { "AS4", new Track("Aston Historic", true) },
            { "AS5", new Track("Aston Grand Prix", true) },
            { "AS6", new Track("Aston Grand Touring", true) },
            { "AS7", new Track("Aston North", true) },
            { "RO1", new Track("Rockingham ISSC") },
            { "RO2", new Track("Rockingham National") },
            { "RO3", new Track("Rockingham Oval") },
            { "RO4", new Track("Rockingham ISSC Long") },
            { "RO5", new Track("Rockingham Lake") },
            { "RO6", new Track("Rockingham Handling") },
            { "RO7", new Track("Rockingham International") },
            { "RO8", new Track("Rockingham Historic") },
            { "RO9", new Track("Rockingham Historic Short") },
            { "RO10", new Track("Rockingham International Long") },
            { "RO11", new Track("Rockingham Sportscar") },
        };

        /// <summary>
        /// Gets all full track names.
        /// </summary>
        public static ReadOnlyCollection<string> FullTrackNames {
            get {
                return new ReadOnlyCollection<string>((from t in TrackMap.Values
                                                       orderby t
                                                       select t.FullTrackName).ToList());
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

            char config = shortTrackName.LastOrDefault();
            if (config == 'R' || config == 'X' || config == 'Y') {
                shortTrackName = shortTrackName.Substring(0, shortTrackName.Length - 1);
            }

            Track track;
            if (TrackMap.TryGetValue(shortTrackName, out track)) {
                if (config == 'R' || config == 'Y') {
                    if (track.HasReverse) {
                        return String.Format("{0} Reversed", track.FullTrackName);
                    }
                    return null;
                }

                if (config == 'X') {
                    return String.Format("{0} Open", track.FullTrackName);
                }

                return track.FullTrackName;
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
            return !String.IsNullOrEmpty(GetFullTrackName(shortTrackName));
        }
    }
}