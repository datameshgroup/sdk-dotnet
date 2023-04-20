using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Filter to compute the totals.
    /// Used for the Get Totals, to request totals for a (or a combination of) particular value of the POI Terminal, Sale Terminal, Cashier, Shift or TotalsGroupID.
    /// </summary>
    public class TotalFilter
    {
        /// <summary>
        /// If totals in the response have to be computed only for this particular value of POIID
        /// </summary>
        public string POIID { get; set; }

        /// <summary>
        /// If totals in the response have to be computed only for this particular value of SaleID
        /// </summary>
        public string SaleID { get; set; }

        /// <summary>
        /// If totals in the response have to be computed only for this particular value of OperatorID
        /// </summary>
        public string OperatorID { get; set; }

        /// <summary>
        /// If totals in the response have to be computed only for this particular value of ShiftNumber
        /// </summary>
        public string ShiftNumber { get; set; }

        /// <summary>
        /// If totals in the response have to be computed only for this particular value of TotalsGroupID
        /// </summary>
        public string TotalsGroupID { get; set; }
    }
}
