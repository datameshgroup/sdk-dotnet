using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model.Transit
{
    /// <summary>
    /// Defines transit specific details for the payment request. Generic data elements associated with the transit payment can be found elsewhere in the payment request. 
    ///
    /// e.g.
    ///   - The DriverID present in Fusion schema as <see cref="SaleData.OperatorID"/>
    ///   - The TripID present in Fusion schema as <see cref="SaleData.SaleTransactionID"/>
    ///   - The VehicleID present in Fusion schema as <see cref="SaleData.SponsoredMerchant.SiteId"/>
    /// 
    /// </summary>
    public class TransitData
    {
        /// <summary>
        /// Indicates if the vehicle is wheelchair accessible/certified.
        /// </summary>
        public bool IsWheelchairEnabled { get; set; }

        /// <summary>
        /// Contains details of the trip.
        /// </summary>
        public Trip Trip { get; set; }

        // <summary>
        /// Optional new field specific for SPOTTO and GIRAFFE
        /// </summary>
        public string ODBS { get; set; }

        /// <summary>
        /// Custom tags for managing the behaviour of the transit payment request.
        /// </summary>
        /// <example>["NTAllowTSSSubsidy", "NTAllowTSSLift", "QLDAllowTSSSubsidy", "NSWAllowTSSLift", "NSWAllowTSSSubsidy"]</example>
        public List<string> Tags { get; set; }
    }
}
