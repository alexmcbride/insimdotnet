using System;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;

namespace InSimDotNet.Helpers {
    /// <summary>
    /// Static class to help with LFS related string operations.
    /// </summary>
    public static class StringHelper {
        private static readonly Regex ColorsRegex = new Regex(@"\^[0-9]");

        /// <summary>
        /// Strips color codes from a string.
        /// </summary>
        /// <param name="value">The string to strip.</param>
        /// <returns>The resulting string, sans colors.</returns>
        public static string StripColors(string value) {
            return ColorsRegex.Replace(value, String.Empty);
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
                    CultureInfo.CurrentCulture,
                    "{0}:{1:00}:{2:00}.{3:000}",
                    value.Hours, 
                    value.Minutes, 
                    value.Seconds, 
                    value.Milliseconds);
            }
            return String.Format(
                CultureInfo.CurrentCulture,
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
