using Newtonsoft.Json;

namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Defines the base model for requests/responses
    /// </summary>
    public abstract class MessagePayload
    {
        [JsonIgnore]
        public MessageClass MessageClass { get; set; }
        
        [JsonIgnore]
        public MessageCategory MessageCategory { get; set; }
        
        [JsonIgnore]
        public MessageType MessageType { get; set; }

        public MessagePayload()
        {
        }

        protected MessagePayload(MessageClass messageClass, MessageCategory messageCategory, MessageType messageType)
        {
            MessageClass = messageClass;
            MessageCategory = messageCategory;
            MessageType = messageType;
        }

        public string GetMessageDescription()
        {
            return MessageCategory.ToString() + MessageType.ToString();
        }

        /// <summary>
        /// If this is a request, this function will define the default paired response payload 
        /// message. Used for error handling.
        /// </summary>
        /// <param name="response">The <see cref="Response"/> object to be included in the response payload</param>
        public abstract MessagePayload CreateDefaultResponseMessagePayload(Response response);
    }
}
