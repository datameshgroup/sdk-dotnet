using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    public class StoredValueResult : StoredValueInformation
    {
        public StoredValueResult() {            
        }

        /// <summary>
        /// Data related to the result of the stored value card transaction.
        /// </summary>
        public StoredValueAccountStatus StoredValueAccountStatus { get; set; }
    }
}
