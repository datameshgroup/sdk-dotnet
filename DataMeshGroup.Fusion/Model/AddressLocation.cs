using DataMeshGroup.Fusion.Model.Transit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Details of the address configured for this POI terminal 
    /// </summary>
    public class AddressLocation
    {
        /// <summary>
        /// First line of the address
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Second line of the address
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// State 
        /// </summary>
        public string AddressState { get; set; }

        /// <summary>
        // Location identifier for this terminal
        /// </summary>
        public string Location { get; set; }
    }
}