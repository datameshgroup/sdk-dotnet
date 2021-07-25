using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    public class SaleToPOIMessage
    {
        public MessageHeader MessageHeader { get; set; }
        public MessagePayload MessagePayload { get; set; }
        public SecurityTrailer SecurityTrailer { get; set; }
    }
}
