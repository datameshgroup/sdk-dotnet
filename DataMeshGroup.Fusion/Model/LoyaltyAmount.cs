using Newtonsoft.Json;

namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Amount of a loyalty account
    /// </summary>
    public class LoyaltyAmount
    {
        /// <summary>
        /// Optional unit of a loyalty amount. If null, defaults to "Monetary"
        /// </summary>
        public LoyaltyUnit? LoyaltyUnit { get; set; }

        /// <summary>
        /// Optional currency, if <see cref="LoyaltyUnit"/> is "Monetary"
        /// </summary>
        public CurrencySymbol? Currency { get; set; } = CurrencySymbol.AUD;
        
        /// <summary>
        /// Value of the loyalty amount
        /// </summary>
        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal AmountValue { get; set; }
    }
}
