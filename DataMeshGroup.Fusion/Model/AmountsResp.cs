namespace DataMeshGroup.Fusion.Model
{
    public class AmountsResp
    {
        public CurrencySymbol? Currency { get; set; } = CurrencySymbol.AUD;

        /// <summary>
        /// The total authorized amount. Includes authorized amount, cash, tips, surcharge & loyalty
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? AuthorizedAmount { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? TotalRebatesAmount { get; set; }

        /// <summary>
        /// Value of the fees associated with this payment. Not included in <see cref="AuthorizedAmount"/>
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? TotalFeesAmount { get; set; }

        /// <summary>
        /// When cashback is enabled, contains the cash component of <see cref="AuthorizedAmount"/>
        /// </summary>        
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? CashBackAmount { get; set; }

        /// <summary>
        /// When tipping is enabled, contains the tip component of <see cref="AuthorizedAmount"/>
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? TipAmount { get; set; }

        /// <summary>
        /// When dynamic surcharge is enabled, contains the surcharge component of <see cref="AuthorizedAmount"/>
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? SurchargeAmount { get; set; }

        /// <summary>
        /// When Success=Partial, contains the <see cref="RequestedAmount"/> from the <see cref="PaymentRequest"/>
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? RequestedAmount { get; set; }

        /// <summary>
        /// When Success=Partial, <see cref="PartialAuthorizedAmount"/> contains the value of the <see cref="RequestedAmount"/> which has been authorized
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? PartialAuthorizedAmount { get; set; }
    }
}
