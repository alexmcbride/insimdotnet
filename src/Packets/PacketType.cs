namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the packet type.
    /// </summary>
    public enum PacketType {
        /// <summary>
        /// Not used.
        /// </summary>
        ISP_NONE,

        /// <summary>
        /// InSim Initialise
        /// </summary>
        ISP_ISI,

        /// <summary>
        /// Version Info
        /// </summary>
        ISP_VER,

        /// <summary>
        /// Multi-purpose
        /// </summary>
        ISP_TINY,

        /// <summary>
        /// Multi purpose
        /// </summary>
        ISP_SMALL,

        /// <summary>
        /// State info
        /// </summary>
        ISP_STA,

        /// <summary>
        /// Single character
        /// </summary>
        ISP_SCH,

        /// <summary>
        /// State flags pack
        /// </summary>
        ISP_SFP,

        /// <summary>
        /// Set car camera
        /// </summary>
        ISP_SCC,

        /// <summary>
        /// Cam pos pack
        /// </summary>
        ISP_CPP,

        /// <summary>
        /// Start multiplayer
        /// </summary>
        ISP_ISM,

        /// <summary>
        /// Message out
        /// </summary>
        ISP_MSO,

        /// <summary>
        /// Hidden /i message
        /// </summary>
        ISP_III,

        /// <summary>
        /// Type message or /command
        /// </summary>
        ISP_MST,

        /// <summary>
        /// Message to a connection
        /// </summary>
        ISP_MTC,

        /// <summary>
        /// Set screen mode
        /// </summary>
        ISP_MOD,

        /// <summary>
        /// Vote notification
        /// </summary>
        ISP_VTN,

        /// <summary>
        /// Race start
        /// </summary>
        ISP_RST,

        /// <summary>
        /// New connection
        /// </summary>
        ISP_NCN,

        /// <summary>
        /// Connection left
        /// </summary>
        ISP_CNL,

        /// <summary>
        /// Connection renamed
        /// </summary>
        ISP_CPR,

        /// <summary>
        /// New player (joined race)
        /// </summary>
        ISP_NPL,

        /// <summary>
        /// Player pit (keeps slot in race)
        /// </summary>
        ISP_PLP,

        /// <summary>
        /// Player leave (spectate - loses slot)
        /// </summary>
        ISP_PLL,

        /// <summary>
        /// Lap time
        /// </summary>
        ISP_LAP,

        /// <summary>
        /// Split x time
        /// </summary>
        ISP_SPX,

        /// <summary>
        /// Pit stop start
        /// </summary>
        ISP_PIT,

        /// <summary>
        /// Pit stop finish
        /// </summary>
        ISP_PSF,

        /// <summary>
        /// Pit lane enter / leave
        /// </summary>
        ISP_PLA,

        /// <summary>
        /// Camera changed
        /// </summary>
        ISP_CCH,

        /// <summary>
        /// Penalty given or cleared
        /// </summary>
        ISP_PEN,

        /// <summary>
        /// Take over car
        /// </summary>
        ISP_TOC,

        /// <summary>
        /// Flag (yellow or blue)
        /// </summary>
        ISP_FLG,

        /// <summary>
        /// Player flags (help flags)
        /// </summary>
        ISP_PFL,

        /// <summary>
        /// Finished race
        /// </summary>
        ISP_FIN,

        /// <summary>
        /// Result confirmed
        /// </summary>
        ISP_RES,

        /// <summary>
        /// Reorder (info or instruction)
        /// </summary>
        ISP_REO,

        /// <summary>
        /// Node and lap packet
        /// </summary>
        ISP_NLP,

        /// <summary>
        /// Multi car info
        /// </summary>
        ISP_MCI,

        /// <summary>
        /// Message type extended
        /// </summary>
        ISP_MSX,

        /// <summary>
        /// Message to local computer
        /// </summary>
        ISP_MSL,

        /// <summary>
        /// Car reset
        /// </summary>
        ISP_CRS,

        /// <summary>
        /// Delete buttons / receive button requests
        /// </summary>
        ISP_BFN,

        /// <summary>
        /// Autocross layout information
        /// </summary>
        ISP_AXI,

        /// <summary>
        /// Hit an autocross object
        /// </summary>
        ISP_AXO,

        /// <summary>
        /// Send button
        /// </summary>
        ISP_BTN,

        /// <summary>
        /// Button Click
        /// </summary>
        ISP_BTC,

        /// <summary>
        /// Button type
        /// </summary>
        ISP_BTT,

        /// <summary>
        /// Replay information packet
        /// </summary>
        ISP_RIP,

        /// <summary>
        /// Screenshot
        /// </summary>
        ISP_SSH,

        /// <summary>
        /// Contact (collision report)
        /// </summary>
        ISP_CON,

        /// <summary>
        /// contact car + object (collision report)
        /// </summary>
        ISP_OBH,

        /// <summary>
        /// report incidents that would violate HLVC
        /// </summary>
        ISP_HLV,

        /// <summary>
        /// player cars
        /// </summary>
        ISP_PLC,

        /// <summary>
        /// autocross multiple objects
        /// </summary>
        ISP_AXM,

        /// <summary>
        /// admin command report
        /// </summary>
        ISP_ACR,

        /// <summary>
        /// car handicaps
        /// </summary>
        ISP_HCP,

        /// <summary>
        /// new connection - extra info for host.
        /// </summary>
        ISP_NCI,

        /// <summary>
        /// join request
        /// </summary>
        ISP_JRR,

        /// <summary>
        /// report InSim checkpoint / InSim circle
        /// </summary>
        ISP_UCO,

        /// <summary>
        /// Object control (currently used for lights)
        /// </summary>
        ISP_OCO,

        /// <summary>
        /// Multi purpose - target to connection
        /// </summary>
        ISP_TTC,

        /// <summary>
        /// Connection has selected a car
        /// </summary>
        ISP_SLC,

        /// <summary>
        /// Car state changed
        /// </summary>
        ISP_CSC,

        /// <summary>
        /// Admin request
        /// </summary>
        IRP_ARQ = 250,

        /// <summary>
        /// Admin response
        /// </summary>
        IRP_ARP = 251,

        /// <summary>
        /// Host list request
        /// </summary>
        IRP_HLR = 252,

        /// <summary>
        /// Host
        /// </summary>
        IRP_HOS = 253,

        /// <summary>
        /// Select host
        /// </summary>
        IRP_SEL = 254,

        /// <summary>
        /// Error
        /// </summary>
        IRP_ERR = 255,
    }
}
