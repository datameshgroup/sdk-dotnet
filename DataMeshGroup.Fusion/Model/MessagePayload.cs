using Newtonsoft.Json;

namespace DataMeshGroup.Fusion.Model
{
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

        public string GetMessageDescription() => MessageCategory.ToString() + MessageType.ToString();
    }
}
