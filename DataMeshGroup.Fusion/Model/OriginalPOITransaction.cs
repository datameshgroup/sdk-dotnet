using Newtonsoft.Json;

namespace DataMeshGroup.Fusion.Model
{
    public class OriginalPOITransaction
    {
        public string SaleID { get; set; }

        public string POIID { get; set; }

        public TransactionIdentification POITransactionID { get; set; }

        public bool? ReuseCardDataFlag { get; set; }

        public string ApprovalCode { get; set; }

        public string CustomerLanguage { get; set; }

        public string AcquirerID { get; set; }
        
        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? AmountValue { get; set; }
        
        public TransactionIdentification HostTransactionID { get; set; }
        
        public bool? LastTransactionFlag { get; set; }
    }
}
