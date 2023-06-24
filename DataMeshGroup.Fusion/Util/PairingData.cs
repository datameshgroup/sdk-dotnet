using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Util
{
    public class PairingData
    {
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

        private string posName;
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
