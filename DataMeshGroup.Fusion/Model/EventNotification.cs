using System;

namespace DataMeshGroup.Fusion.Model
{
    public class EventNotification
    {
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.IsoDateTimeConverter))]
        public DateTime TimeStamp { get; set; }

        public string EventToNotify { get; set; }
        public string EventDetails { get; set; }
    }
}
