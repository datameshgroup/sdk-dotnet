using System.Collections.Generic;

namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Represents a sale item in the product basket
    /// </summary>
    public class SaleItem
    {
        public SaleItem()
        {
            Quantity = 1;
            UnitOfMeasure = UnitOfMeasure.Other;
        }

        /// <summary>
        /// The Sale Item identification inside the transaction
        /// </summary>
        public string ItemID { get; set; }

        /// <summary>
        /// Merchant assigned product code of the item
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// A standard unique identifier for the product. Either the UPC, EAN, or ISBN.
        /// </summary>
        public string EanUpc { get; set; }

        /// <summary>
        /// Unit of measure of the Quantity
        /// </summary>
        public UnitOfMeasure UnitOfMeasure { get; set; }

        /// <summary>
        /// Item unit quantity
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Price per item unit. Present if Quantity is included.
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Total amount of the item
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal ItemAmount { get; set; }

        /// <summary>
        /// Type of tax associated with the item. Default = "GST"
        /// </summary>
        public string TaxCode { get; set; }

        /// <summary>
        /// Commercial or distribution channel of the item. Default = "Unknown"
        /// </summary>
        public string SaleChannel { get; set; }

        /// <summary>
        /// Product name of the item.
        /// </summary>
        public string ProductLabel { get; set; }

        /// <summary>
        /// Additional information, or more detailed description of the product item
        /// </summary>
        public string AdditionalProductInfo { get; set; }

        /// <summary>
        /// Cost of the product to the merchant
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal CostBase { get; set; }

        /// <summary>
        /// If applied, the amount this sale item was discounted by
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal Decimal { get; set; }

        /// <summary>
        /// Product item category
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Product item sub category
        /// </summary>
        public string SubCategory { get; set; }

        /// <summary>
        /// Brand name - typically visible on the product packaging or label
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Remaining number of this item in stock
        /// </summary>
        public string QuantityInStock { get; set; }


        /// <summary>
        /// Array of string with descriptive tags for the product
        /// </summary>
        public List<string> Tags { get; set; }
    }
}
