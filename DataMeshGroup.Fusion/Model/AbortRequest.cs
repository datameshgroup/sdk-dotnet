using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    public class AbortRequest: MessagePayload

    {
        public AbortRequest(): 
            base(MessageClass.Service, MessageCategory.Abort, MessageType.Request)
        {
        }

        public MessageReference MessageReference { get; set; }
        public string AbortReason { get; set; }

        internal override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            return null;
        }

        //public DisplayOutput DisplayOutput { get; set; }
    }
}
