using System.Collections.Generic;

namespace InSimDotNet.Helpers {
    /// <summary>
    /// Static class to help with object names.
    /// </summary>
    public static class ObjectHelper {
        private static readonly Dictionary<int, string[]> ObjMap = new Dictionary<int, string[]>() {
           { 0, new string[] { "Unknown", "Unknown Object" }},

            { 4, new string[] { "Chalk", "Chalk Line Long" }},
            { 5, new string[] { "Chalk", "Chalk Line" }},
            { 6, new string[] { "Chalk", "Chalk Ahead" }},
            { 7, new string[] { "Chalk", "Chalk Ahead Long" }},
            { 8, new string[] { "Chalk", "Chalk Soft Left" }},
            { 9, new string[] { "Chalk", "Chalk Hard Left" }},
            { 10, new string[] { "Chalk", "Chalk Soft Left Long" }},
            { 11, new string[] { "Chalk", "Chalk Soft Right" }},
            { 12, new string[] { "Chalk", "Chalk Hard Right" }},
            { 13, new string[] { "Chalk", "Chalk Soft Right Long" }},

            { 20, new string[] { "Cone", "Cone Red/White" }},
            { 21, new string[] { "Cone", "Cone Red" }},
            { 22, new string[] { "Cone", "Cone Red Striped" }},
            { 23, new string[] { "Cone", "Cone Blue Striped" }},
            { 24, new string[] { "Cone", "Cone Blue" }},
            { 25, new string[] { "Cone", "Cone Green Striped" }},
            { 26, new string[] { "Cone", "Cone Green" }},
            { 27, new string[] { "Cone", "Cone Orange" }},
            { 28, new string[] { "Cone", "Cone White" }},
            { 29, new string[] { "Cone", "Cone Yellow Striped" }},
            { 30, new string[] { "Cone", "Cone Yellow" }},

            { 40, new string[] { "Cone", "Cone Red Directional" }},
            { 41, new string[] { "Cone", "Cone Blue Directional" }},
            { 42, new string[] { "Cone", "Cone Green Directional" }},
            { 43, new string[] { "Cone", "Cone Yellow Directional" }},

            { 48, new string[] { "Tyre", "Tyre" }},
            { 49, new string[] { "Tyres", "Tyre Stack of 2" }},
            { 50, new string[] { "Tyres", "Tyre Stack of 3" }},
            { 51, new string[] { "Tyres", "Tyre Stack of 4" }},
            { 52, new string[] { "Tyre", "Big Tyre" }},
            { 53, new string[] { "Tyres", "Big Tyre Stack of 2" }},
            { 54, new string[] { "Tyres", "Big Tyre Stack of 3" }},
            { 55, new string[] { "Tyres", "Big Tyre Stack of 4" }},

            { 64, new string[] { "Marker", "Marker Curve Left" }},
            { 65, new string[] { "Marker", "Marker Curve Right" }},
            { 66, new string[] { "Marker", "Marker Left" }},
            { 67, new string[] { "Marker", "Marker Right" }},
            { 68, new string[] { "Marker", "Marker Left Hard" }},
            { 69, new string[] { "Marker", "Marker Right Hard" }},
            { 70, new string[] { "Marker", "Marker Left->Right" }},
            { 71, new string[] { "Marker", "Marker Right->Left" }},
            { 72, new string[] { "Marker", "Marker U-Turn->Right" }},
            { 73, new string[] { "Marker", "Marker U-Turn->Left" }},
            { 74, new string[] { "Marker", "Marker Winding Left" }},
            { 75, new string[] { "Marker", "Marker Winding Right" }},
            { 76, new string[] { "Marker", "Marker U-Turn Left" }},
            { 77, new string[] { "Marker", "Marker U-Turn Right" }},

            { 84, new string[] { "Marker", "Marker 25" }},
            { 85, new string[] { "Marker", "Marker 50" }},
            { 86, new string[] { "Marker", "Marker 75" }},
            { 87, new string[] { "Marker", "Marker 100" }},
            { 88, new string[] { "Marker", "Marker 125" }},
            { 89, new string[] { "Marker", "Marker 150" }},
            { 90, new string[] { "Marker", "Marker 200" }},
            { 91, new string[] { "Marker", "Marker 250" }},

            { 96, new string[] { "Railing", "Railing Short" }},
            { 97, new string[] { "Railing", "Railing Medium" }},
            { 98, new string[] { "Railing", "Railing Long" }},

            { 104, new string[] { "Barrier", "Barrier Long" }},
            { 105, new string[] { "Barrier", "Barrier Red" }},
            { 106, new string[] { "Barrier", "Barrier White" }},

            { 112, new string[] { "Banner", "Banner 1" }},
            { 113, new string[] { "Banner", "Banner 2" }},

            { 120, new string[] { "Ramp", "Ramp" }},
            { 121, new string[] { "Ramp", "Ramp Wide" }},

            { 128, new string[] { "Speed Bump", "Speed Bump Long" }},
            { 129, new string[] { "Speed Bump", "Speed Bump" }},

            { 136, new string[] { "Post", "Post Green" }},
            { 137, new string[] { "Post", "Post Orange" }},
            { 138, new string[] { "Post", "Post Red" }},
            { 139, new string[] { "Post", "Post White" }},

            { 144, new string[] { "Bale", "Bale" }},

            { 148, new string[] { "Railing", "Railing" }},
            { 149, new string[] { "Control", "Start lights" }},

            { 160, new string[] { "Sign", "Sign Keep Left" }},
            { 161, new string[] { "Sign", "Sign Keep Right" }},

            { 168, new string[] { "Sign", "Sign 80 km/h" }},
            { 169, new string[] { "Sign", "Sign 50 km/h" }},

            { 172, new string[] { "Concrete", "Concrete Slab" }},
            { 173, new string[] { "Concrete", "Concrete Ramp" }},
            { 174, new string[] { "Concrete", "Concrete Wall" }},
            { 175, new string[] { "Concrete", "Concrete Pillar" }},
            { 176, new string[] { "Concrete", "Concrete Slab Wall" }},
            { 177, new string[] { "Concrete", "Concrete Ramp Wall" }},
            { 178, new string[] { "Concrete", "Concrete Short Slab Wall" }}, 
            { 179, new string[] { "Concrete", "Concrete Wedge" }},

            { 184, new string[] { "Control", "Start position" }},
            { 185, new string[] { "Control", "Pit Start Point" }},
            { 186, new string[] { "Control", "Pit Stop Box" }},

            { 254, new string[] { "Marshall", "Restricted area" }},
            { 255, new string[] { "Marshall", "Route checker" }},
        };

        /// <summary>
        /// Determines the full name of an object or null if the index does not exist.
        /// </summary>
        /// <param name="index">The object's index.</param>
        /// <returns>The full name.</returns>
        public static string GetObjName(int index) {
            string[] obj;
            if (ObjMap.TryGetValue(index, out obj)) {
                return obj[1];
            }
            return null;
        }

        /// <summary>
        /// Determines the type of the object or returns null if the index does not exist.
        /// </summary>
        /// <param name="index">The object's index.</param>
        /// <returns>The type of the object.</returns>
        public static string GetObjType(int index) {
            string[] obj;
            if (ObjMap.TryGetValue(index, out obj)) {
                return obj[0];
            }
            return null;
        }

        /// <summary>
        /// Determines if the specified object exists.
        /// </summary>
        /// <param name="index">The index of the object.</param>
        /// <returns>True if the object exists.</returns>
        public static bool ObjExists(int index) {
            return ObjMap.ContainsKey(index);
        }
    }
}