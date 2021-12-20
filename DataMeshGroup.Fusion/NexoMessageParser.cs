using DataMeshGroup.Fusion.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace DataMeshGroup.Fusion
{
    public class NexoMessageParser : IMessageParser
    {
        /// <summary>
        /// ProtocolVersion implemented by this NexoMessageParser. 
        /// </summary>
        public string ProtocolVersion => "3.1-dmg";

        /// <summary>
        /// Defines if we should be using production or test keys
        /// </summary>
        public bool UseTestKeyIdentifier { get; set; }

        /// <summary>
        /// Defines if we should validate the MAC on responses messages. Should always be enabled in production. Default=true
        /// </summary>
        public bool EnableMACValidation { get; set; } = true;

        /// <summary>
        /// Fired when a log event occurs which is at or above <see cref="LogLevel"/>
        /// </summary>
        public event EventHandler<LogEventArgs> OnLog;

        private readonly JsonSerializer jsonSerializer = new JsonSerializer() { NullValueHandling = NullValueHandling.Ignore };

        public NexoMessageParser()
        {
        }

        public bool TryParseSaleToPOIMessage(string saleToPOIMessageString, string kek, out MessageHeader messageHeader, out MessagePayload messagePayload, out SecurityTrailer securityTrailer)
        {
            messagePayload = null;

            try
            {
                kek = kek?.Trim();
                _ = SecurityTrailerHelper.ValidateKEK(kek); // Throws ArgumentException
            }
            catch (Exception e)
            {
                if (EnableMACValidation)
                {
                    throw;
                }
                OnLog?.Invoke(this, new LogEventArgs() { LogLevel = LogLevel.Debug, Data = $"An error occured validating the KEK. {e.Message}", Exception = e });
            }

            // Parse JSON to JObject
            JObject rootJObject = JsonConvert.DeserializeObject<JObject>(saleToPOIMessageString, new JsonSerializerSettings
            {
                DateParseHandling = DateParseHandling.None, // disable DateParseHandling so we get the same date format as Unify
                FloatParseHandling = FloatParseHandling.Decimal // ensure floating point precision is the same as the host
            });

            // Message from Unify could be SaleToPOIResponse or SaleToPOIRequest
            JToken saleToPOIMessageNode = rootJObject.SelectToken("SaleToPOIResponse", false) ?? rootJObject.SelectToken("SaleToPOIRequest", false);

            // Find MessageHeader and SecurityTrailer object
            JToken messageHeaderJObject = saleToPOIMessageNode?.SelectToken("MessageHeader", false);
            JToken securityTrailerJObject = saleToPOIMessageNode?.SelectToken("SecurityTrailer", false);

            // Parse JObject to MessageHeader 
            messageHeader = messageHeaderJObject?.ToObject<MessageHeader>();
            securityTrailer = securityTrailerJObject?.ToObject<SecurityTrailer>();

            // Throw if we can't find/parse header + trailer
            if (messageHeaderJObject is null || securityTrailerJObject is null || messageHeader is null || securityTrailer is null)
            {
                // to enable forwards-compatibility we need to just return null here instead of throwing an exception
                OnLog?.Invoke(this, new LogEventArgs() { LogLevel = LogLevel.Debug, Data = $"In TryParseSaleToPOIMessage, messageHeaderJObject is null || securityTrailerJObject is null || messageHeader is null || securityTrailer is null" });
                return false;
            }

            // Validate type - must be within DataMeshGroup.Fusion.Model and inherit from MessagePayload
            Type type = Type.GetType($"DataMeshGroup.Fusion.Model.{messageHeader.MessageCategory}{messageHeader.MessageType}");
            if (type is null || !typeof(MessagePayload).IsAssignableFrom(type))
            {
                // to enable forwards-compatibility we need to just return null here instead of throwing an exception
                OnLog?.Invoke(this, new LogEventArgs() { LogLevel = LogLevel.Debug, Data = $"In TryParseSaleToPOIMessage, type is null || type.IsAssignableFrom(typeof(MessagePayload))" });
                return false;
            }

            // Find Payload 
            JToken payloadJObject = saleToPOIMessageNode.SelectToken(messageHeader.GetMessageDescription(), false);
            object payload = payloadJObject?.ToObject(type);
            // Throw if we can't find/parse payload
            if (payloadJObject is null || payload is null)
            {
                // to enable forwards-compatibility we need to just return null here instead of throwing an exception
                OnLog?.Invoke(this, new LogEventArgs() { LogLevel = LogLevel.Debug, Data = $"In TryParseSaleToPOIMessage, payloadJObject is null || payload is null" });
                return false;
            }

            // Validate MAC. Throws MessageFormatException
            try
            {
                SecurityTrailerHelper.ValidateSecurityTrailer(
                    kek,
                    messageHeaderJson: messageHeaderJObject.ToString(Formatting.None),
                    payloadDescription: messageHeader.GetMessageDescription(),
                    payloadJson: payloadJObject.ToString(Formatting.None),
                    expectedMAC: securityTrailer.AuthenticatedData.Recipient.MAC,
                    expectedEncryptedKey: securityTrailer.AuthenticatedData.Recipient.KEK.EncryptedKey
                    );
            }
            catch (Exception e)
            {
                if (EnableMACValidation)
                {
                    throw;
                }
                OnLog?.Invoke(this, new LogEventArgs() { LogLevel = LogLevel.Debug, Data = $"Invalid MAC on response message. {e.Message}", Exception = e });
            }

            // Return
            messagePayload = payload as MessagePayload;
            return true;
        }


        public MessagePayload ParseSaleToPOIMessage(string saleToPOIMessageString, string kek)
        {
            if (!TryParseSaleToPOIMessage(saleToPOIMessageString, kek, out _, out MessagePayload messagePayload, out _))
            {
                OnLog?.Invoke(this, new LogEventArgs() { LogLevel = LogLevel.Debug, Data = $"TryParseSaleToPOIMessage returned null" });
                return null;
            }

            return messagePayload;
        }

        /// <summary>
        /// Constructs a <see cref="SaleToPOIMessage"/> based on the provided <paramref name="serviceID"/>, 
        /// <paramref name="saleID"/>, <paramref name="poiID"/>, <paramref name="kek"/>, and <paramref name="requestMessage"/>
        /// </summary>
        /// <param name="serviceID"></param>
        /// <param name="saleID"></param>
        /// <param name="poiID"></param>
        /// <param name="kek"></param>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"/>
        /// <exception cref="ArgumentException"/>
        public SaleToPOIMessage BuildSaleToPOIMessage(string serviceID, string saleID, string poiID, string kek, MessagePayload requestMessage)
        {
            // Validate request parameters as much as we can - this is the only way we have of notifying the POS something is wrong with them!
            if (string.IsNullOrEmpty(ProtocolVersion)) { throw new FormatException($"Invalid {nameof(ProtocolVersion)}. Required length is > 0"); }
            if (string.IsNullOrEmpty(serviceID)) { throw new ArgumentException($"Invalid {nameof(serviceID)}. Required length is > 0"); }
            if (string.IsNullOrEmpty(saleID)) { throw new ArgumentException($"Invalid {nameof(saleID)}. Required length is > 0"); }
            if (string.IsNullOrEmpty(poiID)) { throw new ArgumentException($"Invalid {nameof(poiID)}. Required length is > 0"); }
            if (requestMessage is null) { throw new ArgumentException($"Invalid request. Message payload must not be null"); }

            kek = kek?.Trim();
            _ = SecurityTrailerHelper.ValidateKEK(kek); // Throws ArgumentException


            // Construct MessageHeader from RequestMessage
            MessageHeader messageHeader = new MessageHeader()
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
            SecurityTrailer securityTrailer = SecurityTrailerHelper.GenerateSecurityTrailer(kek, messageHeader, requestMessage, UseTestKeyIdentifier);

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
            JObject root = new JObject
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
