
namespace InSimDotNet.Packets {
    /// <summary>
    /// Flags to represent the IS_AXM PMOFlags attribute.
    /// </summary>
    public enum PMOFlags {
        /// <summary>
        /// Nowt.
        /// </summary>
        NONE = 0,

        /// <summary>
        /// If PMO_FILE_END is set in a PMO_LOADING_FILE packet, LFS has reached the end of a 
        /// layout file which it is loading. The added objects will then be optimised.
        /// </summary>
        PMO_FILE_END = 1,

        /// <summary>
        /// InSim.txt does not include documentation for this bit.
        /// </summary>
        PMO_SUPPRESS_WARNINGS = 2,
    }
}
