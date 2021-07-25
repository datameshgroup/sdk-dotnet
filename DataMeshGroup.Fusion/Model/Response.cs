using Newtonsoft.Json;

namespace DataMeshGroup.Fusion.Model
{
    public class Response
    {
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
