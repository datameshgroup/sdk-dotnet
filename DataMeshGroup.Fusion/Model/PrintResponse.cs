using DataMeshGroup.Fusion.Model;
using System;
using System.Runtime.CompilerServices;

namespace DataMeshGroup.Fusion.Model
{
    public class PrintResponse : MessagePayload
    {
        public PrintResponse() :
            base(MessageClass.Device, MessageCategory.Print, MessageType.Response)
        {
        }

        /// <summary>
        /// Mirrored from request
        /// </summary>
        public DocumentQualifier DocumentQualifier { get; set; }

        public Response Response { get; set; }

        public override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            throw new NotImplementedException();
        }
    }
}