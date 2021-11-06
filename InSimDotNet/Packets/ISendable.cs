using System.Diagnostics.CodeAnalysis;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents an InSim packet which can be sent to LFS.
    /// </summary>
    public interface ISendable {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        byte Size { get; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        PacketType Type { get; }

        /// <summary>
        /// Sets the request ID.
        /// </summary>
        byte ReqI { set; get; }

        /// <summary>
        /// Gets the packet data as a buffer array.
        /// </summary>
        /// <returns>The packet data.</returns>
        byte[] GetBuffer();
    }
}
