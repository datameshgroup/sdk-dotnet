using Newtonsoft.Json.Converters;
using System;

namespace DataMeshGroup.Fusion.Model
{
    public class PaymentToken
    {
        public TokenRequestedType TokenRequestedType { get; set; }
        public string TokenValue { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime ExpiryDateTime { get; set; }
    }
}
