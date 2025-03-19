namespace DataMeshGroup.Fusion.Model
{
    public class MessageHeader
    {
        /// <summary>
        /// Defines the protocol version to use. Set to 3.1 or 3.1-dmg
        /// </summary>
        public string ProtocolVersion { get; set; }
        
        /// <summary>
        /// The class of message
        /// </summary>
        public MessageClass MessageClass { get; set; }
        
        /// <summary>
        /// The category of the message within a class
        /// </summary>
        public MessageCategory MessageCategory { get; set; }
        
        /// <summary>
        /// The type of a message within a category
        /// </summary>
        public MessageType MessageType { get; set; }
        
        /// <summary>
        /// Unique identifier for the transaction
        /// </summary>
        public string ServiceID { get; set; }


        /// <summary>
        /// Unique identifier for the device
        /// </summary>
        public string DeviceID { get; set; }

        /// <summary>
        /// Unique identifier for the Sale System
        /// </summary>
        public string SaleID { get; set; }

        /// <summary>
        /// Unique identifier for the payment terminal
        /// </summary>
        public string POIID { get; set; }

        /// <summary>
        /// Version of the library to use
        /// </summary>
        public int LibVersion { get; set; }

        public MessageHeader()    
        {
            LibVersion = 2;
        }

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