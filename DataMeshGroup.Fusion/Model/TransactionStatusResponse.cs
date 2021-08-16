namespace DataMeshGroup.Fusion.Model
{
    public class TransactionStatusResponse : MessagePayload
    {
        public TransactionStatusResponse() : base(MessageClass.Service, MessageCategory.TransactionStatus, MessageType.Response)
        {
        }

        public Response Response { get; set; }
        public MessageReference MessageReference { get; set; }
        public RepeatedMessageResponse RepeatedMessageResponse { get; set; }

        internal override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            throw new System.NotImplementedException();
        }
    }
}
