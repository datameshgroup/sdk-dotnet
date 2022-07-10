using Newtonsoft.Json;

namespace DataMeshGroup.Fusion.Model
{
    public class SaleItemRebate
    {
        /// <summary>
        /// A unique identifier for the sale item within the context of this payment. e.g. a 0..n integer which increments by one for each sale item.
        /// </summary>
        public int ItemID { get; set; }

        /// <summary>
        /// A unique identifier for the product within the merchant, such as the SKU. For example if two customers purchase the same product at two different stores owned by the merchant, both purchases should contain the same ProductCode.
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// The barcode, or standard unique identifier for the product. Either the UPC, EAN, or ISBN. Required for products with a UPC, EAN, or ISBN
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
        /// Total amount of the item
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal ItemAmount { get; set; }

        /// <summary>
        /// Short text to qualify a rebate on an line item.
        /// </summary>
        public string RebateLabel { get; set; }
    }
}
