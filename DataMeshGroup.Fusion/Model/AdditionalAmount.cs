using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Defines an additional amount in the amounts response
    /// </summary>
    public class AdditionalAmount
    {
        /// <summary>
        /// Name of the amount
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value of the amount
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? Value { get; set; }
    }
}