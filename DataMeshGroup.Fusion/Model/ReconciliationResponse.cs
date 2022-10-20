using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

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

            _ = sb.AppendLine("DATAMESH TERMINAL");
            _ = sb.AppendLine(GetEnumDescription(ReconciliationType).ToUpper());
            _ = sb.AppendLine();
            _ = sb.AppendLine(DateTime.Now.ToString("G"));
            _ = sb.AppendLine($"MERCHANT ID\t{MID}");
            _ = sb.AppendLine($"TERMINAL ID\t{TID}");
            _ = sb.AppendLine();

            try
            {
                if (LastShiftTotalTime != null)
                {
                    _ = sb.AppendLine("PREVIOUS RESET");
                    _ = sb.AppendLine(LastShiftTotalTime.Value.ToString("G"));
                    _ = sb.AppendLine();
                }


                if (TransactionTotals == null)
                {
                    sb.AppendLine($"");
                    sb.AppendLine($"NO TOTALS");
                    sb.AppendLine($"");
                }
                else
                { 
                    // Calculate totals
                    var summedTotalsGrouped =
                        from tt in TransactionTotals
                        where tt.PaymentTotals != null
                        from pt in tt.PaymentTotals
                        where pt.TransactionType != TransactionType.Failed
                        group new { tt.CardBrand, pt.TransactionType, pt.TransactionAmount, pt.TransactionCount } by new { tt.CardBrand, pt.TransactionType } into combinedTotals
                        let summedTotals = new
                        {
                            CardBrand = combinedTotals.Key.CardBrand,
                            TransactionType = combinedTotals.Key.TransactionType,
                            TotalCount = combinedTotals.Sum(x => x.TransactionCount),
                            TotalAmount = combinedTotals.Sum(x => x.TransactionAmount)
                        }
                        group summedTotals by summedTotals.CardBrand into tmpSummedTotalsGrouped
                        select tmpSummedTotalsGrouped;

                    // Print out totals
                    _ = sb.AppendLine("CARD TOTALS");
                    _ = sb.AppendLine();
                    foreach (var summedTotals in summedTotalsGrouped)
                    {
                        _ = sb.AppendLine(summedTotals.Key.Equals("Card", StringComparison.InvariantCultureIgnoreCase) ? "DEBIT CARD" : summedTotals.Key);
                        foreach (var summedTotal in summedTotals)
                        {
                            string s = $"{summedTotal.TransactionType} ({summedTotal.TotalCount})".PadRight(20) + summedTotal.TotalAmount.Value.ToString("C");
                            _ = sb.AppendLine(s);
                        }
                        _ = sb.AppendLine();
                    }

                }
            }
            catch(Exception e)
            {
                sb.AppendLine($"");
                sb.AppendLine($"ERROR");
                sb.AppendLine($"{e.Message}");
                sb.AppendLine($"");
            }
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

        public override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            throw new NotImplementedException();
        }
    }
}
