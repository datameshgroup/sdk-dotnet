using Newtonsoft.Json;

namespace DataMeshGroup.Fusion.Model
{
    public class CustomerOrder
    {
        public string CustomerOrderID { get; set; }
        public string SaleReferenceId { get; set; }
        public bool? OpenOrderState { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? ForecastedAmount { get; set; }
        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? CurrentAmount { get; set; }
        public string Currency { get; set; }
        public string AccessedBy { get; set; }
        public string AdditionalInformation { get; set; }
    }
}
