using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Response to a <see cref="LogoutRequest"/>
    /// </summary>
    public class LogoutResponse : MessagePayload
    {
        public LogoutResponse() :
            base(MessageClass.Service, MessageCategory.Logout, MessageType.Response)
        {
        }

        public Response Response { get; set; }

        public override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            throw new System.NotImplementedException();
        }
    }
}
