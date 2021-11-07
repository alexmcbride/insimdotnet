namespace InSimDotNet {
    /// <summary>
    /// Represents a vector in three-dimensional space.
    /// </summary>
    public struct Vec {
        /// <summary>
        /// Gets the X coordinate.
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Gets the Y coordinate.
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// Gets the Z coordinate.
        /// </summary>
        public int Z { get; private set; }

        /// <summary>
        /// Creates a new Vector type.
        /// </summary>
        /// <param name="x">The X coordinate</param>
        /// <param name="y">The Y coordinate</param>
        /// <param name="z">The Z coordinate</param>
        public Vec(int x, int y, int z)
            : this() {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Returns if two vectors are equal.
        /// </summary>
        /// <param name="other">The vector to check for equality.</param>
        /// <returns>True if the vectors are equal.</returns>
        public bool Equals(Vec other) {
            return this == other;
        }

        /// <summary>
        /// Returns if two objects equal.
        /// </summary>
        /// <param name="obj">The object to check for equality.</param>
        /// <returns>True if the objects are equal.</returns>
        public override bool Equals(object obj) {
            if (obj is Vec) {
                return Equals((Vec)obj);
            }
            return false;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            return base.GetHashCode();
        }

        /// <summary>
        /// Returns if two vectors are equal.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>True of the vectors are equal.</returns>
        public static bool operator ==(Vec a, Vec b) {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }

        /// <summary>
        /// Returns if two vectors are not-equal.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>True of the vectors are not-equal.</returns>
        public static bool operator !=(Vec a, Vec b) {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z;
        }
    }
}
