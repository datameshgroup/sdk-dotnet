namespace DataMeshGroup.Fusion.Model
{
    public class CardData
    {
        public string PaymentBrand { get; set; }
        public string MaskedPAN { get; set; }
        public string PaymentAccountRef { get; set; }
        public EntryMode EntryMode { get; set; }
        public string CardCountryCode { get; set; }
        public ProtectedCardData ProtectedCardData { get; set; }
        //public string AllowedProductCode { get; set; }
        //public AllowedProduct AllowedProduct { get; set; }

        /// <summary>
        /// Only present if EntryMode is "File". Object with identifies the payment token.
        /// </summary>
        public PaymentToken PaymentToken { get; set; }
        //public CustomerOrder CustomerOrder { get; set; }
    }
}
