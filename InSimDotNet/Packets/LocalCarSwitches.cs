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
        public const int LCS_SET_FLASH = 1 << 1;  // bit 1

        /// <summary>
        ///  bit 2
        /// </summary>
        public const int LCS_SET_HEADLIGHTS = 1 << 2;      // bit 2

        /// <summary>
        /// Bit 3
        /// </summary>
        public const int LCS_SET_HORN = 1 << 3;  // bit 3

        /// <summary>
        /// Bit 4
        /// </summary>
        public const int LCS_SET_SIREN = 1 << 4;	// bit 4

        /// <summary>
        /// Turn signals off
        /// </summary>
        public const int LCS_SIGNALS_OFF = LCS_SET_SIGNALS;

        /// <summary>
        /// Turn signal left
        /// </summary>
        public const int LCS_SIGNALS_LEFT = (1 << 8) | LCS_SET_SIGNALS;

        /// <summary>
        /// Turn signal right
        /// </summary>
        public const int LCS_SIGNALS_RIGHT = (2 << 8) | LCS_SET_SIGNALS;

        /// <summary>
        /// Turn signal hazard
        /// </summary>
        public const int LCS_SIGNALS_HAZARD = (3 << 8) | LCS_SET_SIGNALS;

        /// <summary>
        /// Flash lights off
        /// </summary>
        public const int LCS_FLASH_OFF = LCS_SET_FLASH;

        /// <summary>
        /// Flight lights on
        /// </summary>
        public const int LCS_FLASH_ON = (1 << 10) | LCS_SET_FLASH;

        /// <summary>
        /// Headlights off
        /// </summary>
        public const int LCS_HEADLIGHTS_OFF = LCS_SET_HEADLIGHTS;

        /// <summary>
        /// Headlights on
        /// </summary>
        public const int LCS_HEADLIGHTS_ON = (1 << 11) | LCS_SET_HEADLIGHTS;

        /// <summary>
        /// Turn horn off
        /// </summary>
        public const int LCS_HORN_OFF = LCS_SET_HORN;

        /// <summary>
        /// Horn sound 1
        /// </summary>
        public const int LCS_HORN_1 = (1 << 16) | LCS_SET_HORN;

        /// <summary>
        /// Horn sound 2
        /// </summary>
        public const int LCS_HORN_2 = (2 << 16) | LCS_SET_HORN;

        /// <summary>
        /// Horn sound 3
        /// </summary>
        public const int LCS_HORN_3 = (3 << 16) | LCS_SET_HORN;

        /// <summary>
        /// Horn sound 4
        /// </summary>
        public const int LCS_HORN_4 = (4 << 16) | LCS_SET_HORN;

        /// <summary>
        /// Horn sound 5
        /// </summary>
        public const int LCS_HORN_5 = (5 << 16) | LCS_SET_HORN;

        /// <summary>
        /// Turn siren off
        /// </summary>
        public const int LCS_SIREN_OFF = LCS_SET_SIREN;

        /// <summary>
        /// Turn fast siren on
        /// </summary>
        public const int LCS_SIREN_FAST = (1 << 20) | LCS_SET_SIREN;

        /// <summary>
        /// Turn slow siren on
        /// </summary>
        public const int LCS_SIREN_SLOW = (2 << 20) | LCS_SET_SIREN;
    }
}
