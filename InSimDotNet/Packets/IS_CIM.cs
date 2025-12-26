using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Connection interface mode packet.
    /// </summary>
    /// <remarks>
    /// Reports a connection's interface mode.
    /// </remarks>
    public class IS_CIM : IPacket {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets the packet request ID, as received in the <see cref="IS_BTN"/> packet.
        /// </summary>
        public byte ReqI { get; private set; }

        /// <summary>
        /// Gets the unique ID of the connection that used the button (zero if local).
        /// </summary>
        public byte UCID { get; private set; }

        /// <summary>
        /// Gets which mode the connection is currently in.
        /// </summary>
        public ModeIdentifier Mode { get; private set; }

        /// <summary>
        /// Gets the submode identifiers. Its value depends of the connection mode.
        /// Corresponding enums for each supported mode are available.
        /// </summary>
        public byte SubMode { get; private set; }

        /// <summary>
        /// Gets the selected object type (or zero if unselected).
        /// It may be an AXO_x as in ObjectInfo or one of the marshall enum values.
        /// </summary>
        public AxoObject SelType { get; private set; }

        /// <summary>
        /// Creates a new connection interface mode packet.
        /// </summary>
        public IS_CIM() {
            Size = 8;
            Type = PacketType.ISP_CIM;
        }

        /// <summary>
        /// Creates a new connection interface mode packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_CIM(byte[] buffer)
            : this() {
            var reader = new PacketReader(buffer);
            Size = reader.ReadSize();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            UCID = reader.ReadByte();
            Mode = (ModeIdentifier)reader.ReadByte();
            SubMode = reader.ReadByte();
            SelType = (AxoObject)reader.ReadByte();
            reader.Skip(1);
        }
    }
}
