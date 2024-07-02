using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oBiletCase.Business.Models.RequestDtos
{
    public class GetBusJourneysRequestDataDto : GetBusJourneysDto
    {
        [JsonProperty("origin-id")]
        public override int OriginId { get; set; }

        [JsonProperty("destination-id")]
        public override int DestinationId { get; set; }

        [JsonProperty("departure-date")]
        public override DateTime DepartureDate { get; set; }
    }
}
