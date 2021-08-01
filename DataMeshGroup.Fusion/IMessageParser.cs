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


        Model.SaleToPOIMessage BuildSaleToPOIMessage(string serviceID, string saleID, string poiID, string kek, Model.MessagePayload requestMessage);

        string SaleToPOIMessageToString(Model.SaleToPOIMessage saleToPOIMessage);

        Model.MessagePayload ParseSaleToPOIMessage(string saleToPOIMessage, string kek);
    }
}
