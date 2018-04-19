using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the <see cref="IS_PLC"/> Cars property.
    /// </summary>
    [Flags]
    public enum CarFlags : long {
        /// <summary>
        /// No cars.
        /// </summary>
        None = 0,

        /// <summary>
        /// XF GTI.
        /// </summary>
        XFG = 1,

        /// <summary>
        /// XR GT.
        /// </summary>
        XRG = 2,

        /// <summary>
        /// XR GT TURBO.
        /// </summary>
        XRT = 4,

        /// <summary>
        /// RB4 GT.
        /// </summary>
        RB4 = 8,

        /// <summary>
        /// FXO TURBO.
        /// </summary>
        FXO = 0x10,

        /// <summary>
        /// LX4.
        /// </summary>
        LX4 = 0x20,

        /// <summary>
        /// LX6.
        /// </summary>
        LX6 = 0x40,

        /// <summary>
        /// MRT5.
        /// </summary>
        MRT = 0x80,

        /// <summary>
        /// UF 1000.
        /// </summary>
        UF1 = 0x100,

        /// <summary>
        /// Raceabout.
        /// </summary>
        RAC = 0x200,

        /// <summary>
        /// FZ50.
        /// </summary>
        FZ5 = 0x400,

        /// <summary>
        /// FORMULA XR.
        /// </summary>
        FOX = 0x800,

        /// <summary>
        /// XF GTR.
        /// </summary>
        XFR = 0x1000,

        /// <summary>
        /// UF GTR.
        /// </summary>
        UFR = 0x2000,

        /// <summary>
        /// FORMULA V8.
        /// </summary>
        FO8 = 0x4000,

        /// <summary>
        /// FXO GTR.
        /// </summary>
        FXR = 0x8000,

        /// <summary>
        /// XR GTR.
        /// </summary>
        XRR = 0x10000,

        /// <summary>
        /// FZ50 GTR.
        /// </summary>
        FZR = 0x20000,

        /// <summary>
        /// BMW SAUBER F1.
        /// </summary>
        BF1 = 0x40000,

        /// <summary>
        /// FORMULA BMW FB02.
        /// </summary>
        FBM = 0x80000,

        /// <summary>
        /// All cars.
        /// </summary>
        All = 0xffffffff,
    }
}
