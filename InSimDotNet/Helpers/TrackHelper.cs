using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
            { "WE1", "Westhill International" },
            { "AS1", "Aston Cadet" },
            { "AS2", "Aston Club" },
            { "AS3", "Aston National" },
            { "AS4", "Aston Historic" },
            { "AS5", "Aston Grand Prix" },
            { "AS6", "Aston Grand Touring" },
            { "AS7", "Aston North" },
            { "BL1R", "Blackwood Grand Prix Reversed" },
            { "BL2R", "Blackwood Rallycross Reversed" },
            { "BL3R", "Blackwood Car Park Reversed" },
            { "SO1R", "South City Classic Reversed" },
            { "SO2R", "South City Sprint 1 Reversed" },
            { "SO3R", "South City Sprint 2 Reversed" },
            { "SO4R", "South City Long Reversed" },
            { "SO5R", "South City Town Reversed" },
            { "SO6R", "South City Chicane Reversed" },
            { "FE1R", "Fern Bay Club Reversed" },
            { "FE2R", "Fern Bay Green Reversed" },
            { "FE3R", "Fern Bay Gold Reversed" },
            { "FE4R", "Fern Bay Black Reversed" },
            { "FE5R", "Fern Bay Rallycross Reversed" },
            { "FE6R", "Fern Bay RallyX Green Reversed" },
            { "AU1R", "Autocross Reversed" },
            { "AU2R", "Skid Pad Reversed" },
            { "AU3R", "Drag Strip Reversed" },
            { "AU4R", "Eight Lane Drag Reversed" },
            { "KY1R", "Kyoto Ring Oval Reversed" },
            { "KY2R", "Kyoto Ring National Reversed" },
            { "KY3R", "Kyoto Ring Grand Prix Reversed" },
            { "WE1R", "Westhill International Reversed" },
            { "AS1R", "Aston Cadet Reversed" },
            { "AS2R", "Aston Club Reversed" },
            { "AS3R", "Aston National Reversed" },
            { "AS4R", "Aston Historic Reversed" },
            { "AS5R", "Aston Grand Prix Reversed" },
            { "AS6R", "Aston Grand Touring Reversed" },
            { "AS7R", "Aston North Reversed" },
            { "BL1X", "Blackwood Grand Prix Open" },
            { "BL2X", "Blackwood Rallycross Open" },
            { "BL3X", "Blackwood Car Park Open" },
            { "SO1X", "South City Classic Open" },
            { "SO2X", "South City Sprint 1 Open" },
            { "SO3X", "South City Sprint 2 Open" },
            { "SO4X", "South City Long Open" },
            { "SO5X", "South City Town Open" },
            { "SO6X", "South City Chicane Open" },
            { "FE1X", "Fern Bay Club Open" },
            { "FE2X", "Fern Bay Green Open" },
            { "FE3X", "Fern Bay Gold Open" },
            { "FE4X", "Fern Bay Black Open" },
            { "FE5X", "Fern Bay Rallycross Open" },
            { "FE6X", "Fern Bay RallyX Green Open" },
            { "AU1X", "Autocross Open" },
            { "AU2X", "Skid Pad Open" },
            { "AU3X", "Drag Strip Open" },
            { "AU4X", "Eight Lane Drag Open" },
            { "KY1X", "Kyoto Ring Oval Open" },
            { "KY2X", "Kyoto Ring National Open" },
            { "KY3X", "Kyoto Ring Grand Prix Open" },
            { "WE1X", "Westhill International Open" },
            { "AS1X", "Aston Cadet Open" },
            { "AS2X", "Aston Club Open" },
            { "AS3X", "Aston National Open" },
            { "AS4X", "Aston Historic Open" },
            { "AS5X", "Aston Grand Prix Open" },
            { "AS6X", "Aston Grand Touring Open" },
            { "AS7X", "Aston North Open" },
            { "BL1Y", "Blackwood Grand Prix Open" },
            { "BL2Y", "Blackwood Rallycross Open" },
            { "BL3Y", "Blackwood Car Park Open" },
            { "SO1Y", "South City Classic Open" },
            { "SO2Y", "South City Sprint 1 Open" },
            { "SO3Y", "South City Sprint 2 Open" },
            { "SO4Y", "South City Long Open" },
            { "SO5Y", "South City Town Open" },
            { "SO6Y", "South City Chicane Open" },
            { "FE1Y", "Fern Bay Club Open" },
            { "FE2Y", "Fern Bay Green Open" },
            { "FE3Y", "Fern Bay Gold Open" },
            { "FE4Y", "Fern Bay Black Open" },
            { "FE5Y", "Fern Bay Rallycross Open" },
            { "FE6Y", "Fern Bay RallyX Green Open" },
            { "AU1Y", "Autocross Open" },
            { "AU2Y", "Skid Pad Open" },
            { "AU3Y", "Drag Strip Open" },
            { "AU4Y", "Eight Lane Drag Open" },
            { "KY1Y", "Kyoto Ring Oval Open" },
            { "KY2Y", "Kyoto Ring National Open" },
            { "KY3Y", "Kyoto Ring Grand Prix Open" },
            { "WE1Y", "Westhill International Open" },
            { "AS1Y", "Aston Cadet Open" },
            { "AS2Y", "Aston Club Open" },
            { "AS3Y", "Aston National Open" },
            { "AS4Y", "Aston Historic Open" },
            { "AS5Y", "Aston Grand Prix Open" },
            { "AS6Y", "Aston Grand Touring Open" },
            { "AS7Y", "Aston North Open" },
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

            shortTrackName = shortTrackName.ToUpper(CultureInfo.InvariantCulture);

            string track;
            if (TrackMap.TryGetValue(shortTrackName, out track)) {
                return track;
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
            return TrackMap.ContainsKey(shortTrackName);
        }
    }
}
