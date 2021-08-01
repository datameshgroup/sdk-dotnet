﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    public class ReconciliationResponse : MessagePayload
    {
        public ReconciliationResponse() :
            base(MessageClass.Service, MessageCategory.Reconciliation, MessageType.Response)
        {
        }
        public Response Response { get; set; }

        /// <summary>
        /// Type of Reconciliation requested by the Sale to the POI.
        /// </summary>
        public ReconciliationType ReconciliationType { get; set; }

        /// <summary>
        /// Identification of the reconciliation period between Sale and POI.
        /// </summary>
        public string POIReconciliationID { get; set; }

        /// <summary>
        /// Present if <see cref="Response"/> is Success. One set of totals per value of CardBrand and AcquirerID, ..., TotalsGroupID if presents
        /// </summary>
        public List<TransactionTotals> TransactionTotals { get; set; }


        public string TID { get; set; }
        public string MID { get; set; }
        public string AcquirerID { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.IsoDateTimeConverter))]
        public DateTime? LastShiftTotalTime { get; set; }

        /// <summary>
        /// Builds a plain text version of the reconciliation receipt
        /// </summary>
        /// <returns>
        /// A plain text version of the receipt, or null if no values exist
        /// </returns>
        public string GetReceiptAsPlainText()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Datamesh Terminal");
            sb.AppendLine(GetEnumDescription(ReconciliationType).ToUpper());
            sb.AppendLine();
            sb.AppendLine(DateTime.Now.ToString("G"));
            sb.AppendLine($"Merchant ID\t\t{MID}");
            sb.AppendLine($"Terminal ID\t\t{TID}");
            sb.AppendLine();
            if (LastShiftTotalTime != null)
            {
                sb.AppendLine("Previous Reset");
                sb.AppendLine(LastShiftTotalTime?.ToString("G"));
                sb.AppendLine();
            }
            
            // TODO: populate receipt based on TransactionTotals

            return sb.ToString();
        }

        protected static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}
