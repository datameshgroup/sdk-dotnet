using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    public class MessageReference
    {
        public MessageCategory MessageCategory { get; set; }
        public string ServiceID { get; set; }
        public string DeviceID { get; set; }
        public string SaleID { get; set; }
        public string POIID { get; set; }
    }
}
