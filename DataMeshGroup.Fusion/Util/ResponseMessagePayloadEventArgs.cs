using DataMeshGroup.Fusion.Model;

namespace DataMeshGroup.Fusion
{
    public class MessagePayloadEventArgs<T> where T : MessagePayload
    {
        // public event EventHandler<>
        public MessagePayloadEventArgs(T messagePayload)
        {
            MessagePayload = messagePayload;
        }

        public T MessagePayload { get; set; }
    }
}
