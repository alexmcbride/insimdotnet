namespace InSimDotNet.Packets
{
    /// <summary>
    /// Switches for <see cref="IS_SMALL"/> SMALL_LCS
    /// </summary>
    public static class LocalCarSwitches
    {
        /// <summary>
        /// Bit 0
        /// </summary>
        public const int LCS_SET_SIGNALS = 1;  // bit 0

        /// <summary>
        /// Bit 1
        /// </summary>
        public const int LCS_SET_FLASH = 2;  // bit 1

        /// <summary>
        ///  bit 2
        /// </summary>
        public const int LCS_SET_HEADLIGHTS = 4;      // bit 2

        /// <summary>
        /// Bit 3
        /// </summary>
        public const int LCS_SET_HORN = 8;  // bit 3

        /// <summary>
        /// Bit 4
        /// </summary>
        public const int LCS_SET_SIREN = 0x10;	// bit 4

        /// <summary>
        /// Turn signals off
        /// </summary>
        public const int LCS_SIGNALS_OFF = LCS_SET_SIGNALS;

        /// <summary>
        /// Turn signal left
        /// </summary>
        public const int LCS_SIGNALS_LEFT = LCS_SET_SIGNALS + 256;

        /// <summary>
        /// Turn signal right
        /// </summary>
        public const int LCS_SIGNALS_RIGHT = LCS_SET_SIGNALS + 512;

        /// <summary>
        /// Turn signal hazard
        /// </summary>
        public const int LCS_SIGNALS_HAZARD = LCS_SET_SIGNALS + 512 + 256;

        // Flash lights off
        public const int LCS_FLASH_OFF = LCS_SET_FLASH;

        /// <summary>
        /// Flight lights on
        /// </summary>
        public const int LCS_FLASH_ON = LCS_SET_FLASH + 1024;

        /// <summary>
        /// Headlights off
        /// </summary>
        public const int LCS_HEADLIGHTS_OFF = LCS_SET_HEADLIGHTS;

        /// <summary>
        /// Headlights on
        /// </summary>
        public const int LCS_HEADLIGHTS_ON = LCS_SET_HEADLIGHTS + 2048;

        /// <summary>
        /// Horn off
        /// </summary>
        public const int LCS_HORN_OFF = LCS_SET_HORN;

        /// <summary>
        /// Horn sound 1
        /// </summary>
        public const int LCS_HORN_1 = LCS_SET_HORN + 65536;

        /// <summary>
        /// Horn sound 2
        /// </summary>
        public const int LCS_HORN_2 = LCS_SET_HORN + 131072;

        /// <summary>
        /// Horn sound 3
        /// </summary>
        public const int LCS_HORN_3 = LCS_SET_HORN + 131072 + 65536;

        /// <summary>
        /// Horn sound 4
        /// </summary>
        public const int LCS_HORN_4 = LCS_SET_HORN + 262144;

        /// <summary>
        /// Horn sound 5
        /// </summary>
        public const int LCS_HORN_5 = LCS_SET_HORN + 2097152 + 65536;

        /// <summary>
        /// Turn siren off
        /// </summary>
        public const int LCS_SIREN_OFF = LCS_SET_SIREN;

        /// <summary>
        /// Turn fast siren on
        /// </summary>
        public const int LCS_SIREN_FAST = LCS_SET_SIREN + 1048576;

        /// <summary>
        /// Turn slow siren on
        /// </summary>
        public const int LCS_SIREN_SLOW = LCS_SET_SIREN + 2097152;
    }
}
