namespace DataMeshGroup.Fusion.Model
{
    public class CardAcquisitionResponse : MessagePayload
    {
        public CardAcquisitionResponse()
            : base(MessageClass.Service, MessageCategory.CardAcquisition, MessageType.Response)
        {
        }

        public override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            throw new System.NotImplementedException();
        }

        public Response Response { get; set; }
        public SaleData SaleData { get; set; }
        public POIData POIData { get; set; }
        public PaymentBrand PaymentBrand { get; set; }
        public PaymentInstrumentData PaymentInstrumentData { get; set; }
        //public LoyaltyAccount LoyaltyAccount { get; set; }
        //public CustomerOrder CustomerOrder { get; set; }
    }
}
