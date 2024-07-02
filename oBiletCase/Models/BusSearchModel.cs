
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using oBiletCase.Business.Models.ResponseDtos;
using oBiletCase.Business.Utilities.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace oBiletCase.Models
{
    // BusSearch-Index'teki form için gerekli Model.
    // Nereden-Nereye-NeZaman bilgilerini Controller-View arasýnda aktarmak için oluþturuldu
    public class BusSearchModel
    {
        [Required]
        public string DepartureCity { get; set; }
        [Required]
        public string DestinationCity { get; set; }
        [Required]
        public int? DepartureCityId { get; set; }
        [Required]
        public int? DestinationCityId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [BindProperty]
        [ValidateDate]
        public DateTime DepartureDate { get; set; }
    }

    // BusSearch-Results View'ýna veri aktarmak için oluþturulan model
    // Nereden-Nereye-NeZaman bilgileri ve bulunan sefer listesini içerir.
    public class BusSearchResult
    {
        public string DepartureCity { get; set; }
        public string DestinationCity { get; set; }
        public string DepartureDate { get; set; }

        public int DepartureCityId { get; set; }
        public int DestinationCityId { get; set; }
        public string DepartureDateString { get; set; }

        public IEnumerable<BusJourneyDto> BusJourneys { get; set; }
    }
}
