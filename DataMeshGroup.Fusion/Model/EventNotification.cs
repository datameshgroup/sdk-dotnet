using System;

namespace DataMeshGroup.Fusion.Model
{
    public class EventNotification : MessagePayload
    {

        public EventNotification() : base(MessageClass.Service, MessageCategory.Event, MessageType.Notification)
        {
        }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.IsoDateTimeConverter))]
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Any event which occurs outside a transaction requested by the Sale System. The pair of data elements(EventToNotify, EventDetails) follows the same formatting rules than the pair of data elements(ErrorCondition, AdditionalResponse) inside the Response data structure.
        /// </summary>
        public EventToNotify EventToNotify { get; set; }

        /// <summary>
        /// Information about the event the POI notifies to the Sale System. Mandatory when EventToNotify = "SaleWakeUp",  "FunctionKeyPressed" or "SaleAdmin".
        /// </summary>
        public string EventDetails { get; set; }

        public override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            throw new System.NotImplementedException();
        }
    }
}
