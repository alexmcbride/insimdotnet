using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the <see cref="IS_ISI"/> Flags.
    /// </summary>
    [Flags]
    public enum InSimFlags {
        /// <summary>
        /// Not used.
        /// </summary>
        ISF_RES_0 = 1,

        /// <summary>
        /// Not used.
        /// </summary>
        ISF_RES_1 = 2,

        /// <summary>
        /// Guest or single player.
        /// </summary>
        ISF_LOCAL = 4,

        /// <summary>
        /// Keep colors in <see cref="IS_MSO"/> packet text.
        /// </summary>
        ISF_MSO_COLS = 8,

        /// <summary>
        /// Enable <see cref="IS_NLP"/> packets.
        /// </summary>
        ISF_NLP = 16,

        /// <summary>
        /// Enable <see cref="IS_MCI"/> packets.
        /// </summary>
        ISF_MCI = 32,

        /// <summary>
        /// Enable <see cref="IS_CON"/> packets.
        /// </summary>
        ISF_CON = 64,

        /// <summary>
        /// Enable <see cref="IS_OBH"/> packets.
        /// </summary>
        ISF_OBH = 128,

        /// <summary>
        /// Enable <see cref="IS_HLV"/> packets.
        /// </summary>
        ISF_HLV = 256,

        /// <summary>
        /// Enable receive AXM when loading layout.
        /// </summary>
        ISF_AXM_LOAD = 512,

        /// <summary>
        /// Enable receive AXM when changing objects.
        /// </summary>
        ISF_AXM_EDIT = 1024,

        /// <summary>
        /// Enable to get send JRR join request packets.
        /// </summary>
        ISF_REQ_JOIN = 2048,
    }
}
