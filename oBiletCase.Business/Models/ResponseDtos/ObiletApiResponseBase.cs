using Newtonsoft.Json;
using oBiletCase.Business.Enums;
using oBiletCase.Core.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oBiletCase.Business.Models.RequestDtos
{
    public class ObiletApiResponseBase<T> : ResponseBaseModel
    {

        [JsonProperty("status")]
        public ResponseStatus Status { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; } = "en-EN";

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("user-message")]
        public string UserMessage { get; set; }

        [JsonProperty("apirequest-id")]
        public string ApiRequestId { get; set; }

        [JsonProperty("controller")]
        public string Controller { get; set; }
    }

}
