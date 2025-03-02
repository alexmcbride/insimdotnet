using InSimDotNet.Out;
using System;

namespace InSimDotNet.Packets
{
    public class IS_AII : IPacket
    {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet. ISP_AII
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Optional - returned in any immediate response e.g. reply to CS_SEND_AI_INFO
        /// </summary>
        public byte ReqI { get; set; }

        /// <summary>
        /// Unique ID of AI driver to control
        /// </summary>
        public byte PLID { get; private set; }

        /// <summary>
        /// identical to OutSimMain (main data in OutSim packet)
        /// </summary>
        public OSMain OSData { get; private set; }

        /// <summary>
        /// Flags
        /// </summary>
        public AIFlags Flags { get; private set; }

        /// <summary>
        /// Current gear.Reverse:0, Neutral:1, First:2...
        /// </summary>
        public byte Gear { get; private set; }

        /// <summary>
        /// Spare
        /// </summary>
        public byte Sp2 { get; private set; }

        /// <summary>
        /// Spare
        /// </summary>
        public byte Sp3 { get; private set; }

        /// <summary>
        /// RPM
        /// </summary>
        public float RPM { get; set; }

        /// <summary>
        /// Spare
        /// </summary>
        public float SpF0 { get; set; }

        /// <summary>
        /// Spare
        /// </summary>
        public float SpF1 { get; set; }

        /// <summary>
        /// Dash lights currently switched on
        /// </summary>
        public DashLightFlags ShowLights { get; set; } // Dash lights currently switched on (see DL_x in OutGauge section below)

        /// <summary>
        /// Spare
        /// </summary>
        public long SPU1 { get; set; }

        /// <summary>
        /// Spare
        /// </summary>
        public long SPU2 { get; set; }

        /// <summary>
        /// Spare
        /// </summary>
        public long SPU3 { get; set; }

        /// <summary>
        /// Creates a new AI Info packet.
        /// </summary>
        public IS_AII()
        {
            Size = 96;
            Type = PacketType.ISP_AII;
        }

        /// <summary>
        /// Creates a new AI Info packet.
        /// </summary>
        /// <param name="buffer">A buffer containing the packet data.</param>
        public IS_AII(byte[] buffer)
            : this()
        {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadSize();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            PLID = reader.ReadByte();

            OSData = new OSMain(reader);

            Flags = (AIFlags)reader.ReadByte();
            Gear = reader.ReadByte();
            reader.Skip(1); //Sp2
            reader.Skip(1); //Sp3

            RPM = reader.ReadSingle();
            reader.Skip(4); //SpF0
            reader.Skip(4); //SpF1

            ShowLights = (DashLightFlags)reader.ReadUInt32();
            SPU1 = reader.ReadUInt32(); //SPU1
            SPU2 = reader.ReadUInt32(); //SPU2
            SPU3 = reader.ReadUInt32(); //SPU3
        }
    }

    /// <summary>
    /// AIFlags
    /// </summary>
    [Flags]
    public enum AIFlags
    {
        /// <summary>
        /// detect if engine running
        /// </summary>
        AIFLAGS_IGNITION = 1,
        /// <summary>
        /// upshift lever currently held
        /// </summary>
        AIFLAGS_CHUP = 4,
        /// <summary>
        /// downshift lever currently held
        /// </summary>
        AIFLAGS_CHDN = 8,
    }
}
