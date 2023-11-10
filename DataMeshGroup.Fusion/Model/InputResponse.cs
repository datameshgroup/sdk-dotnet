using System;

namespace DataMeshGroup.Fusion.Model
{
    public class InputResponse : MessagePayload
    {
        public InputResponse() :
            base(MessageClass.Device, MessageCategory.Input, MessageType.Response)
        {
        }

        /// <summary>
        /// Information related to the result the output (display, print, input).
        /// If DisplayOutput present in the request.
        /// </summary>
        public OutputResult OutputResult { get; set; }

        /// <summary>
        /// Contains the result and the content of the input.
        /// </summary>
        public InputResult InputResult { get; set; }

        public override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            throw new NotImplementedException();
        }
    }
}