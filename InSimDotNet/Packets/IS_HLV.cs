using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Hot Lap Validity - illegal ground / hit wall / speeding in pit lane
    /// </summary>
    /// <remarks>
    ///  Set the ISP_HLV flag in the IS_ISI to receive reports of incidents that would violate HLVC
    ///  </remarks>
    public class IS_HLV : IPacket {
        /// <summary>
        /// Gets the packet size.
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the packet type.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets the request ID.
        /// </summary>
        public byte ReqI { get; private set; }

        /// <summary>
        /// Gets the unique ID of the player.
        /// </summary>
        public byte PLID { get; private set; }

        /// <summary>
        /// Gets why the lap is invalid.
        /// </summary>
        public HlvcFlags HLVC { get; private set; }

        /// <summary>
        /// Gets the looping time stamp (time since reset - like TINY_GTH).
        /// </summary>
        public TimeSpan Time { get; private set; }

        /// <summary>
        /// Gets the car contact object.
        /// </summary>
        public CarContOBJ C { get; private set; }

        /// <summary>
        /// Creates a new <see cref="IS_HLV"/> object.
        /// </summary>
        /// <param name="buffer">The data to populate the packet with.</param>
        public IS_HLV(byte[] buffer) {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            PLID = reader.ReadByte();
            HLVC = (HlvcFlags)reader.ReadByte();
            reader.Skip(1);
            Time = TimeSpan.FromMilliseconds(reader.ReadUInt16() * 10);
            C = new CarContOBJ(reader);
        }
    }
}
