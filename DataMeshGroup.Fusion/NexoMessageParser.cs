using DataMeshGroup.Fusion.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace DataMeshGroup.Fusion
{
    public class NexoMessageParser : IMessageParser
    {
        /// <summary>
        /// ProtocolVersion implemented by this NexoMessageParser
        /// </summary>
        public string ProtocolVersion => "3.1-dmg";

        /// <summary>
        /// Defines if we should be using production or test keys
        /// </summary>
        public bool UseTestKeyIdentifier { get; set; } = false;


        private readonly JsonSerializer jsonSerializer = new JsonSerializer() { NullValueHandling = NullValueHandling.Ignore };

        public NexoMessageParser()
        {
        }

        public MessagePayload ParseSaleToPOIMessage(string saleToPOIMessageString, string kek)
        {
            // Parse JSON to JObject - important to disable DateParseHandling so we get an exact json match with the request
            var rootJObject = JsonConvert.DeserializeObject<JObject>(saleToPOIMessageString, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });


            // Message from Unify could be SaleToPOIResponse or SaleToPOIRequest
            var saleToPOIMessage = rootJObject.SelectToken("SaleToPOIResponse", false) ?? rootJObject.SelectToken("SaleToPOIRequest", false);

            // Find MessageHeader and SecurityTrailer object
            var messageHeaderJObject = saleToPOIMessage?.SelectToken("MessageHeader", false);
            var securityTrailerJObject = saleToPOIMessage?.SelectToken("SecurityTrailer", false);

            // Parse JObject to MessageHeader 
            var messageHeader = messageHeaderJObject?.ToObject<MessageHeader>();
            var securityTrailer = securityTrailerJObject?.ToObject<SecurityTrailer>();

            // Throw if we can't find/parse header + trailer
            if (messageHeaderJObject is null || securityTrailerJObject is null || messageHeader is null || securityTrailer is null)
            {
                // to enable forwards-compatibility we need to just return null here instead of throwing an exception
                return null; // TODO: LOG!
            }

            // Validate type
            var type = Type.GetType($"DataMeshGroup.Fusion.Model.{messageHeader.MessageCategory}{messageHeader.MessageType}");
            if (type is null || type.IsAssignableFrom(typeof(MessagePayload)))
            {
                // to enable forwards-compatibility we need to just return null here instead of throwing an exception
                return null;
            }

            // Find Payload 
            var payloadJObject = saleToPOIMessage.SelectToken(messageHeader.GetMessageDescription(), false);
            var payload = payloadJObject?.ToObject(type);
            // Throw if we can't find/parse payload
            if (payloadJObject is null || payload is null)
            {
                // to enable forwards-compatibility we need to just return null here instead of throwing an exception
                return null;
            }

            // Validate MAC. Throws MessageFormatException
            SecurityTrailerHelper.ValidateSecurityTrailer(kek, messageHeader, securityTrailer, messageHeaderJObject.ToString(Formatting.None), payloadJObject.ToString(Formatting.None));

            // Return
            return payload as MessagePayload;
        }

        public SaleToPOIMessage BuildSaleToPOIMessage(string serviceID, string saleID, string poiID, string kek, MessagePayload requestMessage)
        {
            if (string.IsNullOrEmpty(saleID) || string.IsNullOrEmpty(poiID) || string.IsNullOrEmpty(kek))
            {
                throw new FormatException("Valid SaleId, POIID, and KEK are required");
            }

            // Construct MessageHeader from RequestMessage
            var messageHeader = new MessageHeader()
            {
                ProtocolVersion = ProtocolVersion,
                MessageClass = requestMessage.MessageClass,
                MessageCategory = requestMessage.MessageCategory,
                MessageType = requestMessage.MessageType,
                ServiceID = serviceID,
                POIID = poiID,
                SaleID = saleID
            };

            // Create JObject for header and request
            var securityTrailer = SecurityTrailerHelper.GenerateSecurityTrailer(kek, messageHeader, requestMessage, UseTestKeyIdentifier);

            return new SaleToPOIMessage()
            {
                MessageHeader = messageHeader,
                MessagePayload = requestMessage,
                SecurityTrailer = securityTrailer
            };
        }

        public string SaleToPOIMessageToString(Model.SaleToPOIMessage saleToPOIRequest)
        {
            // TODO: this could actually be a SaleToPOIRequest or SaleToPOIResponse. Need to check the message payload as that will give us 
            // a better idea. However... at this point only SaleToPOIRequest is supported
            var root = new JObject
            {
                { "SaleToPOIRequest", new JObject
                    {
                        { "MessageHeader", JObject.FromObject(saleToPOIRequest.MessageHeader, jsonSerializer) },
                        { saleToPOIRequest.MessagePayload.GetMessageDescription(), JObject.FromObject(saleToPOIRequest.MessagePayload, jsonSerializer) },
                        { "SecurityTrailer", JObject.FromObject(saleToPOIRequest.SecurityTrailer, jsonSerializer) }
                    }
                }
            };

            return root.ToString(Formatting.None);
        }
    }
}
