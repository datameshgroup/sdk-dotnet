namespace DataMeshGroup.Fusion.Model
{
    public class TransactionStatusRequest : MessagePayload
    {

        public TransactionStatusRequest() : base(MessageClass.Service, MessageCategory.TransactionStatus, MessageType.Request)
        {
        }

        public MessageReference MessageReference { get; set; }

        internal override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            return new TransactionStatusResponse
            {
                Response = response ?? new Response()
                {
                    Result = Result.Failure,
                    ErrorCondition = ErrorCondition.Aborted,
                    AdditionalResponse = ""
                }
            };
        }
    }
}
