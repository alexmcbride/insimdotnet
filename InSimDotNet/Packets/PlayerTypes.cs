using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the <see cref="IS_NPL"/> PType.
    /// </summary>
    [Flags]
    public enum PlayerTypes {
        /// <summary>
        /// Player is female.
        /// </summary>
        PLT_FEMALE = 1,

        /// <summary>
        /// Player is AI.
        /// </summary>
        PLT_AI = 2,

        /// <summary>
        /// Player is remote.
        /// </summary>
        PLT_REMOTE = 4,
    }
}
