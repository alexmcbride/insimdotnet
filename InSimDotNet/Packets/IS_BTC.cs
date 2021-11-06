using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Button click packet.
    /// </summary>
    /// <remarks>
    /// Sent when a user clicks a button.
    /// </remarks>
    public class IS_BTC : IPacket {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets the packet request ID as received in the <see cref="IS_BTN"/> packet.
        /// </summary>
        public byte ReqI { get; private set; }

        /// <summary>
        /// Gets the unique ID of the connection that clicked the button.
        /// </summary>
        public byte UCID { get; private set; }

        /// <summary>
        /// Gets the button identifier originally sent in the <see cref="IS_BTN"/> packet.
        /// </summary>
        public byte ClickID { get; private set; }

        /// <summary>
        /// Used internally by InSim.
        /// </summary>
        public byte Inst { get; private set; }

        /// <summary>
        /// Gets the button click flags.
        /// </summary>
        public ClickFlags CFlags { get; private set; }

        /// <summary>
        /// Creates a new button click packet.
        /// </summary>
        public IS_BTC() {
            Size = 8;
            Type = PacketType.ISP_BTC;
        }

        /// <summary>
        /// Creates a new button click packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_BTC(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            UCID = reader.ReadByte();
            ClickID = reader.ReadByte();
            Inst = reader.ReadByte();
            CFlags = (ClickFlags)reader.ReadByte();
        }
    }
}
