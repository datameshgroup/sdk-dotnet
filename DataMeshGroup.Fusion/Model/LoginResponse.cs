namespace DataMeshGroup.Fusion.Model
{
    public class LoginResponse : MessagePayload
    {
        public LoginResponse() : base(MessageClass.Service, MessageCategory.Login, MessageType.Response) 
        { 
        }
        public Response Response { get; set; }
        public POISystemData POISystemData { get; set; }

        internal override MessagePayload CreateDefaultResponseMessagePayload(Response response)
        {
            throw new System.NotImplementedException();
        }
    }
}
