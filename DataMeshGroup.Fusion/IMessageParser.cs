using DataMeshGroup.Fusion.Model;
using System;

namespace DataMeshGroup.Fusion
{
    public interface IMessageParser
    {
        /// <summary>
        /// ProtocolVersion implemented by this NexoMessageParser
        /// </summary>
        string ProtocolVersion { get; }

        /// <summary>
        /// Defines if we should be using production or test keys
        /// </summary>
        bool UseTestKeyIdentifier { get; set; }

        /// <summary>
        /// Defines if we should validate the MAC on responses messages. Should always be enabled in production. Default=true
        /// </summary>
        bool EnableMACValidation { get; set; }

        /// <summary>
        /// Fired when a log event occurs which is at or above <see cref="LogLevel"/>
        /// </summary>
        event EventHandler<LogEventArgs> OnLog;


        /// <summary>
        /// Construct a SaleToPOIMessage given the parameters
        /// </summary>
        /// <param name="serviceID"></param>
        /// <param name="saleID"></param>
        /// <param name="poiID"></param>
        /// <param name="kek"></param>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        Model.SaleToPOIMessage BuildSaleToPOIMessage(string serviceID, string saleID, string poiID, string kek, Model.MessagePayload requestMessage);

        /// <summary>
        /// Converts a SaleToPOIMessage to a string
        /// </summary>
        /// <param name="saleToPOIMessage"></param>
        /// <returns></returns>
        string SaleToPOIMessageToString(Model.SaleToPOIMessage saleToPOIMessage);

        /// <summary>
        /// Converts a MessagePayload to a string
        /// </summary>
        /// <param name="messagePayload"></param>
        /// <returns></returns>
        string MessagePayloadToString(Model.MessagePayload messagePayload);

        /// <summary>
        /// Parse a JSON SaleToPOIMessage to MessagePayload, and MessageHeader
        /// </summary>
        /// <param name="saleToPOIMessageString"></param>
        /// <param name="kek"></param>
        /// <param name="messageHeader"></param>
        /// <returns></returns>
        Model.MessagePayload ParseSaleToPOIMessagePayload(string saleToPOIMessageString, string kek, out MessageHeader messageHeader);

        /// <summary>
        /// Parse a JSON SaleToPOIMessage to MessagePayload, and MessageHeader
        /// </summary>
        /// <param name="saleToPOIMessageString">JSON string to parse</param>
        /// <param name="kek">Only required when <see cref="EnableMACValidation"/> is true</param>
        /// <returns></returns>
        Model.SaleToPOIMessage ParseSaleToPOIMessage(string saleToPOIMessageString, string kek = null);

        /// <summary>
        /// Parse a JSON MessagePayload to MessagePayload object
        /// </summary>
        /// <param name="messageCategory"></param>
        /// <param name="messageType"></param>
        /// <param name="messagePayloadString"></param>
        /// <returns></returns>
        Model.MessagePayload ParseMessagePayload(MessageCategory messageCategory, MessageType messageType, string messagePayloadString);

        /// <summary>
        /// Attempts to parse a JSON SaleToPOIMessage to MessageHeader, MessagePayload, and SecurityTrailer
        /// </summary>
        /// <param name="saleToPOIMessage"></param>
        /// <param name="kek"></param>
        /// <param name="messageHeader"></param>
        /// <param name="messagePayload"></param>
        /// <param name="securityTrailer"></param>
        /// <returns></returns>
        bool TryParseSaleToPOIMessage(string saleToPOIMessage, string kek, out MessageHeader messageHeader, out MessagePayload messagePayload, out SecurityTrailer securityTrailer);

    }
}
