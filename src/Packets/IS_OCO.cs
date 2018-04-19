namespace InSimDotNet.Packets {
    /// <summary>
    /// Object COntrol - currently used for switching start lights
    /// </summary>
    public class IS_OCO : ISendable {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets the packet request ID
        /// </summary>
        public byte ReqI { get; set; }

        /// <summary>
        /// Gets the object control actions.
        /// </summary>
        public OCOAction OCOAction { get; set; }

        /// <summary>
        /// Gets or sets the index of the start light object, either from AutoX AXO_START_LIGHTS, 
        /// or set IS_OCO.OCO_INDEX_MAIN to overide the main start light system.
        /// </summary>
        public OCOIndex Index { get; set; }

        /// <summary>
        /// Gets or sets particular start light objects (0 to 63 or 255 = all), corresponds with the 
        /// indentifier in the layout editor.
        /// </summary>
        public byte Identifier { get; set; }

        /// <summary>
        /// Gets or sets a particular start bulb.
        /// </summary>
        public BulbInfo Data { get; set; }

        /// <summary>
        /// Creates a new IS_OCO object.
        /// </summary>
        public IS_OCO() {
            Size = 8;
            Type = PacketType.ISP_OCO;
        }

        /// <summary>
        /// Gets the buffer for this packet.
        /// </summary>
        /// <returns>An array of bytes representing the packet.</returns>
        public byte[] GetBuffer() {
            PacketWriter writer = new PacketWriter(Size);
            writer.Write(Size);
            writer.Write((byte)Type);
            writer.Write(ReqI);
            writer.Skip(1);
            writer.Write((byte)OCOAction);
            writer.Write((byte)Index);
            writer.Write(Identifier);
            writer.Write((byte)Data);
            return writer.GetBuffer();
        }
    }
}
