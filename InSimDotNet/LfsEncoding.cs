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
            Encoding encoding = DefaultEncoding;
            Encoding nextEncoding;
            int i = 0, start = index;
            char escape;

            for (i = index; i < index + length; i++) {
                char control = (char)buffer[i];

                // Check for null terminator.
                if (control == Char.MinValue) {
                    break;
                }

                // If not control character then ignore.
                if (control != ControlChar) {
                    continue;
                }

                // Found control character so encode everything up to this point.
                if (i - start > 0) {
                    output.Append(encoding.GetString(buffer, start, (i - start)));
                }
                start = (i + 2); // skip control chars.

                // Process control character.
                char next = (char)buffer[++i];
                if (EncodingMap.TryGetValue(next, out nextEncoding)) {
                    encoding = nextEncoding; // Switch encoding.
                }
                else if (EscapeMap.TryGetValue(next, out escape)) {
                    output.Append(escape); // Escape character.
                }
                else {
                    // Character not codepage switch or escape, so just ignore it.
                    output.Append(control);
                    output.Append(next);
                }
            }

            // End of string reached so encode up all to this point.
            if (i - start > 0) {
                output.Append(encoding.GetString(buffer, start, (i - start)));
            }

            return output.ToString();
        }

        /// <summary>
        /// Gets the number of bytes a string will take up once it has been converted from unicode string into a LFS encoded string.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <param name="maxLength">The maximum number of characters to test.</param>
        /// <returns>The resulting number of bytes.</returns>
        public static int GetByteCount(string value, int maxLength) {
            Encoding encoding = DefaultEncoding;
            int tempCount, result = 0;

            for (int i = 0; i < value.Length && i < maxLength; i++) {
                if (value[i] <= 127) {
                    // All codepages share first 128 values.
                    result++;
                }
                else if (TryGetByteCount(encoding, value[i], out tempCount)) {
                    // Charcter in current codepage.
                    result += tempCount;
                }
                else {
                    // Search for new codepage.
                    bool found = false;

                    foreach (KeyValuePair<char, Encoding> map in EncodingMap) {
                        if (map.Value == encoding) {
                            continue;
                        }

                        if (TryGetByteCount(map.Value, value[i], out tempCount)) {
                            encoding = map.Value;
                            result += tempCount + 2;
                            found = true;
                            break;
                        }
                    }

                    if (!found) {
                        result++;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Tries to figure out the number of bytes a unicode character will take up once it has been converted into a LFS character. Either one or two bytes.
        /// </summary>
        /// <param name="encoding">The encoding to try and convert the character to.</param>
        /// <param name="value">The character to convert.</param>
        /// <param name="count">The numbers of bytes the character will take up.</param>
        /// <returns>Returns true if the character could be successfully converted or false otherwise.</returns>
        [DebuggerStepThrough]
        private static bool TryGetByteCount(Encoding encoding, char value, out int count) {
            // We use WideCharToMultiByte on Windows as it's very fast, but that's not 
            // available on Mono so we revert to trying to convert a character and then 
            // catching the exception generated when it fails. This is very slow as the 
            // callstack may potentionally need to be unwound for every character in the 
            // string.
            if (IsRunningOnMono) {
                return TryGetByteCountMono(encoding, value, out count);
            }

            return TryGetByteCountDotNet(encoding, value, out count);
        }

        private static bool TryGetByteCountDotNet(Encoding encoding, char value, out int count) {
            bool usedDefault = false;
            count = NativeMethods.WideCharToMultiByte(
                (uint)encoding.CodePage,
                NativeMethods.WC_NO_BEST_FIT_CHARS,
                value.ToString(),
                1,
                null,
                0,
                IntPtr.Zero,
                out usedDefault);
            return !usedDefault;
        }

        private static bool TryGetByteCountMono(Encoding encoding, char value, out int count) {
            try {
                count = encoding.GetByteCount(value.ToString());
                return true;
            }
            catch (EncoderFallbackException) {
                count = 0;
                return false;
            }
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
            Encoding encoding = DefaultEncoding;
            byte[] tempBytes = new byte[2];
            int tempCount;
            int start = index;
            int totalLength = index + (length - 1);

            for (int i = 0; i < value.Length && index < totalLength; i++) {
                // Remove any existing language tags from the string.
                int next = i + 1;
                if (value[i] == '^' && next < value.Length) {
                    switch (value[next]) {
                        case 'L':
                        case 'G':
                        case 'C':
                        case 'J':
                        case 'E':
                        case 'T':
                        case 'B':
                        case 'H':
                        case 'S':
                        case 'K':
                            i++; // skip codepage chars
                            continue;
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
                    bool found = false;
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