using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Button function packet.
    /// </summary>
    /// <remarks>
    /// Used to delete InSim buttons.
    /// </remarks>
    public class IS_BFN : IPacket, ISendable {
        /// <summary>
        /// Gets the packet size.
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the packet type.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets or sets the packet request ID.
        /// </summary>
        public byte ReqI { get; set; }

        /// <summary>
        /// Gets or sets the sub-type of the <see cref="IS_BFN"/> packet.
        /// </summary>
        public ButtonFunction SubT { get; set; }

        /// <summary>
        /// Gets or sets the connection to send to or from (0 = local / 255 = all).
        /// </summary>
        public byte UCID { get; set; }

        /// <summary>
        /// Gets or sets the ID of the button to delete (if SubT is BFN_DEL_BTN).
        /// </summary>
        public byte ClickID { get; set; }

        /// <summary>
        /// Gets or sets the ID of the last button in the range (if SubT is BFN_DEL_BTN and greater than ClickID).
        /// </summary>
        public byte ClickMax { get; set; }

        /// <summary>
        /// Used internally by InSim.
        /// </summary>
        public byte Inst { get; set; }

        /// <summary>
        /// Creates a new button function packet.
        /// </summary>
        public IS_BFN() {
            Size = 8;
            Type = PacketType.ISP_BFN;
        }

        /// <summary>
        /// Creates a new button function packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_BFN(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            SubT = (ButtonFunction)reader.ReadByte();
            UCID = reader.ReadByte();
            ClickID = reader.ReadByte();
            ClickMax = reader.ReadByte();
            Inst = reader.ReadByte();
        }

        /// <summary>
        /// Returns the packet data.
        /// </summary>
        /// <returns>The packet data.</returns>
        public byte[] GetBuffer() {
            PacketWriter writer = new PacketWriter(Size);
            writer.Write(Size);
            writer.Write((byte)Type);
            writer.Write(ReqI);
            writer.Write((byte)SubT);
            writer.Write(UCID);
            writer.Write(ClickID);
            writer.Write(ClickMax);
            writer.Write(Inst);
            return writer.GetBuffer();
        }
    }
}
