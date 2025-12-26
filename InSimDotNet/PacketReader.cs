using System;

namespace InSimDotNet {
    /// <summary>
    /// Class to handle reading a packet.
    /// </summary>
    public class PacketReader {
        private readonly byte[] buffer;
        private int position;

        /// <summary>
        /// Creates a new instance of the <see cref="PacketReader"/> class.
        /// </summary>
        /// <param name="buffer">An array of bytes containing the packet data.</param>
        public PacketReader(byte[] buffer) {
            if (buffer == null) {
                throw new ArgumentNullException("buffer");
            }

            this.buffer = buffer;
        }

        /// <summary>
        /// Skips the specified bytes.
        /// </summary>
        /// <param name="count">The number of bytes to skip.</param>
        public void Skip(int count) {
            position += count;
        }

        /// <summary>
        /// Reads packet size (multiplies it by 4) from the buffer.
        /// </summary>
        /// <returns>Actual packet size in bytes</returns>
        public int ReadSize()
        {
            return buffer[position++] * 4;
        }

        /// <summary>
        /// Reads a byte from the buffer.
        /// </summary>
        /// <returns>A single byte.</returns>
        public byte ReadByte() {
            return buffer[position++];
        }

        /// <summary>
        /// Reads a char from the buffer.
        /// </summary>
        /// <returns>A single char.</returns>
        public char ReadChar() {
            return (char)ReadByte();
        }

        /// <summary>
        /// Reads a sequence of bytes from the buffer.
        /// </summary>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>An array of bytes.</returns>
        public byte[] ReadBytes(int count) {
            byte[] value = new byte[count];
            Buffer.BlockCopy(buffer, position, value, 0, count);
            position += count;
            return value;
        }

        /// <summary>
        /// Reads a word from the buffer.
        /// </summary>
        /// <returns>A 2-byte unsigned integer.</returns>
        [CLSCompliant(false)]
        public ushort ReadUInt16() {
            position += 2;
            return BitConverter.ToUInt16(buffer, position - 2);
        }

        /// <summary>
        /// Reads a short from the buffer.
        /// </summary>
        /// <returns>A 2-byte signed integer</returns>
        public short ReadInt16() {
            position += 2;
            return BitConverter.ToInt16(buffer, position - 2);
        }

        /// <summary>
        /// Reads a unsigned from the buffer.
        /// </summary>
        /// <returns>A 4-byte unsigned integer</returns>
        [CLSCompliant(false)]
        public uint ReadUInt32() {
            position += 4;
            return BitConverter.ToUInt32(buffer, position - 4);
        }

        /// <summary>
        /// Reads an integer from the buffer.
        /// </summary>
        /// <returns>A 4-byte signed integer</returns>
        public int ReadInt32() {
            position += 4;
            return BitConverter.ToInt32(buffer, position - 4);
        }

        /// <summary>
        /// Reads a float from the buffer.
        /// </summary>
        /// <returns>A 4-byte floating point number</returns>
        public float ReadSingle() {
            position += 4;
            return BitConverter.ToSingle(buffer, position - 4);
        }

        /// <summary>
        /// Reads a LFS Car name or modded car skinID (as of 0.6W)
        /// </summary>
        /// <returns>A Unicode string.</returns>
        public string ReadCNameString(out byte[] rawBytes) {
            const int count = 4;
            position += count;

            rawBytes = new byte[count];
            Buffer.BlockCopy(buffer, position - count, rawBytes, 0, count);

            if (IsAlphaNumeric(rawBytes[0]) && IsAlphaNumeric(rawBytes[1]) && IsAlphaNumeric(rawBytes[2]))
            {
                return LfsEncoding.Current.GetString(buffer, position - count, count);
            }

            return rawBytes[2].ToString("X2") + rawBytes[1].ToString("X2") + rawBytes[0].ToString("X2");

            bool IsAlphaNumeric(byte b)
            {
                if (b >= '0' && b <= '9') return true;
                if (b >= 'A' && b <= 'Z') return true;
                if (b >= 'a' && b <= 'z') return true;
                return false;
            }
        }

        /// <summary>
        /// Reads a LFS encoded string from the buffer and
        /// sets the rawBytes parameter to the original bytes.
        /// </summary>
        /// <param name="count">The number of bytes to read.</param>
        /// <param name="rawBytes">Raw bytes used to create the string.</param>
        /// <returns>A Unicode string.</returns>
        public string ReadString(int count, out byte[] rawBytes) {
            position += count;
            rawBytes = new byte[count];
            Buffer.BlockCopy(buffer, position - count, rawBytes, 0, count);
            return LfsEncoding.Current.GetString(rawBytes, 0, count);
        }

        /// <summary>
        /// Reads a Boolean from the buffer.
        /// </summary>
        /// <returns>A Boolean.</returns>
        public bool ReadBoolean() {
            return ReadByte() > 0;
        }

        /// <summary>
        /// Reads a signed byte from the buffer.
        /// </summary>
        /// <returns>An signed byte.</returns>
        [CLSCompliant(false)]
        public sbyte ReadSByte() {
            return (sbyte)ReadByte();
        }
    }
}
