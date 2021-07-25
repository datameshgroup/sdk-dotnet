using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace DataMeshGroup.Fusion.Model
{
    public class TransactionIdentification
    {
        public TransactionIdentification(string transactionID)
        {
            TransactionID = transactionID;
            TimeStamp = DateTime.UtcNow;
        }

        public string TransactionID { get; set; }

        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime TimeStamp { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null || !(obj is TransactionIdentification))
            {
                return false;
            }

            var ti = obj as TransactionIdentification;
            return (TimeStamp == ti.TimeStamp) && (TransactionID == ti.TransactionID);
        }

        public override int GetHashCode()
        {
            return TimeStamp.GetHashCode() + TransactionID.GetHashCode();
        }
    }
}
