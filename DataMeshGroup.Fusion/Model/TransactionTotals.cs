using System.Collections.Generic;

namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Usage If Result is Success, contains all the totals, classified as required by the Sale in the message request. 
    /// At least, transaction totals are provided per Acquirer, Acquirer Settlement, and Card Brand.
    /// </summary>
    public class TransactionTotals
    {
        /// <summary>
        /// Type of payment instrument
        /// </summary>
        public PaymentInstrumentType PaymentInstrumentType { get; set; }

        /// <summary>
        /// Identification of acquirer, if available
        /// </summary>
        public string AcquirerID { get; set; }

        /// <summary>
        /// If <see cref="Response.Result"/> is Partial, and the reconciliation with this Acquirer failed.
        /// </summary>
        public string ErrorCondition { get; set; }

        /// <summary>
        /// Identification of a reconciliation period with a payment or loyalty host
        /// </summary>
        public string HostReconciliationID { get; set; }

        /// <summary>
        /// If configured to present totals per card brand, and Response.Result is Success
        /// </summary>
        public string CardBrand { get; set; }

        /// <summary>
        /// If configured to present totals per POIID brand, and Response.Result is Success
        /// </summary>
        public string POIID { get; set; }

        /// <summary>
        /// If configured to present totals per card SaleID, and Response.Result is Success
        /// </summary>
        public string SaleID { get; set; }

        /// <summary>
        /// If configured to present totals per OperatorID, and Response.Result is Success
        /// </summary>
        public string OperatorID { get; set; }

        /// <summary>
        /// If configured to present totals per ShiftNumber, and Response.Result is Success
        /// </summary>
        public string ShiftNumber { get; set; }

        /// <summary>
        /// If configured to present totals per TotalsGroupID, and Response.Result is Success
        /// </summary>
        public string TotalsGroupID { get; set; }

        /// <summary>
        /// If configured to present totals per PaymentCurrency, and Response.Result is Success
        /// </summary>
        public string PaymentCurrency { get; set; }

        /// <summary>
        /// Totals of the payment transaction during the reconciliation period
        /// </summary>
        public List<PaymentTotals> PaymentTotals { get; set; }
        //public string LoyaltyUnit { get; set; }
        //public string LoyaltyCurrency { get; set; }
        //public LoyaltyTotals LoyaltyTotals { get; set; }
    }
}
