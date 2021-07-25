namespace DataMeshGroup.Fusion.Model
{
    public class CashHandlingDevice
    {
        public bool? CashHandlingOKFlag { get; set; }
        public string Currency { get; set; }
        public CoinsOrBills CoinsOrBills { get; set; }
    }
}
