namespace InSimDotNet.Packets {
    /// <summary>
    /// Some of the object type values to represent the IS_CIM SelType attribute.
    /// The remaining is found in the OCOIndex enumeration.
    /// </summary>
    public enum MarshallObjectType {
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
        MARSH_MARSHALL = 254,

        /// <summary>
        /// Route checker.
        /// </summary>
        MARSH_ROUTE = 255
    }
}
