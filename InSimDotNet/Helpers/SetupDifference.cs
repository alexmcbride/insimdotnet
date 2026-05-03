namespace InSimDotNet.Helpers {
    /// <summary>
    /// Represents a single differing field between two LFS car setups.
    /// </summary>
    public class SetupDifference {
        /// <summary>
        /// The setup field that differs.
        /// </summary>
        public SetupField Field { get; private set; }

        /// <summary>
        /// The formatted value from the reference setup.
        /// </summary>
        public string ExpectedValue { get; private set; }

        /// <summary>
        /// The formatted value from the actual setup.
        /// </summary>
        public string ActualValue { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetupDifference"/> class.
        /// </summary>
        public SetupDifference(SetupField field, string expectedValue, string actualValue) {
            Field = field;
            ExpectedValue = expectedValue;
            ActualValue = actualValue;
        }
    }
}
