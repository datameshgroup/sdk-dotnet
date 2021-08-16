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
            Quantity = 1M;
            UnitOfMeasure = UnitOfMeasure.Other;
        }

        /// <summary>
        /// A unique identifier for the sale item within the context of this payment. e.g. a 0..n integer which increments by one for each sale item.
        /// </summary>
        public int ItemID { get; set; }

        /// <summary>
        /// If this SaleItem is a modifier, ParentItemID indicates the parent SaleItem. Otherwise set as null.
        /// </summary>
        public int? ParentItemID { get; set; }

        /// <summary>
        /// A unique identifier for the product within the merchant. For example if two customers purchase the same product at two different stores owned by the merchant, both purchases should contain the same ProductCode.
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// A standard unique identifier for the product. Either the UPC, EAN, or ISBN. Required for products with a UPC, EAN, or ISBN
        /// </summary>
        public string EanUpc { get; set; }

        /// <summary>
        /// Unit of measure of the Quantity. If this item has no unit of measure, set to "Other"
        /// </summary>
        public UnitOfMeasure UnitOfMeasure { get; set; }

        /// <summary>
        /// Sale item unit quantity.
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal Quantity { get; set; }

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
        /// Product name of the item. Should contain a short, human readable, descriptive name of the product. 
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
        public decimal? CostBase { get; set; }

        /// <summary>
        /// If applied, the amount this sale item was discounted by
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? Discount { get; set; }

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
        /// Remaining number of this item in stock, using UnitOfMeasure as the unit of measure
        /// </summary>
        public decimal? QuantityInStock { get; set; }


        /// <summary>
        /// Array of string with descriptive tags for the product
        /// </summary>
        public List<string> Tags { get; set; }
    }
}
