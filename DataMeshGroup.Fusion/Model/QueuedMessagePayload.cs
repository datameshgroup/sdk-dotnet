using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    public class QueuedMessagePayload
    {
        public QueuedMessagePayload(string serviceID, MessagePayload messagePayload)
        {
            ServiceID = serviceID; 
            MessagePayload = messagePayload;
        }

        public string ServiceID { get; set; }
        public MessagePayload MessagePayload { get; set; }
    }
}
