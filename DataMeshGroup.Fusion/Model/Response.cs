using Newtonsoft.Json;

namespace DataMeshGroup.Fusion.Model
{
    public class Response
    {
        /// <summary>
        /// Construct a default Response. 
        /// Sets Result=Failure, ErrorCondition=cancel, AdditionalResponse=null
        /// </summary>
        public Response() : this(Result.Failure, ErrorCondition.Cancel, null)
        {
        }

        /// <summary>
        /// Construct a <see cref="Response"/> with the specified <see cref="Result"/>
        /// Default Result=Failure, ErrorCondition=cancel, AdditionalResponse=null
        /// </summary>
        public Response(Result result) : this(result, ErrorCondition.Cancel, null)
        {
        }

        /// <summary>
        /// Construct a <see cref="Response"/> with the specified <see cref="Result"/>, <see cref="ErrorCondition"/>
        /// </summary>
        public Response(ErrorCondition errorCondition, string additionalResponse = null) : this(Result.Failure, errorCondition, additionalResponse)
        {
        }

        /// <summary>
        /// Construct a <see cref="Response"/> with the specified <see cref="Result"/>, <see cref="ErrorCondition"/>, and <see cref="AdditionalResponse"/>
        /// </summary>
        public Response(Result result, ErrorCondition errorCondition, string additionalResponse)
        {
            this.Result = result;
            this.ErrorCondition = errorCondition;
            this.AdditionalResponse = additionalResponse;
        }

        /// <summary>
        /// Indicates the success of the response
        /// </summary>
        public Result Result { get; set; }

        /// <summary>
        /// Indicates the error condition behind a failed response. Present when <see cref="Result"/> is <see cref="Result.Failure"/>
        /// </summary>
        public ErrorCondition ErrorCondition { get; set; }

        /// <summary>
        /// Description of the error condition behind a failed response. Present when <see cref="Result"/> is <see cref="Result.Failure"/>
        /// </summary>
        public string AdditionalResponse { get; set; }

        /// <summary>
        /// Helper function. True when Result is <see cref="Result.Success"/> or <see cref="Result.Partial"/>
        /// </summary>
        [JsonIgnore]
        public bool Success => (Result == Result.Success) || (Result == Result.Partial);

        /// <summary>
        /// Helper function to manage serializtion of ErrorCondition
        /// </summary>
        public bool ShouldSerializeErrorCondition()
        {
            return !Success;
        }
    }
}
