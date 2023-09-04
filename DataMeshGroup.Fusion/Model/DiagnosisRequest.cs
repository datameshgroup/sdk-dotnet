using System.Collections.Generic;

namespace DataMeshGroup.Fusion.Model
{
    public class DiagnosisRequest : MessagePayload
    {
        public DiagnosisRequest() :
            base(MessageClass.Service, MessageCategory.Diagnosis, MessageType.Request)
        {
        }

        /// <summary>
        /// The POIID to perform the diagnosis request on. If null will default to MessageHeader.POIID
        /// </summary>
        public string POIID { get; set; }

        /// <summary>
        /// Indicates if Host Diagnosis are required. Host diagnosis is not currently supported
        /// </summary>
        public bool HostDiagnosisFlag { get; set; }

        /// <summary>
        /// Indicates the list of acquirer IDs which diagnosis should be performed on. Only present if <see cref="HostDiagnosisFlag"/> is true
        /// </summary>
        public List<string> AcquirerID { get; set; }

        public override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            return new DiagnosisResponse
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
