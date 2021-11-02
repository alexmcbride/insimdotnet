InSim.NET
=========

*For a .NET 4.5 version supported by the original author of the library go [here](https://github.com/alexmcbride/insimdotnet).*

InSim.NET is a .NET InSim library for the online racing simulator [Live for Speed](http://www.lfs.net/). It allows you to connect to the game and share packets of data. These packets can be used to control Live for Speed, send commands, handle requests and receive car telemetry data. 

The library has been designed to be simple, fast and flexible, and stays as close to the original InSim protocol as possible, while saving you from the nitty-gritty of socket programming directly. 

This project site is for version 5.0 of the library, which includes full InSim, InSim Relay, OutSim and OutGauge support, improvements to the API and improved network efficiency.

To install InSimDotNet using [NuGet](http://nuget.org/) run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console).

```powershell
PM> Install-Package InSimDotNet.Net5
```

Donate
======

You can donate to the original author in his insimdotnet [github repository](https://github.com/alexmcbride/insimdotnet)

You could also get me a pint by clicking here: 
[![alt Donate](https://www.paypalobjects.com/en_US/i/btn/btn_donate_LG.gif)](https://www.paypal.com/donate?business=KSR58B9SZSMG6&no_recurring=0&item_name=A+pint+for+a+good+lad&currency_code=EUR)

InSim
=====

This is  the simplest InSim program you can write (that does something). We initialize InSim and send the message 'Hello, InSim!' to the game's chat.

```csharp
InSim insim = new InSim();

// Initialize InSim
insim.Initialize(new InSimSettings {
    Host = "127.0.0.1",
    Port = 29999,
    Admin = String.Empty,
});

// Send message to LFS
insim.Send("/msg Hello, InSim!");
```

To receive a packet bind a handler using the `InSim.Bind()` method. In this example we bind a handler for the `IS_MSO` (MeSsage Out) packet event.

```csharp
void RunInSim() {
    InSim insim = new InSim();

    // Bind MSO packet event.
    insim.Bind<IS_MSO>(MessageOut);

    // Initialize InSim
    insim.Initialize(new InSimSettings {
        Host = "127.0.0.1",
        Port = 29999,
        Admin = String.Empty,
    });
}

// Method called when MSO packet is recieved
void MessageOut(InSim insim, IS_MSO packet) {
    // Print contents of MSO message to the console.
    Console.WriteLine(packet.Msg);
}
```

To send a packet use the `InSim.Send(ISendable)` method.

```csharp
insim.Send(new IS_TINY {
    SubT = TinyType.TINY_NCN
});
```

To save bandwidth send multiple packets in a single call using the `InSim.Send(params ISendable[])` method.

```csharp
insim.Send(
    new IS_TINY {
        SubT = TinyType.TINY_NCN
    },
    new IS_SMALL {
        SubT = SmallType.SMALL_SSP,
    }
);
```

To keep a program open while InSim is still connected.

```csharp
while (insim.IsConnected) {
    Thread.Sleep(200);
}
```

Here is it all together.

```csharp
void RunInSim() {
    InSim insim = new InSim();

    // Bind packet events.
    insim.Bind<IS_NCN>(NewConnection);
    insim.Bind<IS_NPL>(NewPlayer);

    // Initialize InSim
    insim.Initialize(new InSimSettings {
        Host = "127.0.0.1",
        Port = 29999,
        Admin = String.Empty,
    });
    
    // Request all connections and players to be sent.
    insim.Send(new [] {
        new IS_TINY { SubT = TinyType.TINY_NCN },
        new IS_TINY { SubT = TinyType.TINY_NPL },
    });
    
    // Stop console app from exiting while connection is active.
    while (insim.IsConnected) {
        Thread.Sleep(200);
    }
}

// Method called when NCN packet is recieved
void NewConnection(InSim insim, IS_NCN packet) {
    // Handle new connection.
}

// Method called when NPL packet is recieved
void NewPlayer(InSim insim, IS_NPL packet) {
    // Handle new player.
}
```

InSim Relay
===========

To use InSim Relay it's a matter of setting the `InSimSettings.IsRelayHost` property to true.

```csharp
InSim insim = new InSim();

// Initialize InSim relay
insim.Initialize(new InSimSettings {
    IsRelayHost = true,
});

// Send host select packet
insim.Send(new IR_SEL { HName = "<insert host name>" });
```

OutGauge & OutSim
=================

Using OutGauge (or OutSim) is just as simple! This example prints the currently viewed car's RPM to the console.

```csharp
OutGauge outgauge = new OutGauge();

// Attach OutGauge packet event
outgauge.PacketReceived += (sender, e) => {
    Console.WriteLine(e.RPM);
};

// Start listening for packets
outgauge.Connect("127.0.0.1", 30000);
```

You can find many more examples and information about using the library in the [documentation wiki](https://github.com/alexmcbride/insimdotnet/wiki).
