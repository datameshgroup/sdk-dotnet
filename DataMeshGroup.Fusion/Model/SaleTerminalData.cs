using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Information related to the software and hardware feature of the Sale Terminal.
    /// In the Login Request, if a Sale Terminal is involved in the login. In other messages, when a logical device is out of order (SaleCapabilites), or when the other data have changed since or were not in the Login.
    /// </summary>
    public class SaleTerminalData
    {

        /// <summary>
        /// Construct a default SaleTerminalData
        /// </summary>
        /// <param name="loginRequest">true, set defaults for <see cref="TerminalEnvironment"/>, <see cref="SaleCapabilities"/>, and <see cref="SaleProfile"/> </param>
        public SaleTerminalData(bool loginRequest = true)
        {
            if(loginRequest)
            {
                TerminalEnvironment = DataMeshGroup.Fusion.Model.TerminalEnvironment.Attended;
                SaleCapabilities = new List<SaleCapability>();
                SaleProfile = new SaleProfile() { GenericProfile = GenericProfile.Basic };
            }
        }

        /// <summary>
        /// Environment of the Terminal.
        /// Sent in the Login Request (resp. Response) to identify the environment of the Sale System (resp. POI System) during the session. In other message, when the data has changed since the Login.
        /// </summary>
        public TerminalEnvironment? TerminalEnvironment { get; set; }

        /// <summary>
        /// Hardware capabilities of the Sale Terminal.
        /// </summary>
        public List<SaleCapability> SaleCapabilities { get; set; }

        /// <summary>
        /// Functional profile of the Sale Terminal. Sent in the Login Request to identify the functions that might be requested by the Sale Terminal during the session.
        /// </summary>
        public SaleProfile SaleProfile { get; set; } 

        /// <summary>
        /// Identification of a group of transaction on a POI Terminal, having the same Sale features.
        /// Could be used to group POI for reconciliation or other purpose defined by the Sale System. The default value is assigned by the Login Request. In other message, when the data has changed since or was not in the Login.
        /// </summary>
        public string TotalsGroupID { get; set; }

        /// <summary>
        /// Unique identifier for the sale terminal hardware. i.e. Sale terminal serial number.
        /// </summary>
        public string DeviceID { get; set; }
    }
}
