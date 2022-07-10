namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Data related to the loyalty Acquirer during a loyalty transaction.
    /// </summary>
    public class LoyaltyAcquirerData
    {
        /// <summary>
        /// Identification of the loyalty Acquirer, if available
        /// </summary>
        public string LoyaltyAcquirerID { get; set; }

        /// <summary>
        /// Code assigned to a transaction approval by the Acquirer.
        /// Format is specific to the loyalty acquirer.
        /// Only present if included by the loyalty acquirer.
        /// </summary>
        public string ApprovalCode { get; set; }

        /// <summary>
        /// Identification of the Transaction for the Loyalty Acquirer. 
        /// Format is specific to the loyalty acquirer.
        /// Only present if included by the loyalty acquirer.
        /// </summary>
        public TransactionIdentification LoyaltyTransactionID { get; set; }

        /// <summary>
        /// Identifier of a reconciliation period with a payment or loyalty host.
        /// Only present if included by the loyalty acquirer.
        /// </summary>
        public string HostReconciliationID { get; set; }
    }
}
