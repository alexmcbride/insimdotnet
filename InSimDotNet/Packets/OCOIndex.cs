namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the IS_OCO index.
    /// </summary>
    public enum OCOIndex {
        /// <summary>
        /// Set all lights including main start lights and AutoX lights.
        /// </summary>
        OCO_INDEX_MAIN = 240,

        /// <summary>
        /// Set just AutoX lights.
        /// </summary>
        AXO_START_LIGHTS1 = 149,
        AXO_START_LIGHTS2 = 150,
        AXO_START_LIGHTS3 = 151,
    }
}
