using Newtonsoft.Json;

namespace DataMeshGroup.Fusion.Model
{
    public class SaleItemRebate
    {
        public string ItemID { get; set; }
        public string ProductCode { get; set; }
        public string EanUpc { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Quantity { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? ItemAmount { get; set; }
        
        public string RebateLabel { get; set; }
    }
}
