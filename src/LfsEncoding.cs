using System;
using System.Text;

namespace InSimDotNet {
    /// <summary>
    /// Abstract class for implementing LFS string handling.
    /// </summary>
    public abstract class LfsEncoding {
        private static Func<LfsEncoding> factory = new Func<LfsEncoding>(() => new LfsDefaultEncoding());
        private static Lazy<LfsEncoding> instance = new Lazy<LfsEncoding>(factory);

        /// <summary>
        /// Gets or sets the current encoding for InSim.NET to use when converting strings.
        /// </summary>
        public static LfsEncoding Instance {
            get { return instance.Value; }
        }

        /// <summary>
        /// Gets or sets the factory used to create a LFS encoding type.
        /// </summary>
        public static Func<LfsEncoding> Factory {
            get { return factory; }
            set {
                factory = value;
                instance = new Lazy<LfsEncoding>(factory);
            }
        }

        static LfsEncoding() {
            // Need to register provider to make code pages available on .NET Core.
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        /// <summary>
        /// Override to implement code to convert bytes into string.
        /// </summary>
        /// <param name="buffer">The buffer containing the bytes</param>
        /// <param name="index">The index of the first byte of the string in the buffer.</param>
        /// <param name="length">The length of the string in bytes.</param>
        /// <returns>The converted string.</returns>
        public abstract string GetString(byte[] buffer, int index, int length);

        /// <summary>
        /// Override to implement code to convert string into bytes.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <param name="buffer">The buffer to write the bytes into.</param>
        /// <param name="index">The index for where to start writing into the buffer.</param>
        /// <param name="length">The maximum length the string can be.</param>
        /// <returns>The number bytes written into the buffer.</returns>
        public abstract int GetBytes(string value, byte[] buffer, int index, int length);
    }
}