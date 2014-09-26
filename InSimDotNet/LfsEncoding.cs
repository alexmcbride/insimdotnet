using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace InSimDotNet {
    internal static class LfsEncoding {
        private const char ControlChar = '^';
        private const char FallbackChar = '?';
        private static readonly bool IsRunningOnMono = (Type.GetType("Mono.Runtime") != null);

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
        private static readonly Encoding SubstitutionHackEncoding = EncodingMap['S'];

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

        [DebuggerStepThrough]
        // On Windows we use WideCharToMultiByte as it's fast, on Mono we revent to the slower method of throwing exceptions.
        private static bool TryGetByteCount(Encoding encoding, char value, out int count) {
            if (value >= 0xFF01 && value <= 0xFF5E && encoding != SubstitutionHackEncoding) {
                count = 0;
                return false;
            }

            if (IsRunningOnMono) {
                return TryGetByteCountMono(encoding, value, out count);
            }

            return TryGetByteCountWindows(encoding, value, out count);
        }

        private static bool TryGetByteCountWindows(Encoding encoding, char value, out int count) {
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

        public static int GetBytes(string value, byte[] buffer, int index, int length) {
            Encoding encoding = DefaultEncoding;
            byte[] tempBytes = new byte[2];
            int tempCount;
            int start = index;
            int totalLength = index + (length - 1);

            for (int i = 0; i < value.Length && index < totalLength; i++) {
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

                    // If not found in any codepage then add fallout character.
                    if (!found) {
                        buffer[index++] = (byte)FallbackChar;
                    }
                }
            }

            return index - start;
        }

        //[DebuggerStepThrough]
        // On Windows we use WideCharToMultiByte as it's fast, on Mono we revent to the slower method of throwing exceptions.
        private static bool TryGetBytes(Encoding encoding, char value, byte[] bytes, out int count) {
            if (value >= 0xFF01 && value <= 0xFF5E && encoding != SubstitutionHackEncoding) {
                count = 0;
                return false;
            }

            if (IsRunningOnMono) {
                return TryGetBytesMono(encoding, value, bytes, out count);
            }

            return TryGetBytesWindows(encoding, value, bytes, out count);
        }

        private static bool TryGetBytesWindows(Encoding encoding, char value, byte[] bytes, out int count) {
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