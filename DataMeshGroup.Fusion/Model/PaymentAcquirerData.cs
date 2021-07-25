namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Data related to the response from the payment acquirer
    /// </summary>
    public class PaymentAcquirerData
    {
        /// <summary>
        /// The ID of the acquirer which processed the transaction
        /// </summary>
        public string AcquirerID { get; set; }

        /// <summary>
        /// The acquirer merchant ID (MID)
        /// </summary>
        public string MerchantID { get; set; }

        /// <summary>
        /// The acquirer terminal ID (TID)
        /// </summary>
        public string AcquirerPOIID { get; set; }

        /// <summary>
        /// The acquirer transaction ID
        /// </summary>
        public TransactionIdentification AcquirerTransactionID { get; set; }

        /// <summary>
        /// The Acquirer Approval Code. Also referred to as the Authentication Code
        /// </summary>
        public string ApprovalCode { get; set; }

        /// <summary>
        /// The Acquirer Response Code. Also referred as the PINPad response code
        /// </summary>
        public string ResponseCode { get; set; }

        /// <summary>
        /// The Acquirer RRN
        /// </summary>
        public string RRN { get; set; }

        /// <summary>
        /// The Acquirer STAN
        /// </summary>
        public string STAN { get; set; }

        /// <summary>
        /// Identifier of a reconciliation period with the acquirer. This normally has a date and time component in it
        /// </summary>
        public string HostReconciliationID { get; set; }
    }
}
