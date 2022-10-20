namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Requests Unify disassociate the SaleID and POIID. Results in a <see cref="LogoutResponse"/>
    /// </summary>
    public class LogoutRequest: MessagePayload
    {
        public LogoutRequest() :
            base(MessageClass.Service, MessageCategory.Logout, MessageType.Request)
        {
        }

        public override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            return new LogoutResponse
            {
                Response = response ?? new Response()
                {
                    Result = Result.Failure,
                    ErrorCondition = ErrorCondition.Aborted,
                    AdditionalResponse = ""
                }
            };
        }

        public bool? MaintenanceAllowed { get; set; }
    }
}
