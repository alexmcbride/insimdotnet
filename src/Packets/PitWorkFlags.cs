using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the pit work flags.
    /// </summary>
    [Flags]
    public enum PitWorkFlags {
        /// <summary>
        /// No work done.
        /// </summary>
        PSE_NOTHING = 1,

        /// <summary>
        /// Stopped at pit-box.
        /// </summary>
        PSE_STOP = 2,

        /// <summary>
        /// Front damage.
        /// </summary>
        PSE_FR_DAM = 4,

        /// <summary>
        /// Front wheels changed.
        /// </summary>
        PSE_FR_WHL = 8,

        /// <summary>
        /// Left-front damage.
        /// </summary>
        PSE_LE_FR_DAM = 16,

        /// <summary>
        /// Left-front wheels changed.
        /// </summary>
        PSE_LE_FR_WHL = 32,

        /// <summary>
        /// Right-front damaged.
        /// </summary>
        PSE_RI_FR_DAM = 64,

        /// <summary>
        /// Right-front wheel changed.
        /// </summary>
        PSE_RI_FR_WHL = 128,

        /// <summary>
        /// Rear damage.
        /// </summary>
        PSE_RE_DAM = 256,

        /// <summary>
        /// Rear wheels changed.
        /// </summary>
        PSE_RE_WHL = 512,

        /// <summary>
        /// Left-rear damage.
        /// </summary>
        PSE_LE_RE_DAM = 1024,

        /// <summary>
        /// Left-rear wheel changed.
        /// </summary>
        PSE_LE_RE_WHL = 2048,

        /// <summary>
        /// Right-rear damage.
        /// </summary>
        PSE_RI_RE_DAM = 4096,

        /// <summary>
        /// Right-rear wheel changed.
        /// </summary>
        PSE_RI_RE_WHL = 8192,

        /// <summary>
        /// Minor body damage.
        /// </summary>
        PSE_BODY_MINOR = 16384,

        /// <summary>
        /// Major body damage.
        /// </summary>
        PSE_BODY_MAJOR = 32768,

        /// <summary>
        /// Setup changed.
        /// </summary>
        PSE_SETUP = 65536,

        /// <summary>
        /// Refuelled.
        /// </summary>
        PSE_REFUEL = 131072,
    }
}
