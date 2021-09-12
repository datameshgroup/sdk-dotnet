namespace DataMeshGroup.Fusion.Model
{
    public class CardAcquisitionRequest : MessagePayload
    {
        public CardAcquisitionRequest() :
            base(MessageClass.Service, MessageCategory.CardAcquisition, MessageType.Request)
        {
        }

        public SaleData SaleData { get; set; }
        public CardAcquisitionTransaction CardAcquisitionTransaction { get; set; }

        internal override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            return new CardAcquisitionResponse
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
