namespace DataMeshGroup.Fusion.Model
{
    public class PaymentData
    {
        public PaymentData()
        {
            PaymentType = PaymentType.Normal;
        }

        /// <summary>
        /// Defaults to "Normal". Indicates the type of payment to process. "Normal", "Refund", or "CashAdvance".
        /// </summary>
        public PaymentType PaymentType { get; set; }
        
        public bool? SplitPaymentFlag { get; set; }
        public TransactionIdentification CardAcquisitionReference { get; set; }
        public string RequestedValidityDate { get; set; }
        //public Instalment Instalment { get; set; }
        //public CustomerOrder CustomerOrder { get; set; }
        public PaymentInstrumentData PaymentInstrumentData { get; set; }
    }
}
