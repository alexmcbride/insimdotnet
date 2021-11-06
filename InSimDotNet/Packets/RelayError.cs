using System.Diagnostics.CodeAnalysis;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the <see cref="IR_ERR"/> ErrNo.
    /// </summary>
    public enum RelayError {
        /// <summary>
        /// None.
        /// </summary>
        IR_ERR_NONE = 0,

        /// <summary>
        /// Invalid packet sent by client (wrong structure / length).
        /// </summary>
        IR_ERR_PACKET = 1,

        /// <summary>
        /// Invalid packet sent by client (packet was not allowed to be forwarded to host).
        /// </summary>
        IR_ERR_PACKET2 = 2,

        /// <summary>
        /// Wrong hostname given by client.
        /// </summary>
        IR_ERR_HOSTNAME = 3,

        /// <summary>
        /// Wrong admin pass given by client.
        /// </summary>
        IR_ERR_ADMIN = 4,

        /// <summary>
        /// Wrong spec pass given by client.
        /// </summary>
        IR_ERR_SPEC = 5,

        /// <summary>
        /// Spectator pass required, but none given.
        /// </summary>
        IR_ERR_NOSPEC = 6,
    }
}
