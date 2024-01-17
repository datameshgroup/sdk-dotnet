using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    public class StoredValueData : StoredValueInformation
    {
        public StoredValueData() { }

        /// <summary>
        /// If more than one provider to manage on the POI, and StoredValueAccountID absent
        /// </summary>
        public string StoredValueProvider { get; set; }

        /// <summary>
        /// If the identification of the Stored Value account or card has been made by the Sale System before therequest
        /// </summary>
        public StoredValueAccountID StoredValueAccountID {  get; set; }

        /// <summary>
        /// Identification of a previous POI transaction.
        /// </summary>
        public OriginalPOITransaction OriginalPOITransaction { get; set; }        

    }
}
