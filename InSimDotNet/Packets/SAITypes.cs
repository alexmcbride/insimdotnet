namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the SAIType values
    /// </summary>
    public enum SAIType
    {
        /// <summary>
        /// Movable
        /// </summary>
        SAI_MOVE = 0,
        /// <summary>
        /// Unmovable / Floating
        /// </summary>
        SAI_FLOAT = 1,
        /// <summary>
        /// Unmovable / Ground level
        /// </summary>
        SAI_GROUND = 2,
        /// <summary>
        /// Unmovable / Ground level / Ground angle
        /// </summary>
        SAI_ANGLE = 3
    }
}
