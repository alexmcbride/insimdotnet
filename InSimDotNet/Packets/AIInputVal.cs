namespace InSimDotNet.Packets
{
    /// <summary>
    /// Class for the <see cref="IS_AIC"/> Info collection.
    /// </summary>
    public class AIInputVal
    {
        /// <summary>
        /// Value to set
        /// </summary>
        public AicInputType Input { get; set; }

        /// <summary>
        /// Time to hold (optional) [hundredths of a second]
        /// </summary>
        public byte Time { get; set; }

        /// <summary>
        /// Value to set
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputType"></param>
        /// <param name="toggleValue"></param>
        /// <returns></returns>
        public static AIInputVal GetFrom(AicInputType inputType, AIInputVal_ToggleValues toggleValue)
        {
            return new AIInputVal() { Value = (int)toggleValue, Input = inputType };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputType"></param>
        /// <param name="holdDuration"></param>
        /// <returns></returns>
        public static AIInputVal GetFrom(AicInputType inputType, byte holdDuration)
        {
            return new AIInputVal() { Input = inputType, Time = holdDuration };
        }

        /// <summary>
        /// Writes the <see cref="AIInputVal"/> object to the specified <see cref="PacketWriter"/>
        /// </summary>
        /// <param name="writer">The <see cref="PacketWriter"/> to write the data to.</param>
        public void GetBuffer(PacketWriter writer)
        {
            writer.Write((byte)Input);
            writer.Write(Time);
            writer.Write((ushort)Value);
        }
    }

    /// <summary>
    /// Enum for possible toggleValues
    /// </summary>
    public enum AIInputVal_ToggleValues
    {
        /// <summary>
        /// Toggle
        /// </summary>
        Toggle = 1,
        /// <summary>
        /// Switch off
        /// </summary>
        SwitchOff = 2,
        /// <summary>
        /// Switch on
        /// </summary>
        SwitchOn = 3,
    }

    /// <summary>
    /// Input types
    /// </summary>
    public enum AicInputType
    {
        /// <summary>
        /// steer: 1 hard left / 32768 centre / 65535 hard right
        /// </summary>
        CS_MSX = 0,
        /// <summary>
        /// 0 to 65535
        /// </summary>
        CS_THROTTLE = 1,
        /// <summary>
        /// 0 to 65535
        /// </summary>
        CS_BRAKE = 2,
        /// <summary>
        /// hold shift up lever
        /// </summary>
        CS_CHUP = 3,
        /// <summary>
        /// hold shift down lever
        /// </summary>
        CS_CHDN = 4,
        /// <summary>
        /// toggle
        /// </summary>
        CS_IGNITION = 5,
        /// <summary>
        /// toggle
        /// </summary>
        CS_EXTRALIGHT = 6,
        /// <summary>
        /// 1:off / 2:side / 3:low / 4:high
        /// </summary>
        CS_HEADLIGHTS = 7,
        /// <summary>
        /// hold siren - 1:fast / 2:slow
        /// </summary>
        CS_SIREN = 8,
        /// <summary>
        /// hold horn  - 1 to 5
        /// </summary>
        CS_HORN = 9,
        /// <summary>
        /// hold flash - 1:on
        /// </summary>
        CS_FLASH = 10,
        /// <summary>
        /// 0 to 65535
        /// </summary>
        CS_CLUTCH = 11,
        /// <summary>
        /// 0 to 65535
        /// </summary>
        CS_HANDBRAKE = 12,
        /// <summary>
        /// 1: cancel / 2: left / 3: right / 4: hazard
        /// </summary>
        CS_INDICATORS = 13,
        /// <summary>
        /// for shifter (leave at 255 for sequential control)
        /// </summary>
        CS_GEAR = 14,
        /// <summary>
        /// 0: none / 4: left / 5: left+ / 6: right / 7: right+
        /// </summary>
        CS_LOOK = 15,
        /// <summary>
        /// toggle
        /// </summary>
        CS_PITSPEED = 16,
        /// <summary>
        /// toggle
        /// </summary>
        CS_TCDISABLE = 17,
        /// <summary>
        /// toggle
        /// </summary>
        CS_FOGREAR = 18,
        /// <summary>
        /// toggle
        /// </summary>
        CS_FOGFRONT = 19,


        // Special values for Input
        /// <summary>
        /// Send an IS_AII (AI Info) packet
        /// </summary>
        CS_SEND_AI_INFO = 240,
        /// <summary>
        /// Start or stop sending regular IS_AII packets
        ///	Time = time interval in hundredths of a second (0 : stop)
        /// </summary>
        CS_REPEAT_AI_INFO = 241,

        /// <summary>
        /// CS_SET_HELP_FLAGS - Value can be any combination of
        ///			PIF_AUTOGEARS	// 8 - auto shift
        ///			PIF_HELP_B		// 64 - brake help
        ///			PIF_AUTOCLUTCH	// 512 - auto clutch
        ///		Default value for an AI driver is PIF_AUTOCLUTCH only
        ///		If you set PIF_AUTOGEARS you don't need to set PIF_AUTOCLUTCH
        /// </summary>
        CS_SET_HELP_FLAGS = 253,
        /// <summary>
        /// reset all inputs
        /// Most inputs are zero / CS_MSX is 32768 / CS_GEAR is 255
        /// </summary>
        CS_RESET_INPUTS = 254,
        /// <summary>
        /// The AI driver will stop the car
        /// </summary>
        CS_STOP_CONTROL = 255,
    }
}