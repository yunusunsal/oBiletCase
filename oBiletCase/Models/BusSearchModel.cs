
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using oBiletCase.Business.Models.ResponseDtos;
using oBiletCase.Business.Utilities.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace oBiletCase.Models
{
    // BusSearch-Index'teki form i�in gerekli Model.
    // Nereden-Nereye-NeZaman bilgilerini Controller-View aras�nda aktarmak i�in olu�turuldu
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

    // BusSearch-Results View'�na veri aktarmak i�in olu�turulan model
    // Nereden-Nereye-NeZaman bilgileri ve bulunan sefer listesini i�erir.
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
