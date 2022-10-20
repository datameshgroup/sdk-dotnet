using System;

namespace DataMeshGroup.Fusion.Model
{
    public class SaleToPOIMessage
    {
        public MessageHeader MessageHeader { get; set; }
        public MessagePayload MessagePayload { get; set; }
        public SecurityTrailer SecurityTrailer { get; set; }
    }
}
