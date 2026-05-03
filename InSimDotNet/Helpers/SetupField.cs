namespace InSimDotNet.Helpers {
    /// <summary>
    /// Identifies a specific field in an LFS car setup.
    /// Used in <see cref="SetupDifference"/> so callers can selectively ignore differences.
    /// </summary>
    public enum SetupField {
        // Restrictions
        ExtraWeight,
        IntakeRestriction,
        ExtraWeightDistribution,

        // Aerodynamics
        RearWing,
        FrontWing,

        // Brakes
        BrakePower,
        BrakeDistribution,
        Handbrake,

        // Steering
        MaxSteeringAngle,
        SteeringParallel,
        FrontToe,
        RearToe,
        Caster,

        // Gearbox
        FinalDriveRatio,
        Gear1,
        Gear2,
        Gear3,
        Gear4,
        Gear5,
        Gear6,
        Gear7,

        // Front Differential
        FrontDifferentialType,
        FrontDifferentialPreload,
        FrontDifferentialPowerLock,
        FrontDifferentialCoastLock,
        FrontDifferentialViscosity,

        // Rear Differential
        RearDifferentialType,
        RearDifferentialPreload,
        RearDifferentialPowerLock,
        RearDifferentialCoastLock,
        RearDifferentialViscosity,

        // Center Differential
        CenterDifferentialType,
        CenterDifferentialViscosity,
        CenterDifferentialPowerSplit,

        // Traction Control
        TractionControlActive,
        TractionControlSlipAllowed,

        // Anti-lock Brakes
        AntiLockBrakesActive,

        // Front Suspension
        FrontSuspensionHeight,
        FrontSuspensionStiffness,
        FrontSuspensionBumpDamper,
        FrontSuspensionReboundDamper,
        FrontSuspensionAntiRollBar,

        // Rear Suspension
        RearSuspensionHeight,
        RearSuspensionStiffness,
        RearSuspensionBumpDamper,
        RearSuspensionReboundDamper,
        RearSuspensionAntiRollBar,

        // Wheels
        WheelManufacturer,
        FrontTyreCompound,
        RearTyreCompound,
        FrontTyreSize,
        RearTyreSize,

        // Tyre Pressures
        FrontLeftTyrePressure,
        FrontRightTyrePressure,
        RearLeftTyrePressure,
        RearRightTyrePressure,

        // Camber
        FrontLeftCamber,
        FrontRightCamber,
        RearLeftCamber,
        RearRightCamber,

        // Passengers
        Passenger,

        // Car Configuration
        CarConfig,
    }
}
