using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Defines information related to the merchant performing the transaction
    /// </summary>
    public class SponsoredMerchant
    {
        /// <summary>
        /// Name of the merchant
        /// </summary>
        public string CommonName { get; set; }

        /// <summary>
        /// Location ot the merchant.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Identifier for the merchant site. i.e. Store Id
        /// </summary>
        public string SiteId { get; set; }

        /// <summary>
        /// Business identifier for the merchant performing the transaction. i.e. In Australia, the ABN
        /// </summary>
        public string BusinessID { get; set; }

        /// <summary>
        /// Country Code of the sponsored merchant. Format ISO 3166-1
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// The code which identifies the category of the transaction (MCC).
        /// ISO 18245
        /// This code usually the ISO code assigned by the Acquirer, but could contain additional information. A particular Merchant can have several MCC related to the type of services or goods it provides.
        /// </summary>
        public string MerchantCategoryCode { get; set;}

        /// <summary>
        /// Identifier of the sponsored merchant assigned by the payment facilitator or their acquirer
        /// </summary>
        public string RegisteredIdentifier { get; set; }

    }
}
