using GameSystem.Setting;
using System.Text.RegularExpressions;
using UnityEngine;

namespace GameSystem
{
    public partial class NetworkSystem : SubSystem<NetworkSystemSetting>
    {
        // Serialization and deserialization of network packet
        const int DecimalPlace = 4;
        /// <summary>
        /// Retain numbers to given decimal place
        /// </summary>
        static readonly Regex floatCompressor = new Regex("\\:(-?\\d+\\.\\d{1," + DecimalPlace + "})\\d*([,\\}e])");

        // interface
        public static string PacketToString(PacketBase packet)
        {
            string output = floatCompressor.Replace(JsonUtility.ToJson(packet), ":$1$2");
            if (string.IsNullOrWhiteSpace(output)) throw new System.Exception("PacketEmpty!");
            return output;
        }
        public static PacketBase StringToPacket(string str)
        {
            try
            {
                PacketBase temp = JsonUtility.FromJson(str, typeof(PacketBase)) as PacketBase;
                return JsonUtility.FromJson(str, temp.PktType) as PacketBase;
            }
            catch (System.Exception ex)
            {
                LogError(ex);
                LogError("string:" + str);
            }
            return null;
        }
    }
}
