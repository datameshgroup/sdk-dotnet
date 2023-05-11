using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;

namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Defines a request for summary totals
    /// </summary>
    public class GetTotalsRequest : MessagePayload
    {
        public GetTotalsRequest() :
            base(MessageClass.Service, MessageCategory.GetTotals, MessageType.Request)
        {
        }

        /// <summary>
        /// Require to present totals per value of element included in this cluster(POI Terminal, Sale Terminal, Cashier, Shift, TotalsGroupID)
        /// </summary>
        public List<TotalDetail> TotalDetails { get; set; } = new List<TotalDetail>();

        /// <summary>
        /// Filter to compute the totals.
        /// Used for the Get Totals, to request totals for a (or a combination of) particular value of the POI Terminal, Sale Terminal, Cashier, Shift or TotalsGroupID.
        /// </summary>
        public TotalFilter TotalFilter { get; set; } = new TotalFilter();

        public override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            return new GetTotalsResponse
            {
                Response = response ?? new Response()
                {
                    Result = Result.Failure,
                    ErrorCondition = ErrorCondition.Aborted,
                    AdditionalResponse = ""
                }
            };
        }
    }
}