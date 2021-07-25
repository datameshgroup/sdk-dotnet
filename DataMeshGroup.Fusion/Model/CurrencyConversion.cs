namespace DataMeshGroup.Fusion.Model
{
    public class CurrencyConversion
    {
        public bool? CustomerApprovedFlag { get; set; }
        public AmountType ConvertedAmount { get; set; }
        public string Rate { get; set; }
        public string Markup { get; set; }
        public string Commission { get; set; }
        public string Declaration { get; set; }
    }
}
