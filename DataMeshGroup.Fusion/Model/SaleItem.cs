using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

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
        /// Required if this SaleItem is a 'modifier' or sub-item. Contains the ItemID of the parent SaleItem. Otherwise set as null.
        /// </summary>
        public int? ParentItemID { get; set; }

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
        /// Product name of the item. Should contain a short, human readable, descriptive name of the product. For example, ProductLabel could contain the product name typically printed on the customer receipt.
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
        /// Reason the discount was applied
        /// </summary>
        public string DiscountReason { get; set; }

        /// <summary>
        /// Helper function to set the main category. This is the 0th element of <see cref="Categories"/>
        /// </summary>
        [JsonIgnore]
        public string Category
        {
            get
            {
                return Categories.ElementAtOrDefault(0);
            }
            set
            {
                var c = Categories ?? new List<string>();
                if (c.Count < 1)
                {
                    c.Add(value);
                }
                else
                {
                    c[0] = value;
                }
            }
        }

        /// <summary>
        /// Helper function to set the sub category. This is the 1st element of <see cref="Categories"/>
        /// </summary>
        [JsonIgnore]
        public string SubCategory
        {
            get
            {
                return Categories.ElementAtOrDefault(1);
            }
            set
            {
                var c = Categories ?? new List<string>();

                if (c.Count < 1)
                {
                    c.Add("Category"); // Could be an issue. Why set SubCategory with no Category?
                }

                if (c.Count < 2)
                {
                    c.Add(value);
                }
                else
                {
                    c[1] = value;
                }
            }
        }

        /// <summary>
        /// Represents the hierarchy of categories for this sale item. The main category is at element 0, sub cateogry at element 1, and so on. 
        /// e.g. a keyboard with the category hierarchy of Computers→Accessories→Keyboards would be represented as ["Computers","Accessories","Keyboards"]
        /// </summary>
        public List<string> Categories { get; set; }

        /// <summary>
        /// Brand name - typically visible on the product packaging or label
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Remaining number of this item in stock, using UnitOfMeasure as the unit of measure
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? QuantityInStock { get; set; }

        /// <summary>
        /// Array of string with descriptive tags for the product
        /// </summary>
        public List<string> Tags { get; set; }


        /// <summary>
        /// True if this is a restricted item, false otherwise. Defaults to false when field is null.
        /// </summary>        
        public bool? Restricted { get; set; }

        /// <summary>
        /// Public URL link to the sale items product page
        /// </summary>
        public string PageURL { get; set; }

        /// <summary>
        /// Public image URLs for this sale item
        /// </summary>
        public List<string> ImageURLs { get; set; }

        /// <summary>
        /// Style of the sale item. Free text field.
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// Size of the sale item
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// Colour of the sale item
        /// </summary>
        public string Colour { get; set; }

        /// <summary>
        /// Sale item weight, based on WeightUnitOfMeasure
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? Weight { get; set; }

        /// <summary>
        /// Unit of measure of the Weight
        /// </summary>
        public WeightUnitOfMeasure? WeightUnitOfMeasure { get; set; }

        /// <summary>
        /// Defines custom fields for this <see cref="SaleItem"/> which 
        /// can't be fit into the existing object.
        /// </summary>
        /// <example>
        /// "CustomFields": [
        /// {
        ///   "Name": "FuelProductCode",
        ///   "Type": "Integer",
        ///   "Value": "21"
        /// },
        /// {
        ///   "Name": "SomethingElse",
        ///   "Type": "String",
        ///   "Value": "Blah blah"
        /// },                
        /// {
        ///   "Name": "AnArray",
        ///   "Type": "Array",
        ///   "Value": "[\"1\",\"2\",\"3\"]"
        /// },    
        /// {
        ///   "Name": "AnObject",
        ///   "Type": "Object",
        ///   "Value": "{\"FuelProductCodes\": [21,22]}"
        /// }
        ///]
        /// </example>
        public List<SaleItemCustomField> CustomFields { get; set; }
    }
}
