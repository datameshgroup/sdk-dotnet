using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    public class LoyaltyAccountStatus
    {
        public LoyaltyAccountStatus() { }

        /// <summary>
        /// Data related to a loyalty account processed in the transaction
        /// </summary>
        public LoyaltyAccount LoyaltyAccount { get; set; }

        /// <summary>
        /// Balance of the Account
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? CurrentBalance { get; set; }

        /// <summary>
        /// Unit of a loyalty amount
        /// </summary>
        public LoyaltyUnit LoyaltyUnit { get; set; }

        /// <summary>
        /// Currency - if LoyaltyUnit is Monetary
        /// </summary>
        public string Currency { get; set; }
    }
}
