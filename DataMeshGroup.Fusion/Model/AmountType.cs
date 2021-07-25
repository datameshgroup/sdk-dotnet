using Newtonsoft.Json;

namespace DataMeshGroup.Fusion.Model
{
    public class AmountType
    {
        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? AmountValue { get; set; }
        public string Currency { get; set; }
    }
}
