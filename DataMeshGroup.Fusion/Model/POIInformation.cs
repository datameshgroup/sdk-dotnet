using DataMeshGroup.Fusion.Model.Transit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Information about the POI settings and configuration
    /// </summary>
    public class POIInformation
    {
        /// <summary>
        /// DataMesh terminal ID allocated to the POI terminal
        /// </summary>
        public string TID { get; set; }

        /// <summary>
        /// DataMesh merchant ID allocated to the POI terminal
        /// </summary>
        public string MID { get; set; }

        /// <summary>
        /// Version of the Fusion API running on the POI terminal
        /// </summary>
        public string FusionVersion { get; set; }

        /// <summary>
        /// Software version of the payment application running on the POI terminal
        /// </summary>
        public string SoftwareVersion { get; set; }

        /// <summary>
        /// Details of the address configured for this POI terminal 
        /// </summary>
        public AddressLocation AddressLocation { get; set; }
    }
}