namespace InSimDotNet.Packets {
    /// <summary>
    /// SeLected Car - sent when a connection selects a car (empty if no car)
    /// </summary>
    public class IS_SLC : IPacket {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets the request ID - 0 unless this is a reply to a TINY_SLC request
        /// </summary>
        public byte ReqI { get; private set; }    

        /// <summary>
        /// Gets the connection's unique ID (0 = host)
        /// </summary>
        public byte UCID { get; private set; }   
        
        /// <summary>
        /// Gets the car name.
        /// </summary>
        public string CName { get; private set; }

        /// <summary>
        /// Creates a new IS_SLC packet.
        /// </summary>
        /// <param name="buffer">A buffer containing the packet byte data.</param>
        public IS_SLC(byte[] buffer) {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            UCID = reader.ReadByte();
            CName = reader.ReadString(4);
        }
    }
}
