using Newtonsoft.Json;
using oBiletCase.Core.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oBiletCase.Business.Models.RequestDtos
{
    public class ObiletApiRequestBase<T> : RequestBaseModel
    {
        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("device-session")]
        public DeviceSession DeviceSession { get; set; } = new();

        [JsonProperty("date")]
        public DateTime Date { get; set; } = DateTime.Now;

        [JsonProperty("language")]
        public string Language { get; set; } = "tr-TR";
    }

    public class DeviceSession
    {
        [JsonProperty("session-id")]
        public string SessionId { get; set; }

        [JsonProperty("device-id")]
        public string DeviceId { get; set; }
    }
}
