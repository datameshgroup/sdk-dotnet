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
        /// Fired when a log event occurs which is at or above <see cref="LogLevel"/>
        /// </summary>
        event EventHandler<LogEventArgs> OnLog;


        Model.SaleToPOIMessage BuildSaleToPOIMessage(string serviceID, string saleID, string poiID, string kek, Model.MessagePayload requestMessage);

        string SaleToPOIMessageToString(Model.SaleToPOIMessage saleToPOIMessage);

        bool TryParseSaleToPOIMessage(string saleToPOIMessage, string kek, out MessageHeader messageHeader, out MessagePayload messagePayload, out SecurityTrailer securityTrailer);

        Model.MessagePayload ParseSaleToPOIMessagePayload(string saleToPOIMessageString, string kek, out MessageHeader messageHeader);

        Model.SaleToPOIMessage ParseSaleToPOIMessage(string saleToPOIMessageString, string kek);
    }
}
