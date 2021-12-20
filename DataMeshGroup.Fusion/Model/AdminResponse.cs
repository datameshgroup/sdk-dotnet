using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Response to a <see cref="AdminRequest"/>
    /// </summary>
    public class AdminResponse : MessagePayload
    {
        public AdminResponse() :
            base(MessageClass.Service, MessageCategory.Admin, MessageType.Response)
        {
        }

        public Response Response { get; set; }

        internal override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            throw new System.NotImplementedException();
        }
    }
}
