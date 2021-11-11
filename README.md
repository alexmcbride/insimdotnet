# InSim.NET for .NET 6

**Note - this library may not be stable.**

This is a quick and dirty port of InSim.NET to .NET Core and then to .NET 6. Anything not compatible was stripped out or reworked without a huge amount of testing, also a lot of Windows specific stuff had to be removed. However, it should work OK and provides a base for things moving forward.

The [existing documentation](https://github.com/alexmcbride/insimdotnet/wiki) should still be valid.

If you get errors at runtime about missing assemblies, make sure your project's csproj file has the two following dependencies listed:
```xml
<!-- These can be found in InSim.NET project file. -->
<ItemGroup>
  <PackageReference Include="System.IO.FileSystem" Version="4.3.0" />
  <PackageReference Include="System.Text.Encoding.CodePages" Version="4.5.1" />
</ItemGroup>
```
