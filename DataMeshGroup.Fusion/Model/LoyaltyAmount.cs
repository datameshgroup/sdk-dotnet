using Newtonsoft.Json;

namespace DataMeshGroup.Fusion.Model
{
    public class LoyaltyAmount
    {
        public string LoyaltyUnit { get; set; }
        
        public string Currency { get; set; }
        
        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? AmountValue { get; set; }
    }
}
