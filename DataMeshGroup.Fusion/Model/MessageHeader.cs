namespace DataMeshGroup.Fusion.Model
{
    public class MessageHeader
    {
        public string ProtocolVersion { get; set; }
        public MessageClass MessageClass { get; set; }
        public MessageCategory MessageCategory { get; set; }
        public MessageType MessageType { get; set; }
        public string ServiceID { get; set; }
        public string DeviceID { get; set; }
        public string SaleID { get; set; }
        public string POIID { get; set; }

        public string GetMessageDescription() => MessageCategory.ToString() + MessageType.ToString();

        /// <summary>
        /// Implementation of Newtonsoft 'ShouldSerialize' method. Ensures ProtocolVersion is only serialized for login message types
        /// </summary>
        public bool ShouldSerializeProtocolVersion()
        {
            return MessageCategory == MessageCategory.Login;
        }
    }
}