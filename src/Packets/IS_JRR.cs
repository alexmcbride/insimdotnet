namespace InSimDotNet.Packets {
    /// <summary>
    /// JOIN REQUEST - allows external program to decide if a player can join
    /// </summary>
    public class IS_JRR : IPacket, ISendable {
        /// <summary>
        /// Gets the size of the packet
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets or sets the request ID
        /// </summary>
        public byte ReqI { get; set; }

        /// <summary>
        /// Gets or sets the player ID. ZERO when this is a reply to a join request - SET to move a car
        /// </summary>
        public byte PLID { get; set; }

        /// <summary>
        /// Gets or sets the connection ID. Set when this is a reply to a join request - ignored when moving a car
        /// </summary>
        public byte UCID { get; set; }

        /// <summary>
        /// Gets or sets whether the player joining should be accepted or rejected
        /// </summary>
        public JrrAction JRRAction { get; set; }

        /// <summary>
        /// Gets the ObjectInfo object used to specify start point. 0 : use default start point / Flags = 0x80 : set start point
        /// </summary>
        public ObjectInfo StartPos { get; set; }

        /// <summary>
        /// Creates a new IS_JRR object.
        /// </summary>
        public IS_JRR() {
            Size = 16;
            Type = PacketType.ISP_JRR;
            StartPos = new ObjectInfo();
        }

        /// <summary>
        /// Gets the packet buffer.
        /// </summary>
        /// <returns>The buffer.</returns>
        public byte[] GetBuffer() {
            var writer = new PacketWriter(Size);
            writer.Write(Size);
            writer.Write((byte)Type);
            writer.Write(ReqI);
            writer.Write(PLID);
            writer.Write(UCID);
            writer.Write((byte)JRRAction);
            writer.Skip(2);
            StartPos.GetBuffer(writer);
            return writer.GetBuffer();
        }
    }
}
