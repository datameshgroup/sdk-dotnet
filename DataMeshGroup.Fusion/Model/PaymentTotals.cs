using Newtonsoft.Json;

namespace DataMeshGroup.Fusion.Model
{
    public class PaymentTotals
    {
        /// <summary>
        /// Type of transaction. One of Debit, Credit, ReverseDebit, ReverseCredit, OneTimeReservation, CompletedDeffered, FirstReservation, UpdateReservation, CompletedReservation, CashAdvance, IssuerInstalment, Failed, or Declined
        /// </summary>
        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// Number of processed transaction during the period.
        /// </summary>
        public int? TransactionCount { get; set; }

        /// <summary>
        /// Sum of amount of processed transaction during the period.
        /// </summary>
        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? TransactionAmount { get; set; }
    }
}
