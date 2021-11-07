using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// OBject Hit - car hit an autocross object or an unknown object.
    /// </summary>
    public class IS_OBH : IPacket {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets the request ID.
        /// </summary>
        public byte ReqI { get; private set; }

        /// <summary>
        /// Gets the player's unique ID.
        /// </summary>
        public byte PLID { get; private set; }

        /// <summary>
        /// Gets the closing speed (high 4 bits : reserved / low 12 bits : closing speed (10 = 1 m/s)).
        /// </summary>
        public int SpClose { get; private set; }

        /// <summary>
        /// Gets the looping time stamp (time since reset - like TINY_GTH).
        /// </summary>
        public TimeSpan Time { get; private set; }

        /// <summary>
        /// Gets the car in contact with an object.
        /// </summary>
        public CarContOBJ C { get; private set; }

        /// <summary>
        /// Gets the X position of the object (1 meter = 16).
        /// </summary>
        public short X { get; private set; }

        /// <summary>
        /// Gets the Y position of the object (1 meter = 16).
        /// </summary>
        public short Y { get; private set; }

        /// <summary>
        /// Gets Zbyte as in ObjectInfo if OBH_LAYOUT is set. 
        /// </summary>
        public byte Zbyte { get; private set; }

        /// <summary>
        /// Gets the object index or zero if it is an unknown object.
        /// </summary>
        public byte Index { get; private set; }

        /// <summary>
        /// Gets the object flags.
        /// </summary>
        public ObjectFlags OBHFlags { get; private set; }

        /// <summary>
        /// Creates a new <see cref="IS_OBH"/> object.
        /// </summary>
        /// <param name="buffer">The packet data.</param>
        public IS_OBH(byte[] buffer) {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            PLID = reader.ReadByte();
            SpClose = reader.ReadUInt16();
            Time = TimeSpan.FromMilliseconds(reader.ReadUInt16() * 10);
            C = new CarContOBJ(reader);
            X = reader.ReadInt16();
            Y = reader.ReadInt16();
            Zbyte = reader.ReadByte();
            reader.Skip(1);
            Index = reader.ReadByte();
            OBHFlags = (ObjectFlags)reader.ReadByte();
        }
    };
}
