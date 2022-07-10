namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Data related to the result of a processed loyalty transaction.
    /// </summary>
    public class LoyaltyResult
    {
        /// <summary>
        /// Conveys the identification of the account and the associated loyalty brand.
        /// </summary>
        public LoyaltyAccount LoyaltyAccount { get; set; }

        /// <summary>
        /// Account balance after processing of the transaction
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? CurrentBalance { get; set; }

        /// <summary>
        /// Amount of a loyalty account. For a payment response, contains the redeemed loyalty amount
        /// </summary>
        public LoyaltyAmount LoyaltyAmount { get; set; }

        /// <summary>
        /// Data related to the loyalty Acquirer during a loyalty transaction.
        /// </summary>
        public LoyaltyAcquirerData LoyaltyAcquirerData { get; set; }

        /// <summary>
        /// Rebate form to an award. Present if rebates awarded
        /// </summary>
        public Rebates Rebates { get; set; }
    }
}
