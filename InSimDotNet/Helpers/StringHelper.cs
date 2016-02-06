using System;
using System.Net;
using System.Text;

namespace InSimDotNet.Helpers {
    /// <summary>
    /// Static class to help with LFS related string operations.
    /// </summary>
    public static class StringHelper {
        /// <summary>
        /// Strips color codes from a string.
        /// </summary>
        /// <param name="value">The string to strip.</param>
        /// <returns>The resulting string, sans colors.</returns>
        public static string StripColors(string value) {
            var sb = new StringBuilder(value.Length);

            for (int i = 0; i < value.Length; i++) {
                if (value[i] == '^' && i + 1 < value.Length) {
                    switch (value[i + 1]) {
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                            i++;
                            continue;
                    }
                }

                sb.Append(value[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Strips language codes from a string.
        /// </summary>
        /// <param name="value">The string to strip.</param>
        /// <returns>The resulting string, sans language.</returns>
        public static string StripLanguage(string value) {
            var sb = new StringBuilder(value.Length);

            for (int i = 0; i < value.Length; i++) {
                if (value[i] == '^' && i + 1 < value.Length) {
                    switch (value[i + 1]) {
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
                            i++;
                            continue;
                    }
                }

                sb.Append(value[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Strings colour (^1, ^2 etc..) and language tags (^L, ^J etc..) from a string.
        /// </summary>
        /// <param name="value">The string to strip.</param>
        /// <returns>The resulting string.</returns>
        public static string Strip(string value) {
            var sb = new StringBuilder(value.Length);

            for (int i = 0; i < value.Length; i++) {
                if (value[i] == '^' && i + 1 < value.Length) {
                    switch (value[i + 1]) {
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
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                            i++;
                            continue;
                        case 'v':
                            sb.Append('|');
                            i++;
                            continue;
                        case 'a':
                            sb.Append('*');
                            i++;
                            continue;
                        case 'c':
                            sb.Append(':');
                            i++;
                            continue;
                        case 'd':
                            sb.Append('\\');
                            i++;
                            continue;
                        case 's':
                            sb.Append('/');
                            i++;
                            continue;
                        case 'q':
                            sb.Append('?');
                            i++;
                            continue;
                        case 't':
                            sb.Append('"');
                            i++;
                            continue;
                        case 'l':
                            sb.Append('<');
                            i++;
                            continue;
                        case 'r':
                            sb.Append('>');
                            i++;
                            continue;
                        case '^':
                            sb.Append('^');
                            i++;
                            continue;
                    }
                }

                sb.Append(value[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Unescapes a LFS string (e.g. converts ^v to | etc..)
        /// </summary>
        /// <param name="value">The string to unescape.</param>
        /// <returns>The unescaped string.</returns>
        public static string Unescape(string value) {
            var sb = new StringBuilder(value.Length);

            for (int i = 0; i < value.Length; i++) {
                if (value[i] == '^' && i + 1 < value.Length) {
                    switch (value[i + 1]) {
                        case 'v':
                            sb.Append('|');
                            i++;
                            continue;
                        case 'a':
                            sb.Append('*');
                            i++;
                            continue;
                        case 'c':
                            sb.Append(':');
                            i++;
                            continue;
                        case 'd':
                            sb.Append('\\');
                            i++;
                            continue;
                        case 's':
                            sb.Append('/');
                            i++;
                            continue;
                        case 'q':
                            sb.Append('?');
                            i++;
                            continue;
                        case 't':
                            sb.Append('"');
                            i++;
                            continue;
                        case 'l':
                            sb.Append('<');
                            i++;
                            continue;
                        case 'r':
                            sb.Append('>');
                            i++;
                            continue;
                        case '^':
                            sb.Append('^');
                            i++;
                            continue;
                    }

                }

                sb.Append(value[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Escapes a LFS string (e.g. converts | to ^v and ? to ^q etc..).
        /// </summary>
        /// <param name="value">The string to escape.</param>
        /// <returns>The escaped string.</returns>
        public static string Escape(string value) {
            var sb = new StringBuilder(value.Length);

            for (int i = 0; i < value.Length; i++) {
                switch (value[i]) {
                    case '|':
                        sb.Append("^v");
                        break;
                    case '*':
                        sb.Append("^a");
                        break;
                    case ':':
                        sb.Append("^c");
                        break;
                    case '\\':
                        sb.Append("^d");
                        break;
                    case '/':
                        sb.Append("^s");
                        break;
                    case '?':
                        sb.Append("^q");
                        break;
                    case '"':
                        sb.Append("^t");
                        break;
                    case '<':
                        sb.Append("^l");
                        break;
                    case '>':
                        sb.Append("^r");
                        break;
                    case '^':
                        sb.Append("^^");
                        break;
                    default:
                        sb.Append(value[i]);
                        break;
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts time into a formatted LFS lap time string.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The formatted time string.</returns>
        public static string ToLapTimeString(this TimeSpan value) {
            return value.ToLapTimeString(false);
        }

        /// <summary>
        /// Converts time into a formatted LFS lap time string.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="hours">Set true to force the hours component of the time.</param>
        /// <returns>The formatted time string.</returns>
        public static string ToLapTimeString(this TimeSpan value, bool hours) {
            if (value.Hours > 0 || hours) {
                return String.Format(
                    "{0}:{1:00}:{2:00}.{3:000}",
                    value.Hours,
                    value.Minutes,
                    value.Seconds,
                    value.Milliseconds);
            }
            return String.Format(
                "{0}:{1:00}.{2:000}",
                value.Minutes,
                value.Seconds,
                value.Milliseconds);
        }

        /// <summary>
        /// Converts a TimeSpan to a formatted time string.
        /// </summary>
        /// <param name="value">The TimeSpan to convert.</param>
        /// <returns>The formatted time string.</returns>
        public static string ToTimeString(TimeSpan value) {
            return value.ToLapTimeString();
        }

        /// <summary>
        /// Converts a TimeSpan to a formatted time string.
        /// </summary>
        /// <param name="value">The TimeSpan to convert.</param>
        /// <param name="hours">Set true to force the hours component of the time.</param>
        /// <returns>The formatted time string.</returns>
        public static string ToTimeString(TimeSpan value, bool hours) {
            return value.ToLapTimeString(hours);
        }

        /// <summary>
        /// Determines if a string contains a valid IP address.
        /// </summary>
        /// <param name="value">The IP string to check.</param>
        /// <returns>True if the host is valid.</returns>
        public static bool ValidHost(string value) {
            IPAddress temp;
            return IPAddress.TryParse(value, out temp);
        }

        /// <summary>
        /// Determines if a string contains a valid port number.
        /// </summary>
        /// <param name="value">The port string to check.</param>
        /// <returns>True if the port is valid.</returns>
        public static bool ValidPort(string value) {
            ushort temp;
            return UInt16.TryParse(value, out temp);
        }
    }
}
