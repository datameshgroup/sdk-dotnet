using System;
using System.Collections.Generic;

namespace DataMeshGroup.Fusion.Model
{
    public class ReconciliationRequest : MessagePayload
    {
        public ReconciliationRequest() :
            base(MessageClass.Service, MessageCategory.Reconciliation, MessageType.Request)
        {
        }

        public ReconciliationRequest(ReconciliationType reconciliationType) :
            base(MessageClass.Service, MessageCategory.Reconciliation, MessageType.Request)
        {
            ReconciliationType = reconciliationType;
        }

        

        /// <summary>
        /// Type of Reconciliation requested by the Sale to the POI.
        /// </summary>
        public ReconciliationType ReconciliationType { get; set; }
        
        //public List<string> AcquirerID { get; set; }

        /// <summary>
        /// Identification of the reconciliation period between Sale and POI. Required when <see cref="ReconciliationType"/> is PreviousReconciliation, otherwise null
        /// </summary>
        public string POIReconciliationID { get; set; }
    }
}