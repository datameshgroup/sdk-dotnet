using Newtonsoft.Json;
using System;

namespace DataMeshGroup.Fusion.Model
{
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
        public Parity? PortParamsParity { get; set; }

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
