namespace InSimDotNet.Packets {
    /// <summary>
    /// User Control Object - reports crossing an InSim checkpoint / entering an InSim circle (from layout)
    /// </summary>
    public class IS_UCO : IPacket {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets the packet request ID.
        /// </summary>
        public byte ReqI { get; private set; }

        /// <summary>
        /// Gets the player's unique ID.
        /// </summary>
        public byte PLID { get; private set; }

        /// <summary>
        /// Gets the action flags.
        /// </summary>
        public UCOAction UCOAction { get; private set; }

        /// <summary>
        /// Gets the time in hundredths of a second since start (as in SMALL_RTP).
        /// </summary>
        public long Time { get; private set; }

        /// <summary>
        /// Gets the car contact object for this event.
        /// </summary>
        public CarContOBJ C { get; private set; }

        /// <summary>
        /// Gets info about the checkpoint or circle (see InSim.txt).
        /// </summary>
        public ObjectInfo Info { get; private set; }

        /// <summary>
        /// Creates a new IS_UCO object.
        /// </summary>
        /// <param name="buffer">The buffer containing the packet data.</param>
        public IS_UCO(byte[] buffer) {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            PLID = reader.ReadByte();
            reader.Skip(1);
            UCOAction = (UCOAction)reader.ReadByte();
            reader.Skip(2);
            Time = reader.ReadUInt32();
            C = new CarContOBJ(reader);
            Info = new ObjectInfo(reader);
        }
    }
}
