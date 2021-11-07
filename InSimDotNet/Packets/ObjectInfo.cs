using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Class for the <see cref="IS_AXM"/> Info collection.
    /// </summary>
    public class ObjectInfo {
        /// <summary>
        /// Gets or sets the X position of the object (1 meter = 16)
        /// </summary>
        public short X { get; set; }

        /// <summary>
        /// Gets or sets the Y position of the object (1 meter = 16)
        /// </summary>
        public short Y { get; set; }

        /// <summary>
        /// Gets or sets the height of the object.
        /// </summary>
        public byte Zbyte { get; set; }

        /// <summary>
        /// Gets or sets the object flags (always 0 for objects).
        /// </summary>
        public byte Flags { get; set; }

        /// <summary>
        /// Gets or sets the object index.
        /// </summary>
        public byte Index { get; set; }

        /// <summary>
        /// Gets or sets the object heading.
        /// </summary>
        public byte Heading { get; set; }

        /// <summary>
        /// Creates a new <see cref="ObjectInfo"/> object.
        /// </summary>
        public ObjectInfo() { }

        /// <summary>
        /// Creates a new ObjectInfo object.
        /// </summary>
        /// <param name="reader">A <see cref="PacketReader"/> containing packet data.</param>
        public ObjectInfo(PacketReader reader) {
            if (reader == null) {
                throw new ArgumentNullException("reader");
            }

            X = reader.ReadInt16();
            Y = reader.ReadInt16();
            Zbyte = reader.ReadByte();
            Flags = reader.ReadByte();
            Index = reader.ReadByte();
            Heading = reader.ReadByte();
        }

        /// <summary>
        /// Writes the <see cref="ObjectInfo"/> object to the specified <see cref="PacketWriter"/>
        /// </summary>
        /// <param name="writer">The <see cref="PacketWriter"/> to write the data to.</param>
        public void GetBuffer(PacketWriter writer) {
            if (writer == null) {
                throw new ArgumentNullException("writer");
            }

            writer.Write(X);
            writer.Write(Y);
            writer.Write(Zbyte);
            writer.Write(Flags);
            writer.Write(Index);
            writer.Write(Heading);
        }
    }
}
