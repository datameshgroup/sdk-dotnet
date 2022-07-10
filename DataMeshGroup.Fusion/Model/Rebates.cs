using System.Collections.Generic;

namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Rebate form to an award
    /// </summary>
    public class Rebates
    {

        /// <summary>
        /// The global awarded amount that is not attached to an item
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? TotalRebate { get; set; }

        /// <summary>
        /// Short text to qualify a rebate on an line item.
        /// Present if provided by the acquirer
        /// </summary>
        public string RebateLabel { get; set; }

        /// <summary>
        /// The awarded amount that is attached to an item as a rebate.
        /// Only items with rebate (identified by ItemID)
        /// </summary>
        public List<SaleItemRebate> SaleItemRebate { get; set; }
    }
}
