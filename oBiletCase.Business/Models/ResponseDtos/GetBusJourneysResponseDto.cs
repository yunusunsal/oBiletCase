using Newtonsoft.Json;
using oBiletCase.Business.Models.RequestDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oBiletCase.Business.Models.ResponseDtos
{
    public class GetBusJourneysResponseDto: ObiletApiResponseBase<List<GetBusJourneysData>>
    {

    }

    public class GetBusJourneysData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("partner-id")]
        public int? PartnerId { get; set; }

        [JsonProperty("partner-name")]
        public string PartnerName { get; set; }

        [JsonProperty("route-id")]
        public int? RouteId { get; set; }

        [JsonProperty("bus-type")]
        public string BusType { get; set; }

        [JsonProperty("bus-type-name")]
        public string BusTypeName { get; set; }

        [JsonProperty("total-seats")]
        public int? TotalSeats { get; set; }

        [JsonProperty("available-seats")]
        public int? AvailableSeats { get; set; }

        [JsonProperty("journey")]
        public Journey Journey { get; set; }

        [JsonProperty("features")]
        public List<Feature> Features { get; set; }

        [JsonProperty("origin-location")]
        public string OriginLocation { get; set; }

        [JsonProperty("destination-location")]
        public string DestinationLocation { get; set; }

        [JsonProperty("is-active")]
        public bool? IsActive { get; set; }

        [JsonProperty("origin-location-id")]
        public int? OriginLocationId { get; set; }

        [JsonProperty("destination-location-id")]
        public int? DestinationLocationId { get; set; }

        [JsonProperty("is-promoted")]
        public bool? IsPromoted { get; set; }

        [JsonProperty("cancellation-offset")]
        public int? CancellationOffset { get; set; }

        [JsonProperty("has-bus-shuttle")]
        public bool? HasBussHuttle { get; set; }

        [JsonProperty("disable-sales-without-gov-id")]
        public bool? DisableSalesWithoutGovId { get; set; }

        [JsonProperty("display-offset")]
        public object? DisplayOffset { get; set; }

        [JsonProperty("partner-rating")]
        public double? PartnerRating { get; set; }
    }

    public class Feature
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("priority")]
        public int? Priority { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public object Description { get; set; }

        [JsonProperty("is-promoted")]
        public bool? IsPromoted { get; set; }

        [JsonProperty("back-color")]
        public object BackColor { get; set; }

        [JsonProperty("fore-color")]
        public object ForeColor { get; set; }
    }

    public class Journey
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("stops")]
        public List<Stop> Stops { get; set; }

        [JsonProperty("origin")]
        public string Origin { get; set; }

        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("departure")]
        public DateTime Departure { get; set; }

        [JsonProperty("arrival")]
        public DateTime Arrival { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("original-price")]
        public decimal? OriginalPrice { get; set; }

        [JsonProperty("internet-price")]
        public decimal? InternetPrice { get; set; }

        [JsonProperty("provider-internet-price")]
        public decimal? ProviderInternetPrice { get; set; }

        [JsonProperty("booking")]
        public object? Booking { get; set; }

        [JsonProperty("bus-name")]
        public string BusName { get; set; }

        [JsonProperty("policy")]
        public Policy Policy { get; set; }

        [JsonProperty("features")]
        public List<string> Features { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("available")]
        public bool? Available { get; set; }
    }

    public class Policy
    {
        [JsonProperty("max-seats")]
        public int? MaxSeats { get; set; }

        [JsonProperty("max-single")]
        public int? MaxSingle { get; set; }

        [JsonProperty("max-single-males")]
        public int? MaxSingleMales { get; set; }

        [JsonProperty("max-single-females")]
        public int? MaxSingleFemales { get; set; }

        [JsonProperty("mixed-genders")]
        public bool? MixedGenders { get; set; }

        [JsonProperty("gov-id")]
        public bool? GovId { get; set; }

        [JsonProperty("lht")]
        public bool? Lht { get; set; }
    }

    

    public class Stop
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("station")]
        public string Station { get; set; }

        [JsonProperty("time")]
        public DateTime? Time { get; set; }

        [JsonProperty("is-origin")]
        public bool? IsOrigin { get; set; }

        [JsonProperty("is-destination")]
        public bool? IsDestination { get; set; }
    }

}
