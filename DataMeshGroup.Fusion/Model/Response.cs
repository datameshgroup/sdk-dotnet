using Newtonsoft.Json;

namespace DataMeshGroup.Fusion.Model
{
    public class Response
    {
        public Response() : this(Result.Failure, ErrorCondition.Cancel, null)
        {
        }

        public Response(Result result) : this(result, ErrorCondition.Cancel, null)
        {
        }

        public Response(ErrorCondition errorCondition, string additionalResponse = null) : this(Result.Failure, errorCondition, additionalResponse)
        {
        }

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
    }
}
