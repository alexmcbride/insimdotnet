using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Replay information packet.
    /// </summary>
    /// <remarks>
    /// Used to control replay playback (when request completed one of these is sent in conformation).
    /// </remarks>
    public class IS_RIP : IPacket, ISendable {
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
        /// Gets the error associated with the replay.
        /// </summary>
        public ReplayError Error { get; private set; }

        /// <summary>
        /// Gets or sets if the replay is single player or multiplayer.
        /// </summary>
        public ReplayMode MPR { get; set; }

        /// <summary>
        /// Gets or sets if the replay is paused (request: pause on arrival / reply: paused state).
        /// </summary>
        public bool Paused { get; set; }

        /// <summary>
        /// Gets or sets the replay options.
        /// </summary>
        public ReplayOptions Options { get; set; }

        /// <summary>
        /// Gets or sets the current time in the replay (request: destination / reply: position).
        /// </summary>
        public TimeSpan CTime { get; set; }

        /// <summary>
        /// Gets or sets the total time of the replay
        /// </summary>
        public TimeSpan TTime { get; set; }

        /// <summary>
        /// Gets or sets the replay name.
        /// </summary>
        public string RName { get; set; }

        /// <summary>
        /// Creates a new replay information packet.
        /// </summary>
        public IS_RIP() {
            Size = 80;
            Type = PacketType.ISP_RIP;
            RName = String.Empty;
        }

        /// <summary>
        /// Creates a new replay information packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_RIP(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            Error = (ReplayError)reader.ReadByte();
            MPR = (ReplayMode)reader.ReadByte();
            Paused = reader.ReadBoolean();
            Options = (ReplayOptions)reader.ReadByte();
            reader.Skip(1);

            // Times here are in hundredths, for some reason.
            CTime = TimeSpan.FromMilliseconds(reader.ReadUInt32() * 10);
            TTime = TimeSpan.FromMilliseconds(reader.ReadUInt32() * 10);

            RName = reader.ReadString(64);
        }

        /// <summary>
        /// Gets the packet data.
        /// </summary>
        /// <returns>The packet data.</returns>
        public byte[] GetBuffer() {
            PacketWriter writer = new PacketWriter(Size);
            writer.Write(Size);
            writer.Write((byte)Type);
            writer.Write(ReqI);
            writer.Write((byte)Error);
            writer.Write((byte)MPR);
            writer.Write(Paused);
            writer.Write((byte)Options);
            writer.Skip(1);

            // Convert back to hundredths.
            writer.Write((uint)CTime.TotalMilliseconds / 10);
            writer.Write((uint)TTime.TotalMilliseconds / 10);

            writer.Write(RName, 64);
            return writer.GetBuffer();
        }
    }

}
