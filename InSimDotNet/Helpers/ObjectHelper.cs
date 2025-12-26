using InSimDotNet.Packets;
using System.Collections.Generic;

namespace InSimDotNet.Helpers {
    /// <summary>
    /// Static class to help with object names.
    /// </summary>
    public static class ObjectHelper {
        private static readonly Dictionary<AxoObject, string[]> ObjMap = new Dictionary<AxoObject, string[]>() {
            { AxoObject.AXO_UNKNOWN, new string[] { "Unknown", "Unknown Object" }},

            // Chalk
            { AxoObject.AXO_CHALK_LINE, new string[] { "Chalk", "Chalk Line" }},
            { AxoObject.AXO_CHALK_LINE2, new string[] { "Chalk", "Chalk Line2" }},
            { AxoObject.AXO_CHALK_AHEAD, new string[] { "Chalk", "Chalk Ahead" }},
            { AxoObject.AXO_CHALK_AHEAD2, new string[] { "Chalk", "Chalk Ahead Long" }},
            { AxoObject.AXO_CHALK_LEFT, new string[] { "Chalk", "Chalk Soft Left" }},
            { AxoObject.AXO_CHALK_LEFT2, new string[] { "Chalk", "Chalk Hard Left" }},
            { AxoObject.AXO_CHALK_LEFT3, new string[] { "Chalk", "Chalk Soft Left Long" }},
            { AxoObject.AXO_CHALK_RIGHT, new string[] { "Chalk", "Chalk Soft Right" }},
            { AxoObject.AXO_CHALK_RIGHT2, new string[] { "Chalk", "Chalk Hard Right" }},
            { AxoObject.AXO_CHALK_RIGHT3, new string[] { "Chalk", "Chalk Soft Right Long" }},

            // Paint
            { AxoObject.AXO_PAINT_LETTERS, new string[] { "Paint", "Paint Letters" }},
            { AxoObject.AXO_PAINT_ARROWS, new string[] { "Paint", "Paint Arrows" }},

            // Cones
            { AxoObject.AXO_CONE1, new string[] { "Cone", "Cone1" }},
            { AxoObject.AXO_CONE2, new string[] { "Cone", "Cone2" }},
            { AxoObject.AXO_CONE_TALL1, new string[] { "Cone", "Cone Tall1" }},
            { AxoObject.AXO_CONE_TALL2, new string[] { "Cone", "Cone Tall2" }},
            { AxoObject.AXO_CONE_POINTER, new string[] { "Cone", "Cone Pointer" }},

            // Tyres
            { AxoObject.AXO_TYRE_SINGLE, new string[] { "Tyres", "Tyre" }},
            { AxoObject.AXO_TYRE_STACK2, new string[] { "Tyres", "Tyre Stack of 2" }},
            { AxoObject.AXO_TYRE_STACK3, new string[] { "Tyres", "Tyre Stack of 3" }},
            { AxoObject.AXO_TYRE_STACK4, new string[] { "Tyres", "Tyre Stack of 4" }},
            { AxoObject.AXO_TYRE_SINGLE_BIG, new string[] { "Tyres", "Big Tyre" }},
            { AxoObject.AXO_TYRE_STACK2_BIG, new string[] { "Tyres", "Big Tyre Stack of 2" }},
            { AxoObject.AXO_TYRE_STACK3_BIG, new string[] { "Tyres", "Big Tyre Stack of 3" }},
            { AxoObject.AXO_TYRE_STACK4_BIG, new string[] { "Tyres", "Big Tyre Stack of 4" }},

            // Signs
            { AxoObject.AXO_MARKER_CORNER, new string[] { "Signs", "Marker Corner" }},
            { AxoObject.AXO_MARKER_DISTANCE, new string[] { "Signs", "Marker Distance" }},

            // Letter boards
            { AxoObject.AXO_LETTER_BOARD_WY, new string[] { "Signs", "Letter Board WY" }},
            { AxoObject.AXO_LETTER_BOARD_RB, new string[] { "Signs", "Letter Board RB" }},

            // Railings / Armco
            { AxoObject.AXO_ARMCO1, new string[] { "Barriers", "Railing Short" }},
            { AxoObject.AXO_ARMCO3, new string[] { "Barriers", "Railing Medium" }},
            { AxoObject.AXO_ARMCO5, new string[] { "Barriers", "Railing Long" }},

            // Barriers
            { AxoObject.AXO_BARRIER_LONG, new string[] { "Barriers", "Barrier Long" }},
            { AxoObject.AXO_BARRIER_RED, new string[] { "Barriers", "Barrier Red" }},
            { AxoObject.AXO_BARRIER_WHITE, new string[] { "Barriers", "Barrier White" }},

            // Banner
            { AxoObject.AXO_BANNER, new string[] { "Banners", "Banner" }},

            // Ramps
            { AxoObject.AXO_RAMP1, new string[] { "Ramp", "Ramp" }},
            { AxoObject.AXO_RAMP2, new string[] { "Ramp", "Ramp Wide" }},

            // Vehicles
            { AxoObject.AXO_VEH_SUV, new string[] { "Vehicle", "SUV" }},
            { AxoObject.AXO_VEH_VAN, new string[] { "Vehicle", "Van" }},
            { AxoObject.AXO_VEH_TRUCK, new string[] { "Vehicle", "Truck" }},
            { AxoObject.AXO_VEH_AMBULANCE, new string[] { "Vehicle", "Ambulance" }},

            // Speed bumps
            { AxoObject.AXO_SPEED_HUMP_10M, new string[] { "Speed Bump", "Speed Bump 10m" }},
            { AxoObject.AXO_SPEED_HUMP_6M, new string[] { "Speed Bump", "Speed Bump 6m" }},
            { AxoObject.AXO_SPEED_HUMP_2M, new string[] { "Speed Bump", "Speed Bump 2m" }},
            { AxoObject.AXO_SPEED_HUMP_1M, new string[] { "Speed Bump", "Speed Bump 1m" }},

            // Kerb
            { AxoObject.AXO_KERB, new string[] { "Kerb", "Kerb" }},

            // Posts
            { AxoObject.AXO_POST, new string[] { "Post", "Post Green" }},

            // Structures
            { AxoObject.AXO_MARQUEE, new string[] { "Objects", "Marquee" }},
            { AxoObject.AXO_BALE, new string[] { "Objects", "Bale" }},
            { AxoObject.AXO_BIN1, new string[] { "Objects", "Bin Small" }},
            { AxoObject.AXO_BIN2, new string[] { "Objects", "Bin Large" }},

            { AxoObject.AXO_RAILING1, new string[] { "Railing", "Railing1" }},
            { AxoObject.AXO_RAILING2, new string[] { "Railing", "Railing2" }},

            // Start lights
            { AxoObject.AXO_START_LIGHTS1, new string[] { "Control", "Start Lights" }},
            { AxoObject.AXO_START_LIGHTS2, new string[] { "Control", "Start Lights2" }},
            { AxoObject.AXO_START_LIGHTS3, new string[] { "Control", "Start Lights3" }},

            // More Signs
            { AxoObject.AXO_SIGN_METAL, new string[] { "Signs", "Metal" }},
            { AxoObject.AXO_SIGN_CHEVRON_LEFT, new string[] { "Signs", "Sign Chevron Left" }},
            { AxoObject.AXO_SIGN_CHEVRON_RIGHT, new string[] { "Signs", "Sign Chevron Right" }},
            { AxoObject.AXO_SIGN_SPEED, new string[] { "Signs", "Sign Speed" }},

            // Concrete
            { AxoObject.AXO_CONCRETE_SLAB, new string[] { "Concrete", "Concrete Slab" }},
            { AxoObject.AXO_CONCRETE_RAMP, new string[] { "Concrete", "Concrete Ramp" }},
            { AxoObject.AXO_CONCRETE_WALL, new string[] { "Concrete", "Concrete Wall" }},
            { AxoObject.AXO_CONCRETE_PILLAR, new string[] { "Concrete", "Concrete Pillar" }},
            { AxoObject.AXO_CONCRETE_SLAB_WALL, new string[] { "Concrete", "Concrete Slab Wall" }},
            { AxoObject.AXO_CONCRETE_RAMP_WALL, new string[] { "Concrete", "Concrete Ramp Wall" }},
            { AxoObject.AXO_CONCRETE_SHORT_SLAB_WALL, new string[] { "Concrete", "Concrete Short Slab Wall" }},
            { AxoObject.AXO_CONCRETE_WEDGE, new string[] { "Concrete", "Concrete Wedge" }},

            // Start / pit
            { AxoObject.AXO_START_POSITION, new string[] { "Control", "Start position" }},
            { AxoObject.AXO_PIT_START_POINT, new string[] { "Control", "Pit Start Point" }},
            { AxoObject.AXO_PIT_STOP_BOX, new string[] { "Control", "Pit Stop Box" }},

            // Marshal / InSim
            { AxoObject.AXO_MARSHAL, new string[] { "Marshal", "Marshal" }},
            { AxoObject.MARSH_IS_CP, new string[] { "Marshal", "InSim checkpoint" }},
            { AxoObject.MARSH_IS_AREA, new string[] { "Marshal", "InSim circle" }},
            { AxoObject.MARSH_MARSHAL, new string[] { "Marshal", "Restricted Area" }},
            { AxoObject.MARSH_ROUTE, new string[] { "Marshal", "Route Checker" }},
        };


        /// <summary>
        /// Determines the full name of an object or null if the index does not exist.
        /// </summary>
        /// <param name="index">The object's AxoObject.</param>
        /// <returns>The full name.</returns>
        public static string GetObjName(AxoObject index) {
            if (ObjMap.TryGetValue(index, out string[] obj)) {
                return obj[1];
            }
            return null;
        }

        /// <summary>
        /// Determines the type of the object or returns null if the index does not exist.
        /// </summary>
        /// <param name="index">The object's AxoObject.</param>
        /// <returns>The type of the object.</returns>
        public static string GetObjType(AxoObject index) {
            if (ObjMap.TryGetValue(index, out string[] obj))
            {
                return obj[0];
            }
            return null;
        }

        /// <summary>
        /// Determines if the specified object exists.
        /// </summary>
        /// <param name="index">The AxoObject of the object.</param>
        /// <returns>True if the object exists.</returns>
        public static bool ObjExists(AxoObject index) {
            return ObjMap.ContainsKey(index);
        }
    }
}