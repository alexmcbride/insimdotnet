using System;

namespace InSimDotNet.Helpers {
    /// <summary>
    /// Static class to help with LFS related math operations.
    /// </summary>
    public static class MathHelper {
        /// <summary>
        /// Converts LFS speed value into meters per second.
        /// </summary>
        /// <param name="speed">The speed to convert.</param>
        /// <returns>The speed in meters per second.</returns>
        public static double SpeedToMps(double speed) {
            return speed / 327.68;
        }

        /// <summary>
        /// Converts LFS speed value to miles per hour.
        /// </summary>
        /// <param name="speed">The speed to convert.</param>
        /// <returns>The speed in miles per hour.</returns>
        public static double SpeedToMph(double speed) {
            return speed / 146.486067;
        }

        /// <summary>
        /// Converts LFS speed value to kilometres per hour.
        /// </summary>
        /// <param name="speed">The speed to convert.</param>
        /// <returns>The speed in kilometres per hour.</returns>
        public static double SpeedToKph(double speed) {
            return speed / 91.02;
        }

        /// <summary>
        /// Converts meters per second to miles per hour.
        /// </summary>
        /// <param name="mps">The meters per second to convert.</param>
        /// <returns>The speed in miles per hour.</returns>
        public static double MpsToMph(double mps) {
            return mps * 2.236936;
        }

        /// <summary>
        /// Converts meters per second to kilometres per hour.
        /// </summary>
        /// <param name="mps">The meters per second to convert.</param>
        /// <returns>The speed in kilometres per hour.</returns>
        public static double MpsToKph(double mps) {
            return mps * 3.6;
        }

        /// <summary>
        /// Converts meters to miles.
        /// </summary>
        /// <param name="meters">The meters to convert.</param>
        /// <returns>The distance in meters.</returns>
        public static double MetersToMiles(double meters) {
            return meters / 1609.344;
        }

        /// <summary>
        /// Converts meters to kilometres.
        /// </summary>
        /// <param name="meters">The meters to convert.</param>
        /// <returns>The distance in kilometres.</returns>
        public static double MetersToKilometers(double meters) {
            return meters / 1000.0;
        }

        /// <summary>
        /// Converts LFS world-coordinates to meters.
        /// </summary>
        /// <param name="length">The coordinates to convert.</param>
        /// <returns>The length in meters.</returns>
        public static double LengthToMeters(double length) {
            return length / 65536.0;
        }

        /// <summary>
        /// Converts LFS world-coordinates to miles.
        /// </summary>
        /// <param name="length">The coordinates to convert.</param>
        /// <returns>The length in miles.</returns>
        public static double LengthToMiles(double length) {
            return MetersToMiles(LengthToMeters(length));
        }

        /// <summary>
        /// Converts LFS world-coordinates to kilometres.
        /// </summary>
        /// <param name="length">The coordinates to convert.</param>
        /// <returns>The length in kilometres.</returns>
        public static double LengthToKilometers(double length) {
            return MetersToKilometers(LengthToMeters(length));
        }

        /// <summary>
        /// Converts radians to degrees.
        /// </summary>
        /// <param name="radians">The radians to convert.</param>
        /// <returns>The resulting degrees.</returns>
        public static double RadiansToDegrees(double radians) {
            //return radians * (180.0 / Math.PI);
            return radians * 57.295773;
        }

        /// <summary>
        /// Converts degrees to radians.
        /// </summary>
        /// <param name="degrees">The degrees to convert.</param>
        /// <returns>The resulting radians.</returns>
        public static double DegreesToRadians(double degrees) {
            return degrees * 0.01745329;
        }

        /// <summary>
        /// Converts radians to rotations per minute.
        /// </summary>
        /// <param name="radians">The radians to convert.</param>
        /// <returns>The resulting rotations per minute.</returns>
        public static double RadiansToRpm(double radians) {
            return radians * 9.549295;
        }

        /// <summary>
        /// Determines the absolute distance between two points in 3D space.
        /// </summary>
        /// <param name="aX">The X coordinate of the first point.</param>
        /// <param name="aY">The Y coordinate of the first point.</param>
        /// <param name="aZ">The Z coordinate of the first point.</param>
        /// <param name="bX">The X coordinate of the second point.</param>
        /// <param name="bY">The Y coordinate of the second point.</param>
        /// <param name="bZ">The Z coordinate of the second point.</param>
        /// <returns>The resulting distance.</returns>
        public static double Distance(double aX, double aY, double aZ, double bX, double bY, double bZ) {
            return Math.Sqrt((bX - aX) * (bX - aX) + (bY - aY) * (bY - aY) + (bZ - aZ) * (bZ - aZ));
        }

        /// <summary>
        /// Determines the absolute distance between two points in 2D space.
        /// </summary>
        /// <param name="aX">The X coordinate of the first point.</param>
        /// <param name="aY">The Y coordinate of the first point.</param>
        /// <param name="bX">The X coordinate of the second point.</param>
        /// <param name="bY">The Y coordinate of the second point.</param>
        /// <returns>The resulting distance.</returns>
        public static double Distance(double aX, double aY, double bX, double bY) {
            return Math.Sqrt((bX - aX) * (bX - aX) + (bY - aY) * (bY - aY));
        }

        /// <summary>
        /// Determines the absolute distance between two points.
        /// </summary>
        /// <param name="point1">The first point.</param>
        /// <param name="point2">The second point.</param>
        /// <returns>The resulting distance.</returns>
        public static double Distance(double point1, double point2) {
            return Math.Abs((float)(point1 - point2));
        }

        /// <summary>
        /// Determines if two rectangles are intersecting.
        /// </summary>
        /// <param name="aX">The X coordinate of the first rectangle.</param>
        /// <param name="aY">The Y coordinate of the first rectangle.</param>
        /// <param name="aWidth">The width of the first rectangle.</param>
        /// <param name="aHeight">The height of the first rectangle.</param>
        /// <param name="bX">The X coordinate of the second rectangle.</param>
        /// <param name="bY">The Y coordinate of the second rectangle.</param>
        /// <param name="bWidth">The width of the second rectangle.</param>
        /// <param name="bHeight">The height of the second rectangle.</param>
        /// <returns>True if the rectangles are intersecting.</returns>
        public static bool Intersects(int aX, int aY, int aWidth, int aHeight, int bX, int bY, int bWidth, int bHeight) {
            return ((aX <= bX) && ((bX + bWidth) <= (aX + aWidth)) && (aY <= bY)) && ((bY + bHeight) <= (aY + aHeight));
        }

        /// <summary>
        /// Determines if a rectangle contains a point.
        /// </summary>
        /// <param name="rectX">The X coordinates of the rectangle.</param>
        /// <param name="rectY">The Y coordinates of the rectangle.</param>
        /// <param name="rectWidth">The width of the rectangle.</param>
        /// <param name="rectHeight">The height of the rectangle.</param>
        /// <param name="x">The X coordinates of the point to check.</param>
        /// <param name="y">The Y coordinates of the point to check.</param>
        /// <returns>True if the rectangle contains the point.</returns>
        public static bool Contains(int rectX, int rectY, int rectWidth, int rectHeight, int x, int y) {
            return (((rectX <= x) && (x < (rectX + rectWidth))) && (rectY <= y)) && (y < (rectY + rectHeight));
        }
    }
}
