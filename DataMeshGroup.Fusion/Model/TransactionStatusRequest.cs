namespace DataMeshGroup.Fusion.Model
{
    public class TransactionStatusRequest : MessagePayload
    {

        public TransactionStatusRequest() : base(MessageClass.Service, MessageCategory.TransactionStatus, MessageType.Request)
        {
        }

        public MessageReference MessageReference { get; set; }
    }
}
