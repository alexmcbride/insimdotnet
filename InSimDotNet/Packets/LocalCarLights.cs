namespace InSimDotNet.Packets
{
    /// <summary>
    /// Switches for <see cref="IS_SMALL"/> SMALL_LCL
    /// </summary>
    public static class LocalCarLights
    {
        /// <summary>
        /// bit 0
        /// </summary>
        public const int LCL_SET_SIGNALS = 0x1;

        /// <summary>
        /// bit 2
        /// </summary>
        public const int LCL_SET_LIGHTS = 0x4;
        
        /// <summary>
        /// bit 4
        /// </summary>
        public const int LCL_SET_FOG_REAR = 0x10;

        /// <summary>
        /// bit 5
        /// </summary>
        public const int LCL_SET_FOG_FRONT = 0x20;

        /// <summary>
        /// bit 6
        /// </summary>
        public const int LCL_SET_EXTRA = 0x40;

        /// <summary>
        /// Turn signals off
        /// </summary>
        public const int LCL_SIGNALS_OFF = LCL_SET_SIGNALS;

        /// <summary>
        /// Turn signal left
        /// </summary>
        public const int LCL_SIGNALS_LEFT = LCL_SET_SIGNALS + 0x10000;

        /// <summary>
        /// Turn signal right
        /// </summary>
        public const int LCL_SIGNALS_RIGHT = LCL_SET_SIGNALS + 0x20000;

        /// <summary>
        /// Turn signal hazard
        /// </summary>
        public const int LCL_SIGNALS_HAZARD = LCL_SET_SIGNALS + 0x30000;

        /// <summary>
        /// Turn lights off
        /// </summary>
        public const int LCL_LIGHTS_OFF = LCL_SET_LIGHTS;

        /// <summary>
        /// Turn side lights on
        /// </summary>
        public const int LCL_LIGHTS_SIDE = LCL_SET_LIGHTS + 0xA0000;

        /// <summary>
        /// Turn low lights on
        /// </summary>
        public const int LCL_LIGHTS_LOW = LCL_SET_LIGHTS + 0xB0000;

        /// <summary>
        /// Turn high lights on
        /// </summary>
        public const int LCL_LIGHTS_HIGH = LCL_SET_LIGHTS + 0xC0000;

        /// <summary>
        /// Turn rear fog lights off
        /// </summary>
        public const int LCL_FOG_REAR_OFF = LCL_SET_FOG_REAR;

        /// <summary>
        /// Turn rear fog lights on
        /// </summary>
        public const int LCL_FOG_REAR_ON = LCL_SET_FOG_REAR + 0x100000;

        /// <summary>
        /// Turn front fog lights off
        /// </summary>
        public const int LCL_FOG_FRONT_OFF = LCL_SET_FOG_FRONT;

        /// <summary>
        /// Turn front fog lights on
        /// </summary>
        public const int LCL_FOG_FRONT_ON = LCL_SET_FOG_FRONT + 0x200000;

        /// <summary>
        /// Turn extra lights off
        /// </summary>
        public const int LCL_EXTRA_OFF = LCL_SET_EXTRA;

        /// <summary>
        /// Turn extra lights on
        /// </summary>
        public const int LCL_EXTRA_ON = LCL_SET_EXTRA + 0x400000;
    }
}
