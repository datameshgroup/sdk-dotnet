using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    public class StoredValueAccountStatus
    {
        public StoredValueAccountStatus() { 
        }

        /// <summary>
        /// Identification of the stored value account or the stored value card
        /// </summary>
        public StoredValueAccountID StoredValueAccountID { get; set; }

        /// <summary>
        /// Balance of the stored value card
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? CurrentBalance {  get; set; }
    }
}
