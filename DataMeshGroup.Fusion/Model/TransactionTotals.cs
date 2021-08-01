using System.Collections.Generic;

namespace DataMeshGroup.Fusion.Model
{
    public class TransactionTotals
    {
        /// <summary>
        /// Type of payment instrument
        /// </summary>
        public PaymentInstrumentType PaymentInstrumentType { get; set; }

        public string AcquirerID { get; set; }

        /// <summary>
        /// If <see cref="Response.Result"/> is Partial, and the reconciliation with this Acquirer failed.
        /// </summary>
        public string ErrorCondition { get; set; }

        public string HostReconciliationID { get; set; }

        /// <summary>
        /// If configured to present totals per card brand, and Response.Result is Success
        /// </summary>
        public string CardBrand { get; set; }

        public string POIID { get; set; }
        public string SaleID { get; set; }
        public string OperatorID { get; set; }
        public string ShiftNumber { get; set; }
        public string TotalsGroupID { get; set; }
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
