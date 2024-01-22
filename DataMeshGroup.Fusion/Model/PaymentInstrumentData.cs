namespace DataMeshGroup.Fusion.Model
{
    public class PaymentInstrumentData
    {
        public PaymentInstrumentType? PaymentInstrumentType { get; set; }
        public CardData CardData { get; set; }
        //public CheckData CheckData { get; set; }
        //public MobileData MobileData { get; set; }

        /// <summary>
        /// Identification of the stored value account or the stored value card        
        /// </summary>
        public StoredValueAccountID StoredValueAccountID {  get; set; }
    }
}
