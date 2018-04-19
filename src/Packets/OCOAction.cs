namespace InSimDotNet.Packets {
    /// <summary>
    /// Enum to represent the IS_OCO OCOAction enumeration.
    /// </summary>
    public enum OCOAction {
        /// <summary>
        /// Nowt.
        /// </summary>
        OCO_ZERO = 0,

        /// <summary>
        /// Give up control of all lights
        /// </summary>
        OCO_LIGHTS_RESET = 4,

        /// <summary>
        /// Use Data byte to set the bulbs
        /// </summary>
        OCO_LIGHTS_SET = 5,

        /// <summary>
        /// Give up control of the specified lights
        /// </summary>
        OCO_LIGHTS_UNSET = 6,
    }

}
