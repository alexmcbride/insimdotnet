using System;
using System.Diagnostics.CodeAnalysis;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the pit work flags.
    /// </summary>
    [Flags]
    public enum PitWorkFlags {
        /// <summary>
        /// No work done.
        /// </summary>
        PSE_NOTHING,		

        /// <summary>
        /// Stopped at pit-box.
        /// </summary>
        PSE_STOP,			

        /// <summary>
        /// Front damage.
        /// </summary>
        PSE_FR_DAM,			

        /// <summary>
        /// Front wheels changed.
        /// </summary>
        PSE_FR_WHL,			

        /// <summary>
        /// Left-front damage.
        /// </summary>
        PSE_LE_FR_DAM,

        /// <summary>
        /// Left-front wheels changed.
        /// </summary>
        PSE_LE_FR_WHL,

        /// <summary>
        /// Right-front damaged.
        /// </summary>
        PSE_RI_FR_DAM,

        /// <summary>
        /// Right-front wheel changed.
        /// </summary>
        PSE_RI_FR_WHL,

        /// <summary>
        /// Rear damage.
        /// </summary>
        PSE_RE_DAM,

        /// <summary>
        /// Rear wheels changed.
        /// </summary>
        PSE_RE_WHL,

        /// <summary>
        /// Left-rear damage.
        /// </summary>
        PSE_LE_RE_DAM,

        /// <summary>
        /// Left-rear wheel changed.
        /// </summary>
        PSE_LE_RE_WHL,

        /// <summary>
        /// Right-rear damage.
        /// </summary>
        PSE_RI_RE_DAM,

        /// <summary>
        /// Right-rear wheel changed.
        /// </summary>
        PSE_RI_RE_WHL,

        /// <summary>
        /// Minor body damage.
        /// </summary>
        PSE_BODY_MINOR,

        /// <summary>
        /// Major body damage.
        /// </summary>
        PSE_BODY_MAJOR,

        /// <summary>
        /// Setup changed.
        /// </summary>
        PSE_SETUP,

        /// <summary>
        /// Refuelled.
        /// </summary>
        PSE_REFUEL,
    }
}
