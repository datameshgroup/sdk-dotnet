using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Util
{
    /// <summary>
    /// Indicates if the pairing is for cloud, USB, or Bluetooth
    /// </summary>
    public enum PairingMode
    {
        Cloud = 0,
        USB = 1,
        Bluetooth = 2
    }

    public enum PortParity
    {
        None = 0,
        Odd = 1,
        Even = 2,
        Mark = 3,
        Space = 4
    }

    /// <summary>
    /// Indicates if the USB port is connected to the base or terminal
    /// </summary>
    public enum PortType
    {
        SerialModeBaseConnectedUSB = 0,
        SerialModeTerminalConnectedUSB = 1,
    }


    public class PairingData
    {
        /// <summary>
        /// Indicates if the pairing is for cloud, USB, or Bluetooth. Optional value. 0 (Cloud) is the default.
        /// </summary>
        [JsonProperty("m")]
        public PairingMode? Mode { get; set; }

        /// <summary>
        /// Indicates if the USB port is connected to the base or terminal. Optional value. 0 (SerialModeBaseConnectedUSB) is the default.
        /// </summary>
        [JsonProperty("u")]
        public PortType? PortType { get; set; }

        /// <summary>
        /// Baud rate for the serial port. Optional value. 115200 is the default
        /// Supported values: 9600, 19200, 38400, 115200
        /// </summary>
        [JsonProperty("sb")]
        public int? PortParamsBaudRate { get; set; }

        /// <summary>
        /// Parity for the serial port. Optional value. 0 (None) is the default
        /// Supported values 0=None, 1=Odd, 2=Even, 3=Mark, 4=Space
        /// </summary>
        [JsonProperty("sp")]
        public PortParity? PortParamsParity { get; set; }

        /// <summary>
        /// Data bits for the serial port. Optional value. 8 is the default
        /// </summary>
        [JsonProperty("sd")]
        public int PortParamsDataBits { get; set; }

        /// <summary>
        /// Encryption key used for terminal to pos comms
        /// </summary>
        [JsonProperty("k")]
        public string EncryptionKey { get; set; }

        /// <summary>
        /// Sale ID (UUIDv4 formatted)
        /// </summary>
        [JsonProperty("s")]
        public string SaleID { get; set; }

        /// <summary>
        /// UUIDv4 formatted pairing POIID
        /// </summary>
        [JsonProperty("p")]
        public string PairingPOIID { get; set; }

        /// <summary>
        /// 48 random characters 0-9, A-F
        /// </summary>
        [JsonProperty("k")]
        public string KEK { get; set; }

        [JsonProperty("c")]
        public string CertificationCode { get; set; }

        [JsonProperty("n")]
        public string POSName { get; set; }

        [JsonProperty("v")]
        public int Version { get; set; }

        public static string CreateKEK()
        {
            Random r = new Random();
            char[] chars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            string result = "";
            for (int i = 0; i < 48; i++)
            {
                result += chars[r.Next(15)];
            }
            return result;
        }
    }
}
