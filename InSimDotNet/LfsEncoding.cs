using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace InSimDotNet {
    /// <summary>
    /// Handles converting strings from LFS encoding into unicode and vice versa.
    /// </summary>
    internal static class LfsEncoding {
        private const char ControlChar = '^';
        private const char FallbackChar = '?';
        private static readonly bool IsRunningOnMono = (Type.GetType("Mono.Runtime") != null);

        // ExceptionFallback is only used by Mono code path, otherwise we want ReplacementFallback everywhere.
        // These codepages don't translate perfectly to LFS but the best we can do in .NET.
        private static readonly Dictionary<char, Encoding> EncodingMap = new Dictionary<char, Encoding> {
            { 'L', Encoding.GetEncoding(1252, EncoderExceptionFallback.ExceptionFallback, DecoderExceptionFallback.ReplacementFallback) },
            { 'G', Encoding.GetEncoding(1253, EncoderExceptionFallback.ExceptionFallback, DecoderExceptionFallback.ReplacementFallback) },
            { 'C', Encoding.GetEncoding(1251, EncoderExceptionFallback.ExceptionFallback, DecoderExceptionFallback.ReplacementFallback) },
            { 'J', Encoding.GetEncoding(932, EncoderExceptionFallback.ExceptionFallback, DecoderExceptionFallback.ReplacementFallback) },
            { 'E', Encoding.GetEncoding(1250, EncoderExceptionFallback.ExceptionFallback, DecoderExceptionFallback.ReplacementFallback) },
            { 'T', Encoding.GetEncoding(1254, EncoderExceptionFallback.ExceptionFallback, DecoderExceptionFallback.ReplacementFallback) },
            { 'B', Encoding.GetEncoding(1257, EncoderExceptionFallback.ExceptionFallback, DecoderExceptionFallback.ReplacementFallback) },
            { 'H', Encoding.GetEncoding(950, EncoderExceptionFallback.ExceptionFallback, DecoderExceptionFallback.ReplacementFallback) },
            { 'S', Encoding.GetEncoding(936, EncoderExceptionFallback.ExceptionFallback, DecoderExceptionFallback.ReplacementFallback) },
            { 'K', Encoding.GetEncoding(949, EncoderExceptionFallback.ExceptionFallback, DecoderExceptionFallback.ReplacementFallback) },
        };

        private static readonly Encoding DefaultEncoding = EncodingMap['L'];

        private static readonly Dictionary<char, char> EscapeMap = new Dictionary<char, char> {
            { 'v', '|' },
            { 'a', '*' },
            { 'c', ':' },
            { 'd', '\\' },
            { 's', '/' },
            { 'q', '?' },
            { 't', '"' },
            { 'l', '<' },
            { 'r', '>' },
            { '^', '^' },
        };

        /// <summary>
        /// Converts a LFS encoded string to unicode.
        /// </summary>
        /// <param name="buffer">The buffer containing the packet data.</param>
        /// <param name="index">The index that the string starts in the packet data.</param>
        /// <param name="length">The length of the string.</param>
        /// <returns>The resulting unicode string.</returns>
        public static string GetString(byte[] buffer, int index, int length) {
            StringBuilder output = new StringBuilder(length);
            Encoding encoding = DefaultEncoding, nextEncoding;
            int i = 0, lastEncode = index;
            char escape, control, next;

            for (i = index; i < index + length; i++) {
                control = (char)buffer[i];

                // Check for null terminator.
                if (control == Char.MinValue) {
                    break;
                }

                // If not control character then ignore.
                if (control != ControlChar) {
                    continue;
                }

                next = (char)buffer[i + 1];
                if (EncodingMap.TryGetValue(next, out nextEncoding)) {
                    // We're switching encoding to encode everything up to this point.
                    output.Append(encoding.GetString(buffer, lastEncode, (i - lastEncode)));
                    lastEncode = i;
                    encoding = nextEncoding; // Switch encoding.
                }
                else if (EscapeMap.TryGetValue(next, out escape)) {
                    output.Append(escape); // Escape character.
                }
            }

            // Encoding anything that's left.
            if (i - lastEncode > 0) {
                output.Append(encoding.GetString(buffer, lastEncode, (i - lastEncode)));
            }

            return output.ToString();
        }

        /// <summary>
        /// Converts a unicode string into a LFS encoded string.
        /// </summary>
        /// <param name="value">The unicode string to convert.</param>
        /// <param name="buffer">The packet buffer into which the bytes will be written.</param>
        /// <param name="index">The index in the packet buffer to start writing bytes.</param>
        /// <param name="length">The maximum number of bytes to write.</param>
        /// <returns>The number of bytes written during the operation.</returns>
        public static int GetBytes(string value, byte[] buffer, int index, int length) {
            Encoding encoding = DefaultEncoding, tempEncoding;
            byte[] tempBytes = new byte[2];
            int tempCount, start = index, totalLength = index + (length - 1);
            bool found;

            for (int i = 0; i < value.Length && index < totalLength; i++) {
                // Figure out which codepage to try first.
                if (value[i] == '^' && i + 1 < value.Length) {
                    if (EncodingMap.TryGetValue(value[i + 1], out tempEncoding)) {
                        encoding = tempEncoding;
                    }
                }

                if (value[i] <= 127) {
                    // All codepages share ASCII values.
                    buffer[index++] = (byte)value[i];
                }
                else if (TryGetBytes(encoding, value[i], tempBytes, out tempCount)) {
                    // Character exists in current codepage.
                    Buffer.BlockCopy(tempBytes, 0, buffer, index, tempCount);
                    index += tempCount;
                }
                else {
                    // Search for new codepage.
                    found = false;
                    foreach (KeyValuePair<char, Encoding> map in EncodingMap) {
                        if (map.Value == encoding) {
                            continue; // Skip current as we've already searched it.
                        }

                        if (TryGetBytes(map.Value, value[i], tempBytes, out tempCount)) {
                            // Switch codepage.
                            encoding = map.Value;

                            // Add control characters.
                            buffer[index++] = (byte)ControlChar;
                            buffer[index++] = (byte)map.Key;

                            // Copy to buffer.
                            Buffer.BlockCopy(tempBytes, 0, buffer, index, tempCount);
                            index += tempCount;
                            found = true;

                            break;
                        }
                    }

                    // If not found in any codepage then add fallback character.
                    if (!found) {
                        buffer[index++] = (byte)FallbackChar;
                    }
                }
            }

            return index - start;
        }

        /// <summary>
        /// Tries to convert a unicode character into a LFS encoded one.
        /// </summary>
        /// <param name="encoding">The encoding to attempt to convert the character into.</param>
        /// <param name="value">The character to attempt to encode.</param>
        /// <param name="bytes">The array to write the LFS encoded character into.</param>
        /// <param name="count">The number of bytes that the character was encoded into.</param>
        /// <returns>Returns true if the conversion was successful or false if otherwise.</returns>
        [DebuggerStepThrough]
        private static bool TryGetBytes(Encoding encoding, char value, byte[] bytes, out int count) {
            // We use WideCharToMultiByte on Windows as it's very fast, but that's not 
            // available on Mono so we revert to trying to convert a character and then 
            // catching the exception generated when it fails. This is very slow as the 
            // callstack may potentionally need to be unwound for every character in the 
            // string.
            if (IsRunningOnMono) {
                return TryGetBytesMono(encoding, value, bytes, out count);
            }

            return TryGetBytesDotNet(encoding, value, bytes, out count);
        }

        private static bool TryGetBytesDotNet(Encoding encoding, char value, byte[] bytes, out int count) {
            bool usedDefault = false;
            count = NativeMethods.WideCharToMultiByte(
                (uint)encoding.CodePage,
                NativeMethods.WC_NO_BEST_FIT_CHARS,
                value.ToString(),
                1,
                bytes,
                2,
                IntPtr.Zero,
                out usedDefault);
            return !usedDefault;
        }

        private static bool TryGetBytesMono(Encoding encoding, char value, byte[] bytes, out int count) {
            try {
                count = encoding.GetBytes(value.ToString(), 0, 1, bytes, 0);
                return true;
            }
            catch (EncoderFallbackException) {
                count = 0;
                return false;
            }
        }
    }
}