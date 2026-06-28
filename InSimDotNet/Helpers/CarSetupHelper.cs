using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using InSimDotNet.Packets;

namespace InSimDotNet.Helpers {
    /// <summary>
    /// Static helper for comparing LFS car setups.
    /// </summary>
    public static class CarSetupHelper {

        private const int SetFileMinSize = 132;
        private const int SetFileHeaderSize = 12;

        // Absolute offsets within a full .set file
        private const int OffsetInfoFlags = 12;
        private const int OffsetExtraWeightDist = 14;
        private const int OffsetWheelManufacturer = 15;
        private const int OffsetBrakePower = 16;
        private const int OffsetRearWing = 20;
        private const int OffsetFrontWing = 21;
        private const int OffsetExtraWeight = 22;
        private const int OffsetIntakeRestriction = 23;
        private const int OffsetMaxSteeringLock = 24;
        private const int OffsetParallelSteering = 25;
        private const int OffsetBrakeBalance = 26;
        private const int OffsetEngineBrakeReduction = 27;
        private const int OffsetCenterDiffType = 28;
        private const int OffsetCenterDiffViscosity = 29;
        private const int OffsetCenterDiffPowerSplit = 31;
        private const int OffsetGear7 = 32;
        private const int OffsetPassengers = 48;
        private const int OffsetCarConfig = 49;
        private const int OffsetTCSlip = 50;
        private const int OffsetTCMinSpeed = 51;
        private const int OffsetRearRideHeight = 52;
        private const int OffsetRearSpringStiffness = 56;
        private const int OffsetRearBumpDamping = 60;
        private const int OffsetRearReboundDamping = 64;
        private const int OffsetRearAntiRollBar = 68;
        private const int OffsetHandbrake = 72;
        private const int OffsetRearToe = 76;
        private const int OffsetRearTyreType = 78;
        private const int OffsetRearLeftCamber = 80;
        private const int OffsetRearRightCamber = 81;
        private const int OffsetRearTyreSize = 82;
        private const int OffsetRearDiffPreload = 83;
        private const int OffsetRearDiffType = 84;
        private const int OffsetRearDiffViscosity = 85;
        private const int OffsetRearDiffPowerLock = 86;
        private const int OffsetRearDiffCoastLock = 87;
        private const int OffsetRearLeftPressure = 88;
        private const int OffsetRearRightPressure = 90;
        private const int OffsetFrontRideHeight = 92;
        private const int OffsetFrontSpringStiffness = 96;
        private const int OffsetFrontBumpDamping = 100;
        private const int OffsetFrontReboundDamping = 104;
        private const int OffsetFrontAntiRollBar = 108;
        private const int OffsetFrontToe = 116;
        private const int OffsetFrontCaster = 117;
        private const int OffsetFrontTyreType = 118;
        private const int OffsetFrontLeftCamber = 120;
        private const int OffsetFrontRightCamber = 121;
        private const int OffsetFrontTyreSize = 122;
        private const int OffsetFrontDiffPreload = 123;
        private const int OffsetFrontDiffType = 124;
        private const int OffsetFrontDiffViscosity = 125;
        private const int OffsetFrontDiffPowerLock = 126;
        private const int OffsetFrontDiffCoastLock = 127;
        private const int OffsetFrontLeftPressure = 128;
        private const int OffsetFrontRightPressure = 130;

        public class ParsedCarSetup {
            public string ExtraWeight, IntakeRestriction, ExtraWeightDistribution;
            public string RearWing, FrontWing;
            public string BrakePower, BrakeDistribution, Handbrake, EngineBrakeReduction;
            public string MaxSteeringAngle, SteeringParallel, FrontToe, RearToe, Caster;
            public string FinalDriveRatio, Gear1, Gear2, Gear3, Gear4, Gear5, Gear6, Gear7;
            public DiffType FrontDiffType, RearDiffType;
            public string FrontDiffPreload, FrontDiffPowerLock, FrontDiffCoastLock, FrontDiffViscosity;
            public string RearDiffPreload, RearDiffPowerLock, RearDiffCoastLock, RearDiffViscosity;
            public CenterDiffType CenterDiffType;
            public string CenterDiffViscosity, CenterDiffPowerSplit;
            public string TractionControlActive, TractionControlSlip, TractionControlMinSpeed;
            public string AntiLockBrakesActive;
            public string FrontSuspHeight, FrontSuspStiffness, FrontSuspBump, FrontSuspRebound, FrontSuspARB;
            public string RearSuspHeight, RearSuspStiffness, RearSuspBump, RearSuspRebound, RearSuspARB;
            public WheelManufacturer WheelManufacturer;
            public TyreCompound FrontTyreCompound, RearTyreCompound;
            public string FrontLeftPressure, FrontRightPressure, RearLeftPressure, RearRightPressure;
            public string FrontLeftCamber, FrontRightCamber, RearLeftCamber, RearRightCamber;
            public string Passenger;
            public string CarConfig;
            public string FrontTyreSize, RearTyreSize;
        }

        /// <summary>
        /// Compares a reference .set file against an IS_SET packet and returns all differing fields.
        /// </summary>
        /// <param name="referenceSetFile">Raw bytes of the reference .set file (at least 132 bytes, must start with SRSETT).</param>
        /// <param name="actual">The IS_SET packet received from a player.</param>
        /// <returns>Read-only list of differences. Empty if setups are identical.</returns>
        public static IReadOnlyList<SetupDifference> CompareSetups(byte[] referenceSetFile, IS_SET actual) {
            if (referenceSetFile == null) throw new ArgumentNullException("referenceSetFile");
            if (actual == null) throw new ArgumentNullException("actual");
            if (actual.Setup == null || actual.Setup.Length < 120)
                throw new ArgumentException("IS_SET.Setup must be 120 bytes.", "actual");
            if (referenceSetFile.Length < SetFileMinSize)
                throw new ArgumentException("Reference .set file must be at least 132 bytes.", "referenceSetFile");

            ParsedCarSetup expected = ParseSetup(referenceSetFile, true);
            ParsedCarSetup actualParsed = ParseSetup(actual.Setup, false);
            return BuildDifferences(expected, actualParsed);
        }

        /// <summary>
        /// Compares two parsed setups and returns a list of differences between them.  
        /// </summary>
        /// <param name="expected">The expected setup to compare against. Represents the baseline or reference configuration.</param>
        /// <param name="actualSetup">The actual setup to compare. Represents the configuration to be evaluated for differences.</param>
        /// <returns>A read-only list of differences between the expected and actual setups. The list is empty if no differences
        /// are found.</returns>
        public static IReadOnlyList<SetupDifference> CompareSetups(ParsedCarSetup expected, IS_SET actual) {
            return BuildDifferences(expected, actual.ParsedSetup);
        }
        
        /// <summary>
        /// Compares two parsed setups and returns a list of differences between them.  
        /// </summary>
        /// <param name="expected">The expected setup to compare against. Represents the baseline or reference configuration.</param>
        /// <param name="actualSetup">The actual setup to compare. Represents the configuration to be evaluated for differences.</param>
        /// <returns>A read-only list of differences between the expected and actual setups. The list is empty if no differences
        /// are found.</returns>
        public static IReadOnlyList<SetupDifference> CompareSetups(ParsedCarSetup expected, ParsedCarSetup actualSetup) {
            return BuildDifferences(expected, actualSetup);
        }

        /// <summary>
        /// Compares current setup with the expected configuration and returns a list of differences relevant.
        /// </summary>
        /// <param name="actualSetup">The parsed setup to compare against the expected configuration. Cannot be null.</param>
        /// <param name="expected">The expected parsed setup configuration to use as a baseline for comparison. Cannot be null.</param>
        /// <returns>A read-only list of setup differences that highlight changes affecting lap time. The list is empty if there
        /// are no relevant differences.</returns>
        public static IReadOnlyList<SetupDifference> CompareWithExpected(this ParsedCarSetup actualSetup, ParsedCarSetup expected)
        {
            return BuildDifferences(expected, actualSetup);
        }

        // --- Parser ---

        // Offsets are file-relative (matching the official LFS doc).
        // For IS_SET.Setup[], which starts at file offset +12, subtract SetFileHeaderSize from each offset.
        // shift = 0 for .set file, shift = SetFileHeaderSize for IS_SET.Setup.
        public static ParsedCarSetup ParseSetup(byte[] d, bool isFile) {
            var s = new ParsedCarSetup();
            int shift = isFile ? 0 : SetFileHeaderSize;

            byte flags = d[OffsetInfoFlags - shift];
            s.TractionControlActive = ((flags >> 1) & 1).ToString();
            s.AntiLockBrakesActive = ((flags >> 2) & 1).ToString();

            s.ExtraWeightDistribution = SByte(d, OffsetExtraWeightDist - shift);
            s.WheelManufacturer = (WheelManufacturer)d[OffsetWheelManufacturer - shift];
            s.BrakePower = Float0(d, OffsetBrakePower - shift);
            s.RearWing = SByte(d, OffsetRearWing - shift);
            s.FrontWing = SByte(d, OffsetFrontWing - shift);
            s.ExtraWeight = SByte(d, OffsetExtraWeight - shift);
            s.IntakeRestriction = SByte(d, OffsetIntakeRestriction - shift);
            s.MaxSteeringAngle = SByte(d, OffsetMaxSteeringLock - shift);
            s.SteeringParallel = SByte(d, OffsetParallelSteering - shift);
            s.BrakeDistribution = SByte(d, OffsetBrakeBalance - shift);
            s.EngineBrakeReduction = SByte(d, OffsetEngineBrakeReduction - shift);
            s.Handbrake = HandbrakeValue(
                BitConverter.ToSingle(d, OffsetHandbrake - shift),
                BitConverter.ToSingle(d, OffsetBrakePower - shift));

            s.CenterDiffType = (CenterDiffType)(sbyte)d[OffsetCenterDiffType - shift];
            s.CenterDiffViscosity = SByte(d, OffsetCenterDiffViscosity - shift);
            s.CenterDiffPowerSplit = SByte(d, OffsetCenterDiffPowerSplit - shift);

            // Gear area starts at file offset 32. Order differs between formats:
            //   .set file:   Gear7, FDR, Gear1, Gear2, Gear3, Gear4, Gear5, Gear6
            //   IS_SET.Setup: Gear1, Gear2, Gear3, Gear4, Gear5, Gear6, Gear7, FDR
            int gearBase = OffsetGear7 - shift;
            if (isFile) {
                s.Gear7 = GearRatio(BitConverter.ToUInt16(d, gearBase));
                s.FinalDriveRatio = GearRatio(BitConverter.ToUInt16(d, gearBase + 2));
                s.Gear1 = GearRatio(BitConverter.ToUInt16(d, gearBase + 4));
                s.Gear2 = GearRatio(BitConverter.ToUInt16(d, gearBase + 6));
                s.Gear3 = GearRatio(BitConverter.ToUInt16(d, gearBase + 8));
                s.Gear4 = GearRatio(BitConverter.ToUInt16(d, gearBase + 10));
                s.Gear5 = GearRatio(BitConverter.ToUInt16(d, gearBase + 12));
                s.Gear6 = GearRatio(BitConverter.ToUInt16(d, gearBase + 14));
            } else {
                s.Gear1 = GearRatio(BitConverter.ToUInt16(d, gearBase));
                s.Gear2 = GearRatio(BitConverter.ToUInt16(d, gearBase + 2));
                s.Gear3 = GearRatio(BitConverter.ToUInt16(d, gearBase + 4));
                s.Gear4 = GearRatio(BitConverter.ToUInt16(d, gearBase + 6));
                s.Gear5 = GearRatio(BitConverter.ToUInt16(d, gearBase + 8));
                s.Gear6 = GearRatio(BitConverter.ToUInt16(d, gearBase + 10));
                s.Gear7 = GearRatio(BitConverter.ToUInt16(d, gearBase + 12));
                s.FinalDriveRatio = GearRatio(BitConverter.ToUInt16(d, gearBase + 14));
            }

            s.Passenger = d[OffsetPassengers - shift].ToString();
            s.CarConfig = d[OffsetCarConfig - shift].ToString();
            s.TractionControlSlip = TCSlip((sbyte)d[OffsetTCSlip - shift]);
            s.TractionControlMinSpeed = d[OffsetTCMinSpeed - shift].ToString();

            s.RearSuspHeight = Float3(d, OffsetRearRideHeight - shift);
            s.RearSuspStiffness = FloatDiv1000F1(d, OffsetRearSpringStiffness - shift);
            s.RearSuspBump = FloatDiv1000F1(d, OffsetRearBumpDamping - shift);
            s.RearSuspRebound = FloatDiv1000F1(d, OffsetRearReboundDamping - shift);
            s.RearSuspARB = FloatDiv1000F1(d, OffsetRearAntiRollBar - shift);

            s.RearToe = Toe(d[OffsetRearToe - shift]);
            s.RearTyreCompound = (TyreCompound)d[OffsetRearTyreType - shift];
            s.RearLeftCamber = Camber(d[OffsetRearLeftCamber - shift]);
            s.RearRightCamber = Camber(d[OffsetRearRightCamber - shift]);
            s.RearTyreSize = d[OffsetRearTyreSize - shift].ToString();

            s.RearDiffPreload = ((sbyte)d[OffsetRearDiffPreload - shift] * 10).ToString();
            s.RearDiffType = (DiffType)(sbyte)d[OffsetRearDiffType - shift];
            s.RearDiffViscosity = SByte(d, OffsetRearDiffViscosity - shift);
            s.RearDiffPowerLock = d[OffsetRearDiffPowerLock - shift].ToString();
            s.RearDiffCoastLock = d[OffsetRearDiffCoastLock - shift].ToString();

            s.RearLeftPressure = BitConverter.ToUInt16(d, OffsetRearLeftPressure - shift).ToString();
            s.RearRightPressure = BitConverter.ToUInt16(d, OffsetRearRightPressure - shift).ToString();

            s.FrontSuspHeight = Float3(d, OffsetFrontRideHeight - shift);
            s.FrontSuspStiffness = FloatDiv1000F1(d, OffsetFrontSpringStiffness - shift);
            s.FrontSuspBump = FloatDiv1000F1(d, OffsetFrontBumpDamping - shift);
            s.FrontSuspRebound = FloatDiv1000F1(d, OffsetFrontReboundDamping - shift);
            s.FrontSuspARB = FloatDiv1000F1(d, OffsetFrontAntiRollBar - shift);

            s.FrontToe = Toe(d[OffsetFrontToe - shift]);
            s.Caster = Caster((sbyte)d[OffsetFrontCaster - shift]);
            s.FrontTyreCompound = (TyreCompound)d[OffsetFrontTyreType - shift];
            s.FrontLeftCamber = Camber(d[OffsetFrontLeftCamber - shift]);
            s.FrontRightCamber = Camber(d[OffsetFrontRightCamber - shift]);
            s.FrontTyreSize = d[OffsetFrontTyreSize - shift].ToString();

            s.FrontDiffPreload = ((sbyte)d[OffsetFrontDiffPreload - shift] * 10).ToString();
            s.FrontDiffType = (DiffType)(sbyte)d[OffsetFrontDiffType - shift];
            s.FrontDiffViscosity = SByte(d, OffsetFrontDiffViscosity - shift);
            s.FrontDiffPowerLock = d[OffsetFrontDiffPowerLock - shift].ToString();
            s.FrontDiffCoastLock = d[OffsetFrontDiffCoastLock - shift].ToString();

            s.FrontLeftPressure = BitConverter.ToUInt16(d, OffsetFrontLeftPressure - shift).ToString();
            s.FrontRightPressure = BitConverter.ToUInt16(d, OffsetFrontRightPressure - shift).ToString();

            return s;
        }

        // --- Comparison ---

        private static IReadOnlyList<SetupDifference> BuildDifferences(ParsedCarSetup e, ParsedCarSetup a) {
            var diffs = new List<SetupDifference>();

            // Restrictions
            Diff(diffs, SetupField.ExtraWeight, e.ExtraWeight, a.ExtraWeight);
            Diff(diffs, SetupField.IntakeRestriction, e.IntakeRestriction, a.IntakeRestriction);
            Diff(diffs, SetupField.ExtraWeightDistribution, e.ExtraWeightDistribution, a.ExtraWeightDistribution);

            // Aerodynamics
            Diff(diffs, SetupField.RearWing, e.RearWing, a.RearWing);
            Diff(diffs, SetupField.FrontWing, e.FrontWing, a.FrontWing);

            // Brakes
            Diff(diffs, SetupField.BrakePower, e.BrakePower, a.BrakePower);
            Diff(diffs, SetupField.BrakeDistribution, e.BrakeDistribution, a.BrakeDistribution);
            Diff(diffs, SetupField.EngineBrakeReduction, e.EngineBrakeReduction, a.EngineBrakeReduction);
            Diff(diffs, SetupField.Handbrake, e.Handbrake, a.Handbrake);

            // Steering
            Diff(diffs, SetupField.MaxSteeringAngle, e.MaxSteeringAngle, a.MaxSteeringAngle);
            Diff(diffs, SetupField.SteeringParallel, e.SteeringParallel, a.SteeringParallel);
            Diff(diffs, SetupField.FrontToe, e.FrontToe, a.FrontToe);
            Diff(diffs, SetupField.RearToe, e.RearToe, a.RearToe);
            Diff(diffs, SetupField.Caster, e.Caster, a.Caster);

            // Gearbox
            Diff(diffs, SetupField.FinalDriveRatio, e.FinalDriveRatio, a.FinalDriveRatio);
            Diff(diffs, SetupField.Gear1, e.Gear1, a.Gear1);
            Diff(diffs, SetupField.Gear2, e.Gear2, a.Gear2);
            Diff(diffs, SetupField.Gear3, e.Gear3, a.Gear3);
            Diff(diffs, SetupField.Gear4, e.Gear4, a.Gear4);
            Diff(diffs, SetupField.Gear5, e.Gear5, a.Gear5);
            Diff(diffs, SetupField.Gear6, e.Gear6, a.Gear6);
            Diff(diffs, SetupField.Gear7, e.Gear7, a.Gear7);

            // Differentials — type-aware: if type differs only report type; otherwise compare relevant fields
            DiffFrontRearDiff(diffs,
                SetupField.FrontDifferentialType, e.FrontDiffType, a.FrontDiffType,
                SetupField.FrontDifferentialPreload, e.FrontDiffPreload, a.FrontDiffPreload,
                SetupField.FrontDifferentialPowerLock, e.FrontDiffPowerLock, a.FrontDiffPowerLock,
                SetupField.FrontDifferentialCoastLock, e.FrontDiffCoastLock, a.FrontDiffCoastLock,
                SetupField.FrontDifferentialViscosity, e.FrontDiffViscosity, a.FrontDiffViscosity);

            DiffFrontRearDiff(diffs,
                SetupField.RearDifferentialType, e.RearDiffType, a.RearDiffType,
                SetupField.RearDifferentialPreload, e.RearDiffPreload, a.RearDiffPreload,
                SetupField.RearDifferentialPowerLock, e.RearDiffPowerLock, a.RearDiffPowerLock,
                SetupField.RearDifferentialCoastLock, e.RearDiffCoastLock, a.RearDiffCoastLock,
                SetupField.RearDifferentialViscosity, e.RearDiffViscosity, a.RearDiffViscosity);

            DiffCenterDiff(diffs,
                e.CenterDiffType, a.CenterDiffType,
                e.CenterDiffViscosity, a.CenterDiffViscosity,
                e.CenterDiffPowerSplit, a.CenterDiffPowerSplit);

            // Traction Control
            Diff(diffs, SetupField.TractionControlActive, e.TractionControlActive, a.TractionControlActive);
            Diff(diffs, SetupField.TractionControlSlipAllowed, e.TractionControlSlip, a.TractionControlSlip);
            Diff(diffs, SetupField.TractionControlMinSpeed, e.TractionControlMinSpeed, a.TractionControlMinSpeed);

            // Anti-lock Brakes
            Diff(diffs, SetupField.AntiLockBrakesActive, e.AntiLockBrakesActive, a.AntiLockBrakesActive);

            // Suspension
            Diff(diffs, SetupField.FrontSuspensionStiffness, e.FrontSuspStiffness, a.FrontSuspStiffness);
            Diff(diffs, SetupField.FrontSuspensionBumpDamper, e.FrontSuspBump, a.FrontSuspBump);
            Diff(diffs, SetupField.FrontSuspensionReboundDamper, e.FrontSuspRebound, a.FrontSuspRebound);
            Diff(diffs, SetupField.FrontSuspensionAntiRollBar, e.FrontSuspARB, a.FrontSuspARB);
            Diff(diffs, SetupField.FrontSuspensionHeight, e.FrontSuspHeight, a.FrontSuspHeight);
            Diff(diffs, SetupField.RearSuspensionStiffness, e.RearSuspStiffness, a.RearSuspStiffness);
            Diff(diffs, SetupField.RearSuspensionBumpDamper, e.RearSuspBump, a.RearSuspBump);
            Diff(diffs, SetupField.RearSuspensionReboundDamper, e.RearSuspRebound, a.RearSuspRebound);
            Diff(diffs, SetupField.RearSuspensionAntiRollBar, e.RearSuspARB, a.RearSuspARB);
            Diff(diffs, SetupField.RearSuspensionHeight, e.RearSuspHeight, a.RearSuspHeight);

            // Wheels
            Diff(diffs, SetupField.WheelManufacturer, e.WheelManufacturer, a.WheelManufacturer, v => v.ToDisplayName());
            Diff(diffs, SetupField.FrontTyreCompound, e.FrontTyreCompound, a.FrontTyreCompound, v => v.ToDisplayName());
            Diff(diffs, SetupField.RearTyreCompound, e.RearTyreCompound, a.RearTyreCompound, v => v.ToDisplayName());
            Diff(diffs, SetupField.FrontTyreSize, e.FrontTyreSize, a.FrontTyreSize);
            Diff(diffs, SetupField.RearTyreSize, e.RearTyreSize, a.RearTyreSize);

            // Tyre pressures
            Diff(diffs, SetupField.FrontLeftTyrePressure, e.FrontLeftPressure, a.FrontLeftPressure);
            Diff(diffs, SetupField.FrontRightTyrePressure, e.FrontRightPressure, a.FrontRightPressure);
            Diff(diffs, SetupField.RearLeftTyrePressure, e.RearLeftPressure, a.RearLeftPressure);
            Diff(diffs, SetupField.RearRightTyrePressure, e.RearRightPressure, a.RearRightPressure);

            // Camber
            Diff(diffs, SetupField.FrontLeftCamber, e.FrontLeftCamber, a.FrontLeftCamber);
            Diff(diffs, SetupField.FrontRightCamber, e.FrontRightCamber, a.FrontRightCamber);
            Diff(diffs, SetupField.RearLeftCamber, e.RearLeftCamber, a.RearLeftCamber);
            Diff(diffs, SetupField.RearRightCamber, e.RearRightCamber, a.RearRightCamber);

            // Passengers
            Diff(diffs, SetupField.Passenger, e.Passenger, a.Passenger);

            // Car Configuration
            Diff(diffs, SetupField.CarConfig, e.CarConfig, a.CarConfig);

            return new ReadOnlyCollection<SetupDifference>(diffs);
        }

        private static void Diff(List<SetupDifference> diffs, SetupField field, string expected, string actual) {
            if (expected != actual)
                diffs.Add(new SetupDifference(field, expected, actual));
        }

        private static void Diff<T>(List<SetupDifference> diffs, SetupField field, T expected, T actual, System.Func<T, string> displayName) {
            if (!expected.Equals(actual))
                diffs.Add(new SetupDifference(field, displayName(expected), displayName(actual)));
        }

        private static void DiffFrontRearDiff(
            List<SetupDifference> diffs,
            SetupField typeField, DiffType refType, DiffType actType,
            SetupField preloadField, string refPreload, string actPreload,
            SetupField powerLockField, string refPowerLock, string actPowerLock,
            SetupField coastLockField, string refCoastLock, string actCoastLock,
            SetupField viscosityField, string refViscosity, string actViscosity) {

            if (refType != actType) {
                diffs.Add(new SetupDifference(typeField, refType.ToDisplayName(), actType.ToDisplayName()));
                return;
            }
            if (refType == DiffType.ClutchPack) {
                Diff(diffs, powerLockField, refPowerLock, actPowerLock);
                Diff(diffs, coastLockField, refCoastLock, actCoastLock);
                Diff(diffs, preloadField, refPreload, actPreload);
            } else if (refType == DiffType.Viscous) {
                Diff(diffs, viscosityField, refViscosity, actViscosity);
                Diff(diffs, powerLockField, refPowerLock, actPowerLock);
            }
            // Open / Locked: only type matters, already checked above
        }

        private static void DiffCenterDiff(
            List<SetupDifference> diffs,
            CenterDiffType refType, CenterDiffType actType,
            string refViscosity, string actViscosity,
            string refPowerSplit, string actPowerSplit) {

            if (refType != actType) {
                diffs.Add(new SetupDifference(SetupField.CenterDifferentialType, refType.ToDisplayName(), actType.ToDisplayName()));
                return;
            }
            if (refType == CenterDiffType.Viscous) {
                Diff(diffs, SetupField.CenterDifferentialViscosity, refViscosity, actViscosity);
                Diff(diffs, SetupField.CenterDifferentialPowerSplit, refPowerSplit, actPowerSplit);
            }
        }

        // --- Format helpers ---

        private static string SByte(byte[] d, int offset) {
            return ((sbyte)d[offset]).ToString();
        }

        private static string Float0(byte[] d, int offset) {
            return ((int)Math.Round(BitConverter.ToSingle(d, offset))).ToString();
        }

        private static string Float3(byte[] d, int offset) {
            return BitConverter.ToSingle(d, offset).ToString("F3", CultureInfo.InvariantCulture);
        }

        private static string FloatDiv1000F1(byte[] d, int offset) {
            return ((int)BitConverter.ToSingle(d, offset) / 1000.0).ToString("F1", CultureInfo.InvariantCulture);
        }

        private static string HandbrakeValue(float handbrake, float brakePower) {
            float value = handbrake == 0f ? brakePower : handbrake;
            return ((int)Math.Round(value)).ToString();
        }

        private static string GearRatio(ushort raw) {
            return (0.5 + 7.0 * raw / 65534.0).ToString("F3", CultureInfo.InvariantCulture);
        }

        private static string Toe(byte raw) {
            return ((raw - 9.0) / 10.0).ToString(CultureInfo.InvariantCulture);
        }

        private static string Caster(sbyte raw) {
            return (raw / 10.0).ToString(CultureInfo.InvariantCulture);
        }

        private static string Camber(byte raw) {
            return ((raw / 10.0) - 4.5).ToString(CultureInfo.InvariantCulture);
        }

        private static string TCSlip(sbyte raw) {
            return (raw / 10.0).ToString("F1", CultureInfo.InvariantCulture);
        }

    }
}
