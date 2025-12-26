namespace InSimDotNet.Packets
{
    public enum AxoObject : byte
    {
        AXO_UNKNOWN = 0,
        AXO_CHALK_LINE = 4,
        AXO_CHALK_LINE2 = 5,
        AXO_CHALK_AHEAD = 6,
        AXO_CHALK_AHEAD2 = 7,
        AXO_CHALK_LEFT = 8,
        AXO_CHALK_LEFT2 = 9,
        AXO_CHALK_LEFT3 = 10,
        AXO_CHALK_RIGHT = 11,
        AXO_CHALK_RIGHT2 = 12,
        AXO_CHALK_RIGHT3 = 13,
        AXO_PAINT_LETTERS = 16,
        AXO_PAINT_ARROWS = 17,
        AXO_CONE1 = 20,
        AXO_CONE2 = 21,
        AXO_CONE_TALL1 = 32,
        AXO_CONE_TALL2 = 33,
        AXO_CONE_POINTER = 40,
        AXO_TYRE_SINGLE = 48,
        AXO_TYRE_STACK2 = 49,
        AXO_TYRE_STACK3 = 50,
        AXO_TYRE_STACK4 = 51,
        AXO_TYRE_SINGLE_BIG = 52,
        AXO_TYRE_STACK2_BIG = 53,
        AXO_TYRE_STACK3_BIG = 54,
        AXO_TYRE_STACK4_BIG = 55,
        AXO_MARKER_CORNER = 64,
        AXO_MARKER_DISTANCE = 84,
        AXO_LETTER_BOARD_WY = 92,
        AXO_LETTER_BOARD_RB = 93,
        AXO_ARMCO1 = 96,
        AXO_ARMCO3 = 97,
        AXO_ARMCO5 = 98,
        AXO_BARRIER_LONG = 104,
        AXO_BARRIER_RED = 105,
        AXO_BARRIER_WHITE = 106,
        AXO_BANNER = 112,
        AXO_RAMP1 = 120,
        AXO_RAMP2 = 121,
        AXO_VEH_SUV = 124,
        AXO_VEH_VAN = 125,
        AXO_VEH_TRUCK = 126,
        AXO_VEH_AMBULANCE = 127,
        AXO_SPEED_HUMP_10M = 128,
        AXO_SPEED_HUMP_6M = 129,
        AXO_SPEED_HUMP_2M = 130,
        AXO_SPEED_HUMP_1M = 131,
        AXO_KERB = 132,
        AXO_POST = 136,
        AXO_MARQUEE = 140,
        AXO_BALE = 144,
        AXO_BIN1 = 145,
        AXO_BIN2 = 146,
        AXO_RAILING1 = 147,
        AXO_RAILING2 = 148,
        AXO_START_LIGHTS1 = 149,
        AXO_START_LIGHTS2 = 150,
        AXO_START_LIGHTS3 = 151,
        AXO_SIGN_METAL = 160,
        AXO_SIGN_CHEVRON_LEFT = 164,
        AXO_SIGN_CHEVRON_RIGHT = 165,
        AXO_SIGN_SPEED = 168,
        AXO_CONCRETE_SLAB = 172,
        AXO_CONCRETE_RAMP = 173,
        AXO_CONCRETE_WALL = 174,
        AXO_CONCRETE_PILLAR = 175,
        AXO_CONCRETE_SLAB_WALL = 176,
        AXO_CONCRETE_RAMP_WALL = 177,
        AXO_CONCRETE_SHORT_SLAB_WALL = 178,
        AXO_CONCRETE_WEDGE = 179,
        AXO_START_POSITION = 184,
        AXO_PIT_START_POINT = 185,
        AXO_PIT_STOP_BOX = 186,
        AXO_MARSHAL = 240,

        //Extra not listed in LYT.TXT file but can be in IS_CIM or in LYT file
        /// <summary>
        /// InSim checkpoint.
        /// </summary>
        MARSH_IS_CP = 252,
        /// <summary>
        /// InSim circle.
        /// </summary>
        MARSH_IS_AREA = 253,
        /// <summary>
        /// Restricted area.
        /// </summary>
        MARSH_MARSHAL = 254,
        /// <summary>
        /// Route checker.
        /// </summary>
        MARSH_ROUTE = 255
    }
}
