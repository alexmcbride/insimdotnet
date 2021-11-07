using System;

namespace InSimDotNet {
    /// <summary>
    /// Class to manage writing a packet.
    /// </summary>
    public class PacketWriter {
        private readonly byte[] buffer;
        private int position;

        /// <summary>
        /// Creates a new instance of the <see cref="PacketWriter"/> class.
        /// </summary>
        /// <param name="size">The size of the packet that will be written.</param>
        public PacketWriter(int size) {
            buffer = new byte[size];
        }

        /// <summary>
        /// Returns the underlying buffer.
        /// </summary>
        /// <returns>An array of bytes representing the packet.</returns>
        public byte[] GetBuffer() {
            return buffer;
        }

        /// <summary>
        /// Skips the specified bytes.
        /// </summary>
        /// <param name="count">The number of bytes to skip.</param>
        public void Skip(int count) {
            position += count;
        }

        /// <summary>
        /// Writes the specified value to the buffer.
        /// </summary>
        /// <param name="value">A single byte.</param>
        public void Write(byte value) {
            buffer[position++] = value;
        }

        /// <summary>
        /// Writes the specified value to the buffer.
        /// </summary>
        /// <param name="value">A single signed byte.</param>
        public void Write(sbyte value) {
            buffer[position++] = (byte)value;
        }

        /// <summary>
        /// Writes the specified sequence of bytes to the buffer.
        /// </summary>
        /// <param name="value">An array of bytes to write</param>
        public void Write(byte[] value) {
            if (value == null) {
                throw new ArgumentNullException("value");
            }

            Write(value, value.Length);
        }

        /// <summary>
        /// Writes the specified sequence of bytes to the buffer.
        /// </summary>
        /// <param name="value">An array of bytes to write</param>
        /// <param name="count">The number of bytes to write.</param>
        public void Write(byte[] value, int count) {
            if (value == null) {
                throw new ArgumentNullException("value");
            }

            Buffer.BlockCopy(value, 0, buffer, position, count);
            position += count;
        }

        /// <summary>
        /// Writes the specified value to the buffer.
        /// </summary>
        /// <param name="value">A 2-byte unsigned short.</param>
        public void Write(ushort value) {
            Write(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes the specified value to the buffer.
        /// </summary>
        /// <param name="value">A 2-byte signed short.</param>
        public void Write(short value) {
            Write(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes the specified value to the buffer.
        /// </summary>
        /// <param name="value">A 4-byte unsigned integer.</param>
        public void Write(uint value) {
            Write(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes the specified value to the buffer.
        /// </summary>
        /// <param name="value">A 4-byte signed integer.</param>
        public void Write(int value) {
            Write(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes the specified value to the buffer.
        /// </summary>
        /// <param name="value">A 4-byte floating point number.</param>
        public void Write(float value) {
            Write(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes the specified value to the buffer.
        /// </summary>
        /// <param name="value">A single character.</param>
        public void Write(char value) {
            Write((byte)value);
        }

        /// <summary>
        /// Writes the specified Unicode string to the buffer as a LFS encoded string.
        /// </summary>
        /// <param name="value">A Unicode string.</param>
        /// <param name="length">The maximum length of the string to write.</param>
        public void Write(string value, int length) {
            if (value == null) {
                throw new ArgumentNullException("value");
            }

            LfsEncoding.Current.GetBytes(value, buffer, position, length);
            position += length;
        }

        /// <summary>
        /// Writes the specified value to the buffer.
        /// </summary>
        /// <param name="value">A Boolean.</param>
        public void Write(bool value) {
            Write(value ? (byte)1 : (byte)0);
        }
    }
}
