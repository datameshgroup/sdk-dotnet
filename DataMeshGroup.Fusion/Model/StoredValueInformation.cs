using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    public class StoredValueInformation
    {
        /// <summary>
        /// Identification of operation to proceed on the stored value account or the stored value card
        /// </summary>
        public StoredValueTransactionType StoredValueTransactionType { get; set; }

        /// <summary>
        /// Product code of stored value item.
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// A standard unique identifier for the product. Either the UPC, EAN, or ISBN. Required for products with a UPC, EAN, or ISBN.
        /// </summary>
        public string EanUpc { get; set; }

        /// <summary>
        /// Indicates the amount to be loaded onto the account. Exclusive of fees.
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? ItemAmount { get; set; }

        /// <summary>
        /// Total of fees associated with the transaction
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? TotalFeesAmount { get; set; }

        /// <summary>
        /// Three character(ISO 4217 formatted) currency code.
        /// </summary>
        public string Currency { get; set; }
    }
}
