using static InSimDotNet.Helpers.CarSetupHelper;

namespace InSimDotNet.Packets
{
    /// <summary>
    /// Car setup packet (IS_SET)
    /// </summary>
    /// <remarks>
    /// Contains the setup of a player.
    /// Sent when ISF_SET flag is enabled and a player sends a setup.
    /// </remarks>
    public class IS_SET : IPacket
    {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Request ID (always 0 for IS_SET)
        /// </summary>
        public byte ReqI { get; private set; }

        /// <summary>
        /// Player's unique ID
        /// </summary>
        public byte PLID { get; private set; }

        /// <summary>
        /// Skin prefix (4 characters)
        /// </summary>
        public string CName { get; private set; }

        /// <summary>
        /// FuelLoad at start
        /// </summary>
        public byte FuelLoad { get; private set; }
        
        /// <summary>
        /// nearly the same as a setup file without the first 12 bytes (but see note below about gear ratios)
        /// </summary>
        public byte[] Setup { get; private set; }

        /// <summary>
        /// Gets the parsed representation of the current car setup.
        /// </summary>
        private ParsedCarSetup _parsedSetup;
        private bool _parsedSetupInitialized;

        public ParsedCarSetup ParsedSetup
        {
            get
            {
                if (!_parsedSetupInitialized)
                {
                    _parsedSetup = ParseSetup(Setup, false);
                    _parsedSetupInitialized = true;
                }
                return _parsedSetup;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IS_SET"/> class.
        /// </summary>
        public IS_SET()
        {
            Size = 136;
            Type = PacketType.ISP_SET;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IS_SET"/> class from a byte array.
        /// </summary>
        /// <param name="buffer">The byte array to initialize from.</param>
        public IS_SET(byte[] buffer)
            : this()
        {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadSize();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            PLID = reader.ReadByte();
            CName = reader.ReadCNameString();
            reader.Skip(4);
            FuelLoad = reader.ReadByte();
            reader.Skip(3);
            Setup = reader.ReadBytes(120);
        }
    }
}
