using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace InSimDotNet.Helpers
{
    /// <summary>
    /// Static class to help with LFS related string operations.
    /// </summary>
    public static class StringHelper {
        private static readonly char[] ColorCodes = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private static readonly char[] LanguageCodes = new char[] { 'L', 'G', 'C', 'J', 'E', 'T', 'B', 'H', 'S', 'K' };

        private static readonly Dictionary<char, char> UnescapeMap = new Dictionary<char, char> {
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

        private static readonly Dictionary<char, char> EscapeMap = new Dictionary<char, char> {
            { '|', 'v' },
            { '*', 'a' },
            { ':', 'c' },
            { '\\', 'd' },
            { '/', 's' },
            { '?', 'q' },
            { '"', 't' },
            { '<', 'l' },
            { '>', 'r' },
            { '^', '^' },
        };

        /// <summary>
        /// Strips color codes from a string.
        /// </summary>
        /// <param name="value">The string to strip.</param>
        /// <returns>The resulting string, sans colors.</returns>
        public static string StripColors(string value) {
            if (string.IsNullOrEmpty(value)) {
                return value;
            }

            var sb = new StringBuilder(value.Length);

            for (int i = 0; i < value.Length; i++) {
                if (value[i] == '^' && i + 1 < value.Length) {
                    if (ColorCodes.Contains(value[i + 1])) {
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
            if (String.IsNullOrEmpty(value)) {
                throw new ArgumentNullException("value");
            }

            var sb = new StringBuilder(value.Length);

            for (int i = 0; i < value.Length; i++) {
                if (value[i] == '^' && i + 1 < value.Length) {
                    if (LanguageCodes.Contains(value[i + 1])) {
                        i++;
                        continue;
                    }
                }

                sb.Append(value[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// strips colour (^1, ^2 etc..) and language tags (^L, ^J etc..) from a string.
        /// </summary>
        /// <param name="value">The string to strip.</param>
        /// <returns>The resulting string.</returns>
        public static string Strip(string value) {
            if (String.IsNullOrEmpty(value)) {
                throw new ArgumentNullException("value");
            }

            var sb = new StringBuilder(value.Length);

            for (int i = 0; i < value.Length; i++) {
                if (value[i] == '^' && i + 1 < value.Length) {
                    char next = value[i + 1];

                    if (ColorCodes.Contains(next) || LanguageCodes.Contains(next)) {
                        i++;
                        continue;
                    }

                    char c;
                    if (UnescapeMap.TryGetValue(next, out c)) {
                        sb.Append(c);
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
            if (String.IsNullOrEmpty(value)) {
                throw new ArgumentNullException("value");
            }

            var sb = new StringBuilder(value.Length);

            for (int i = 0; i < value.Length; i++) {
                if (value[i] == '^' && i + 1 < value.Length) {
                    char c;
                    if (UnescapeMap.TryGetValue(value[i + 1], out c)) {
                        sb.Append(c);
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
            if (String.IsNullOrEmpty(value)) {
                throw new ArgumentNullException("value");
            }

            var sb = new StringBuilder(value.Length);

            for (int i = 0; i < value.Length; i++) {
                char c;
                if (EscapeMap.TryGetValue(value[i], out c)) {
                    sb.AppendFormat("^{0}", c);
                }
                else {
                    sb.Append(value[i]);
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
