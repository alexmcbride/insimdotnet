using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Camera position packet.
    /// </summary>
    /// <remarks>
    /// Used to control camera position, in car or in Shift+U mode. To request one to be 
    /// sent send a <see cref="IS_TINY"/> with a SubT of TINY_SCP.
    /// </remarks>
    public class IS_CPP : IPacket, ISendable {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets or sets the packet request ID.
        /// </summary>
        public byte ReqI { get; set; }

        /// <summary>
        /// Gets or sets the camera position vector.
        /// </summary>
        public Vec Pos { get; set; }

        /// <summary>
        /// Gets or sets the heading - 0 points along Y axis.
        /// </summary>
        public int H { get; set; }

        /// <summary>
        /// Gets or sets the pitch - 0 means looking at horizon.
        /// </summary>
        public int P { get; set; }

        /// <summary>
        /// Gets or sets the roll - 0 means no roll.
        /// </summary>
        public int R { get; set; }

        /// <summary>
        /// Gets or sets the unique ID of the viewed player (0 = none).
        /// </summary>
        public byte ViewPLID { get; set; }

        /// <summary>
        /// Gets or sets the current camera, as reported in <see cref="IS_SFP"/>.
        /// </summary>
        public ViewIndentifier InGameCam { get; set; }

        /// <summary>
        /// Gets or sets the field of view.
        /// </summary>
        public float FOV { get; set; }

        /// <summary>
        /// Gets or sets the time to move to this camera position (0 = instant).
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Gets or sets the state flags.
        /// </summary>
        public StateFlags Flags { get; set; }

        /// <summary>
        /// Creates a new camera position packet.
        /// </summary>
        public IS_CPP() {
            Size = 32;
            Type = PacketType.ISP_CPP;
        }

        /// <summary>
        /// Creates a new camera position packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_CPP(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            reader.Skip(1);
            Pos = new Vec(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
            H = reader.ReadUInt16();
            P = reader.ReadUInt16();
            R = reader.ReadUInt16();
            ViewPLID = reader.ReadByte();
            InGameCam = (ViewIndentifier)reader.ReadByte();
            FOV = reader.ReadSingle();
            Time = TimeSpan.FromMilliseconds(reader.ReadUInt16());
            Flags = (StateFlags)reader.ReadUInt16();
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
            writer.Skip(1);
            writer.Write(Pos.X);
            writer.Write(Pos.Y);
            writer.Write(Pos.Z);
            writer.Write((ushort)H);
            writer.Write((ushort)P);
            writer.Write((ushort)R);
            writer.Write(ViewPLID);
            writer.Write((byte)InGameCam);
            writer.Write(FOV);
            writer.Write((ushort)Time.TotalMilliseconds);
            writer.Write((ushort)Flags);
            return writer.GetBuffer();
        }
    }

}
