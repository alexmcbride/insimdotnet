using System;

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
        /// layout file which it is loading.
        /// </summary>
        PMO_FILE_END = 1,

        /// <summary>
        /// When objects are moved or modified in the layout editor, two IS_AXM packets are
        /// sent.  A PMO_DEL_OBJECTS followed by a PMO_ADD_OBJECTS.  In this case the flag
        /// PMO_MOVE_MODIFY is set in the PMOFlags byte of both packets.
        /// </summary>
        PMO_MOVE_MODIFY = 2,

        /// <summary>
        /// If you send an IS_AXM with PMOAction of PMO_SELECTION it is possible for it to be
        /// either a selection of real objects (as if the user selected several objects while
        /// holding the CTRL key) or a clipboard selection (as if the user pressed CTRL+C after
        /// selecting objects).  Clipboard is the default selection mode.  A real selection can
        /// be set by using the PMO_SELECTION_REAL bit in the PMOFlags byte.
        /// </summary>
        PMO_SELECTION_REAL = 4,

        /// <summary>
        /// If you send an IS_AXM with PMOAction of PMO_ADD_OBJECTS you may wish to set the
        /// UCID to one of the guest connections (for example if that user's action caused the
        /// objects to be added).  In this case some validity checks are done on the guest's
        /// computer which may report "invalid position" or "intersecting object" and delete
        /// the objects.  This can be avoided by setting the PMO_AVOID_CHECK bit.
        /// </summary>
        PMO_AVOID_CHECK = 8
    }
}
