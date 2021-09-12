using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataMeshGroup.Fusion.Model
{
    public class Instalment
    {
        public List<InstalmentType> InstalmentType { get; set; }
        public string SequenceNumber { get; set; }
        public string PlanID { get; set; }
        public string Period { get; set; }
        public PeriodUnit PeriodUnit { get; set; }
        public string FirstPaymentDate { get; set; }
        public string TotalNbOfPayments { get; set; }
        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? CumulativeAmount { get; set; }
        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? FirstAmount { get; set; }
        public string Charges { get; set; }
    }
}
