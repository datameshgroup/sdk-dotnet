namespace DataMeshGroup.Fusion.Model
{
    public class AmountsResp
    {
        public CurrencySymbol? Currency { get; set; } = CurrencySymbol.AUD;

        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? AuthorizedAmount { get; set; }
       
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? TotalRebatesAmount { get; set; }
        
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? TotalFeesAmount { get; set; }
        
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? CashBackAmount { get; set; }
        
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? TipAmount { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? SurchargeAmount { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? RequestedAmount { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? PartialAuthorizedAmount { get; set; }
    }
}
