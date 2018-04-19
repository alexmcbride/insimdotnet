namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the pit lane facts.
    /// </summary>
    public enum PitLaneFact {
        /// <summary>
        /// Left pit lane
        /// </summary>
        PITLANE_EXIT,		

        /// <summary>
        /// Entered pit lane
        /// </summary>
        PITLANE_ENTER,		

        /// <summary>
        /// Entered for no purpose
        /// </summary>
        PITLANE_NO_PURPOSE,	

        /// <summary>
        /// Entered for drive-through
        /// </summary>
        PITLANE_DT,			

        /// <summary>
        /// Entered for stop-go
        /// </summary>
        PITLANE_SG,			
    }
}
