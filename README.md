# InSim.NET for .NET 6 / .NET 8

This is a fork of [InSim.NET](https://github.com/alexmcbride/insimdotnet) ported to .NET 6 / .NET 8.

- **master** branch — .NET Framework 4.8 (classic)
- **net6** branch — .NET 6 / .NET 8 (this branch)

The [original documentation](https://github.com/alexmcbride/insimdotnet/wiki) still applies for core InSim usage.

---

## Installation

Reference the NuGet package in your project.

If you get runtime errors about missing assemblies, add these to your `.csproj`:

```xml
<PackageReference Include="System.IO.FileSystem" Version="4.3.0" />
<PackageReference Include="System.Text.Encoding.CodePages" Version="4.5.1" />
```

---

## Quick Start

```csharp
InSim insim = new InSim();

insim.Bind<IS_MSO>(MessageOut);

insim.Initialize(new InSimSettings {
    Host = "127.0.0.1",
    Port = 29999,
    Admin = string.Empty,
});

while (insim.IsConnected)
    Thread.Sleep(200);

void MessageOut(InSim insim, IS_MSO packet) {
    Console.WriteLine(packet.Msg);
}
```

---

## Car Setup Validation — `CarSetupHelper`

This fork adds `CarSetupHelper` in the `InSimDotNet.Helpers` namespace for comparing car setups received via `IS_SET` against a reference `.set` file.

### Basic usage

```csharp
// Load the reference .set file bytes
byte[] referenceSetBytes = File.ReadAllBytes("reference.set");

// Bind the IS_SET packet event
insim.Bind<IS_SET>(OnCarSetup);

void OnCarSetup(InSim insim, IS_SET packet) {
    var diffs = CarSetupHelper.CompareSetups(referenceSetBytes, packet);
    if (diffs.Count == 0) {
        Console.WriteLine("Setup matches the reference.");
    } else {
        foreach (var d in diffs)
            Console.WriteLine($"{d.Field}: expected {d.ExpectedValue}, got {d.ActualValue}");
    }
}
```

### `SetupDifference`

Each entry in the returned list has:

| Property | Type | Description |
|---|---|---|
| `Field` | `SetupField` | Enum value identifying the differing field |
| `ExpectedValue` | `string` | Human-readable value from the reference setup |
| `ActualValue` | `string` | Human-readable value from the driver's setup |

### `SetupField` enum

All comparable fields:

```
ExtraWeight, IntakeRestriction, ExtraWeightDistribution
RearWing, FrontWing
BrakePower, BrakeDistribution, Handbrake, AntiLockBrakesActive
MaxSteeringAngle, SteeringParallel, FrontToe, RearToe, Caster
FinalDriveRatio, Gear1–Gear7
FrontDifferentialType, FrontDifferentialPreload, FrontDifferentialPowerLock,
  FrontDifferentialCoastLock, FrontDifferentialViscosity
RearDifferentialType, RearDifferentialPreload, RearDifferentialPowerLock,
  RearDifferentialCoastLock, RearDifferentialViscosity
CenterDifferentialType, CenterDifferentialViscosity, CenterDifferentialPowerSplit
TractionControlActive, TractionControlSlipAllowed, TractionControlMinSpeed
FrontSuspensionHeight, FrontSuspensionStiffness, FrontSuspensionBumpDamper,
  FrontSuspensionReboundDamper, FrontSuspensionAntiRollBar
RearSuspensionHeight, RearSuspensionStiffness, RearSuspensionBumpDamper,
  RearSuspensionReboundDamper, RearSuspensionAntiRollBar
WheelManufacturer
FrontTyreCompound, RearTyreCompound, FrontTyreSize, RearTyreSize
FrontLeftTyrePressure, FrontRightTyrePressure, RearLeftTyrePressure, RearRightTyrePressure
FrontLeftCamber, FrontRightCamber, RearLeftCamber, RearRightCamber
Passenger, CarConfig
```

### Parsing a setup file directly

```csharp
byte[] bytes = File.ReadAllBytes("mysetup.set");
CarSetupHelper.ParsedCarSetup parsed = CarSetupHelper.ParseSetup(bytes, isFile: true);
Console.WriteLine($"Rear wing: {parsed.RearWing}");
Console.WriteLine($"TC min speed: {parsed.TractionControlMinSpeed} m/s");
```

### Comparing two parsed setups

```csharp
var expected = CarSetupHelper.ParseSetup(refBytes, isFile: true);
var actual   = CarSetupHelper.ParseSetup(actualBytes, isFile: true);
var diffs    = CarSetupHelper.CompareSetups(expected, actual);
```

---

