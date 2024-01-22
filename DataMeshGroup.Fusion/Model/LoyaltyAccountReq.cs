using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    public class LoyaltyAccountReq
    {
        public LoyaltyAccountReq() { }

        /// <summary>
        /// Reference to the last CardAcquisition, to use the same card
        /// </summary>
        public TransactionIdentification CardAcquisitionReference { get; set; }

        /// <summary>
        /// Identification of a Loyalty account
        /// </summary>
        public LoyaltyAccountID LoyaltyAccountID{ get; set; }
    }
}
