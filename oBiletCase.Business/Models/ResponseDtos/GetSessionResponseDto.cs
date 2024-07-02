using Newtonsoft.Json;
using oBiletCase.Core.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oBiletCase.Business.Models.ResponseDtos
{
    public class GetSessionResponseDto : ResponseBaseModel
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public GetSessionData Data { get; set; } = new();

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("user-message")]
        public string UserMessage { get; set; }

        [JsonProperty("api-request-id")]
        public string ApirequestId { get; set; }

        [JsonProperty("controller")]
        public string Controller { get; set; }

        [JsonProperty("client-request-id")]
        public string ClientRequestId { get; set; }

        [JsonProperty("web-correlation-id")]
        public string WebCorrelationId { get; set; }

        [JsonProperty("correlation-id")]
        public string CorrelationId { get; set; }
    }

    public class GetSessionData
    {
        [JsonProperty("session-id")]
        public string SessionId { get; set; }

        [JsonProperty("device-id")]
        public string DeviceId { get; set; }

        [JsonProperty("affiliate")]
        public string Affiliate { get; set; }

        [JsonProperty("device-type")]
        public int DeviceType { get; set; }

        [JsonProperty("device")]
        public string Device { get; set; }

        [JsonProperty("ip-country")]
        public string IpCountry { get; set; }
    }



}
