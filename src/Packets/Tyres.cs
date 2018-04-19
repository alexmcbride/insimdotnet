
namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents car tyres.
    /// </summary>
    public struct Tyres {
        /// <summary>
        /// Gets the front-left tyre.
        /// </summary>
        public TyreCompound FrontLeft { get; private set; }

        /// <summary>
        /// Gets the front-right tyre.
        /// </summary>
        public TyreCompound FrontRight { get; private set; }

        /// <summary>
        /// Gets the rear-left tyre.
        /// </summary>
        public TyreCompound RearLeft { get; private set; }

        /// <summary>
        /// Gets the rear-right tyre.
        /// </summary>
        public TyreCompound RearRight { get; private set; }

        /// <summary>
        /// Creates a new Tyres object.
        /// </summary>
        /// <param name="frontLeft">The front-left tyre</param>
        /// <param name="frontRight">The front-right tyre</param>
        /// <param name="rearLeft">The rear-left tyre</param>
        /// <param name="rearRight">The rear-right tyre</param>
        public Tyres(TyreCompound rearLeft, TyreCompound rearRight, TyreCompound frontLeft, TyreCompound frontRight)
            : this() {
            FrontLeft = frontLeft;
            FrontRight = frontRight;
            RearLeft = rearLeft;
            RearRight = rearRight;
        }

        /// <summary>
        /// Returns if two tyres are equal.
        /// </summary>
        /// <param name="other">The tyre to check for equality.</param>
        /// <returns>True if the tyres are equal.</returns>
        public bool Equals(Tyres other) {
            return this == other;
        }

        /// <summary>
        /// Returns if two objects equal.
        /// </summary>
        /// <param name="obj">The object to check for equality.</param>
        /// <returns>True if the objects are equal.</returns>
        public override bool Equals(object obj) {
            if (obj is Tyres) {
                return Equals((Tyres)obj);
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
        /// Returns if two tyres are equal.
        /// </summary>
        /// <param name="a">The first tyre.</param>
        /// <param name="b">The second tyre.</param>
        /// <returns>True of the tyres are equal.</returns>
        public static bool operator ==(Tyres a, Tyres b) {
            return a.FrontLeft == b.FrontLeft && 
                a.FrontRight == b.FrontRight && 
                a.RearLeft == b.RearLeft &&
                a.RearRight == b.RearRight;
        }

        /// <summary>
        /// Returns if two vectors are not-equal.
        /// </summary>
        /// <param name="a">The first tyre.</param>
        /// <param name="b">The second tyre.</param>
        /// <returns>True of the tyres are not-equal.</returns>
        public static bool operator !=(Tyres a, Tyres b) {
            return !(a == b);
        }
    }
}
