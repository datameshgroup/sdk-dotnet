namespace DataMeshGroup.Fusion.Model
{
    public class AmountsReq
    {
        public CurrencySymbol Currency { get; set; } = CurrencySymbol.AUD;

        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))] 
        public decimal? RequestedAmount { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? CashBackAmount { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? TipAmount { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? SurchargeAmount { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? PaidAmount { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? MinimumAmountDeliver { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? MaximumCashBackAmount { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? MinimumSplitAmount { get; set; }
    }
}
