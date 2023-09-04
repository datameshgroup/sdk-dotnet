using System.Collections.Generic;

namespace DataMeshGroup.Fusion.Model
{
    public class DiagnosisResponse : MessagePayload
    {
        public DiagnosisResponse()
            : base(MessageClass.Service, MessageCategory.Diagnosis, MessageType.Response)
        {
        }

        public override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Indicates the result of the request
        /// </summary>
        public Response Response { get; set; }


        /// <summary>
        /// List of sale systems the POI terminal is logged into
        /// </summary>
        public List<string> LoggedSaleID { get; set; }

        /// <summary>
        /// Indicate the availability of the POI Terminal components.
        /// </summary>
        public POIStatus POIStatus { get; set; }

        /// <summary>
        /// Indicate the reachability of each host by the POI Termina
        /// </summary>
        public List<HostStatus> HostStatus { get; set; }

        /// <summary>
        /// Extra data outside the standard API. For a <see cref="DiagnosisResponse"/> the <see cref="POIInformation"/> will be set.
        /// </summary>
        public ResponseExtensionData ExtensionData { get; set; }
    }
}
