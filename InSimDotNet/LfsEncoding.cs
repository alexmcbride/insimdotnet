using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace InSimDotNet {
    internal static class LfsEncoding {
        private const char ControlChar = '^';
        private const char FallbackChar = '?';
        private static readonly Encoding DefaultEncoding = Encoding.GetEncoding(1252);

        private static readonly Dictionary<char, Encoding> EncodingMap = new Dictionary<char, Encoding> {
            { 'L', Encoding.GetEncoding(1252) },
            { 'G', Encoding.GetEncoding(1253) },
            { 'C', Encoding.GetEncoding(1251) },
            { 'J', Encoding.GetEncoding(932) },
            { 'E', Encoding.GetEncoding(1250) },
            { 'T', Encoding.GetEncoding(1254) },
            { 'B', Encoding.GetEncoding(1257) },
            { 'H', Encoding.GetEncoding(950) },
            { 'S', Encoding.GetEncoding(936) },
            { 'K', Encoding.GetEncoding(949) },
        };

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
                if (control == Char.MinValue) break;
                if (control != ControlChar) continue;

                if (i - start > 0) {
                    output.Append(encoding.GetString(buffer, start, (i - start)));
                }
                start = (i + 2); // skip control chars.

                char next = (char)buffer[++i];
                if (EncodingMap.TryGetValue(next, out nextEncoding)) {
                    encoding = nextEncoding;
                }
                else if (EscapeMap.TryGetValue(next, out escape)) {
                    output.Append(escape);
                }
                else {
                    output.Append(control);
                    output.Append(next);
                }
            }

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
                    result++;
                }
                else if (TryGetByteCount(encoding, value[i], out tempCount)) {
                    result += tempCount;
                }
                else {
                    bool found = false;
                    foreach (KeyValuePair<char, Encoding> map in EncodingMap) {
                        if (map.Value == encoding) continue;

                        if (TryGetByteCount(map.Value, value[i], out tempCount)) {
                            encoding = map.Value;
                            result += tempCount + 2;
                            found = true;
                            break;
                        }
                    }
                    if (!found) result++;
                }
            }

            return result;
        }

        [DebuggerStepThrough]
        private static bool TryGetByteCount(Encoding encoding, char value, out int count) {
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

        public static int GetBytes(string value, byte[] buffer, int index, int length) {
            Encoding encoding = DefaultEncoding;
            byte[] tempBytes = new byte[2];
            int tempCount;
            int start = index;
            int totalLength = index + (length - 1);

            for (int i = 0; i < value.Length && index < totalLength; i++) {
                if (value[i] <= 127) {
                    buffer[index++] = (byte)value[i];
                }
                else if (TryGetBytes(encoding, value[i], tempBytes, out tempCount)) {
                    Buffer.BlockCopy(tempBytes, 0, buffer, index, tempCount);
                    index += tempCount;
                }
                else {
                    bool found = false;
                    foreach (KeyValuePair<char, Encoding> map in EncodingMap) {
                        if (map.Value == encoding) continue;

                        if (TryGetBytes(map.Value, value[i], tempBytes, out tempCount)) {
                            encoding = map.Value;
                            buffer[index++] = (byte)ControlChar;
                            buffer[index++] = (byte)map.Key;
                            Buffer.BlockCopy(tempBytes, 0, buffer, index, tempCount);
                            index += tempCount;
                            found = true;
                            break;
                        }
                    }

                    if (!found) {
                        buffer[index++] = (byte)FallbackChar;
                    }
                }
            }

            return index - start;
        }

        [DebuggerStepThrough]
        private static bool TryGetBytes(Encoding encoding, char value, byte[] bytes, out int count) {
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
    }
}
