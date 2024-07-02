using Newtonsoft.Json;
using oBiletCase.Core.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oBiletCase.Business.Models.RequestDtos
{
    public class GetSessionRequestDto : RequestBaseModel
    {
        [JsonProperty("type")]
        public int Type { get; set; } = 7;

        [JsonProperty("connection")]
        public Connection Connection { get; set; } = new();

        [JsonProperty("browser")]
        public Browser Browser { get; set; } = new();
    }


    public class Connection
    {
        [JsonProperty("ip-address")]
        public string IpAddress { get; set; } = "127.0.0.1";

        [JsonProperty("port")]
        public string Port { get; set; } = "443";
    }

    public class Browser
    {
        [JsonProperty("version")]
        public string Version { get; set; } = "1.0.0.0";

        [JsonProperty("name")]
        public string Name { get; set; } = "Chrome";
    }
}
