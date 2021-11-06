using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Button type packet.
    /// </summary>
    /// <remarks>
    /// Sent when user types text into a text-entry button.
    /// </remarks>
    public class IS_BTT : IPacket {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public byte Size { get; private set; }

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
        /// Gets the button identifier originally sent in the <see cref="IS_BTN"/> packet.
        /// </summary>
        public byte ClickID { get; private set; }

        /// <summary>
        /// Used internally by InSim.
        /// </summary>
        public byte Inst { get; private set; }

        /// <summary>
        /// Gets the max number of character the user can type in.
        /// </summary>
        public byte TypeIn { get; private set; }

        /// <summary>
        /// Gets the text the user entered.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Creates a new button type packet.
        /// </summary>
        public IS_BTT() {
            Size = 104;
            Type = PacketType.ISP_BTT;
            Text = String.Empty;
        }

        /// <summary>
        /// Creates a new button type packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_BTT(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            UCID = reader.ReadByte();
            ClickID = reader.ReadByte();
            Inst = reader.ReadByte();
            TypeIn = reader.ReadByte();
            reader.Skip(1);
            Text = reader.ReadString(96);
        }
    }
}
