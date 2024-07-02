using Newtonsoft.Json;
using oBiletCase.Business.Models.RequestDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oBiletCase.Business.Models.ResponseDtos
{
    public class GetBusLocationsResponseDto : ObiletApiResponseBase<List<BusLocationResponse>>
    {
    }

    public class BusLocationResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("parent-id")]
        public int ParentId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("geo-location")]
        public GeoLocation GeoLocation { get; set; }

        [JsonProperty("zoom")]
        public int Zoom { get; set; }

        [JsonProperty("tz-code")]
        public string TzCode { get; set; }

        [JsonProperty("weather-code")]
        public object WeatherCode { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("reference-code")]
        public string ReferenceCode { get; set; }

        [JsonProperty("city-id")]
        public int CityId { get; set; }

        [JsonProperty("reference-country")]
        public object ReferenceCountry { get; set; }

        [JsonProperty("country-id")]
        public int CountryId { get; set; }

        [JsonProperty("keywords")]
        public string Keywords { get; set; }

        [JsonProperty("city-name")]
        public string CityName { get; set; }

        [JsonProperty("languages")]
        public object Languages { get; set; }

        [JsonProperty("country-name")]
        public string CountryName { get; set; }

        [JsonProperty("code")]
        public object Code { get; set; }

        [JsonProperty("show-country")]
        public bool ShowCountry { get; set; }

        [JsonProperty("area-code")]
        public object AreaCode { get; set; }

        [JsonProperty("long-name")]
        public string LongName { get; set; }

        [JsonProperty("is-city-center")]
        public bool IsCityCenter { get; set; }
    }

     public class GeoLocation
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public int zoom { get; set; }
    }
}
