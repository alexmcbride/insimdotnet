namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the <see cref="IS_TINY"/> SubT.
    /// </summary>
    public enum TinyType {
        /// <summary>
        /// Keep alive. Used for maintaining the connection.
        /// </summary>
        TINY_NONE,

        /// <summary>
        /// Request for a version packet to be sent.
        /// </summary>
        TINY_VER,

        /// <summary>
        /// Close InSim.
        /// </summary>
        TINY_CLOSE,

        /// <summary>
        /// External program requesting a reply.
        /// </summary>
        TINY_PING,

        /// <summary>
        /// Reply to a ping request.
        /// </summary>
        TINY_REPLY,

        /// <summary>
        /// Vote cancelled.
        /// </summary>
        TINY_VTC,

        /// <summary>
        /// Send camera pos.
        /// </summary>
        TINY_SCP,

        /// <summary>
        /// Send state info.
        /// </summary>
        TINY_SST,

        /// <summary>
        /// Get time in hundredths (i.e. SMALL_RTP).
        /// </summary>
        TINY_GTH,

        /// <summary>
        /// Multi player end.
        /// </summary>
        TINY_MPE,

        /// <summary>
        /// Get multiplayer info (i.e. ISP_ISM).
        /// </summary>
        TINY_ISM,

        /// <summary>
        /// Race end (return to game setup screen).
        /// </summary>
        TINY_REN,

        /// <summary>
        /// All players cleared from race.
        /// </summary>
        TINY_CLR,

        /// <summary>
        /// Get all connections.
        /// </summary>
        TINY_NCN,

        /// <summary>
        /// Get all players.
        /// </summary>
        TINY_NPL,

        /// <summary>
        /// Get all results.
        /// </summary>
        TINY_RES,

        /// <summary>
        /// Send an IS_NLP.
        /// </summary>
        TINY_NLP,

        /// <summary>
        /// Send an IS_MCI.
        /// </summary>
        TINY_MCI,

        /// <summary>
        /// Send an IS_REO.
        /// </summary>
        TINY_REO,

        /// <summary>
        /// Send an IS_RST.
        /// </summary>
        TINY_RST,

        /// <summary>
        /// Send an IS_AXI - AutoX Info.
        /// </summary>
        TINY_AXI,

        /// <summary>
        /// Autocross cleared.
        /// </summary>
        TINY_AXC,

        /// <summary>
        /// Send an IS_RIP - Replay Information Packet.
        /// </summary>
        TINY_RIP,

        /// <summary>
        /// Send an IS_NCI for all guests (on host only)
        /// </summary>
        TINY_NCI,

        /// <summary>
        /// Request a SMALL_ALC is sent.
        /// </summary>
        TINY_ALC,

        /// <summary>
        /// Request all IS_AXM packets sent.
        /// </summary>
        TINY_AXM,

        /// <summary>
        /// Request IS_SLC packets for all players
        /// </summary>
        TINY_SLC
    }
}
