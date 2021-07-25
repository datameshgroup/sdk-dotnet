using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    public class RepeatedMessageResponse
    {
        public MessageHeader MessageHeader { get; set; }
        public RepeatedResponseMessageBody RepeatedResponseMessageBody { get; set; }
    }
}
