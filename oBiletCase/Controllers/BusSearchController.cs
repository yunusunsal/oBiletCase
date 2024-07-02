using Microsoft.AspNetCore.Mvc;
using oBiletCase.Models;
using oBiletCase.Business.Interfaces;
using oBiletCase.Business.Models.RequestDtos;
using oBiletCase.Core.App;
using oBiletCase.Core.Extensions;
using oBiletCase.Core.Store;
using System.Text.Json;
using System.Web;
using oBiletCase.Business.Models.ResponseDtos;
using Microsoft.Extensions.Logging;

public class BusSearchController : Controller
{
    private readonly ILogger<BusSearchController> _logger;
    private readonly IOBiletService _oBiletService;

    public BusSearchController(ILogger<BusSearchController> logger, IOBiletService oBiletService)
    {
        _logger = logger;
        _oBiletService = oBiletService;
    }

    public async Task<IActionResult> Index()
    {
        //Kullanýcýnýn kayýtlý son arama verileri cookie'den alýnýyor.
        var searchData = CookieHelper.Get<BusSearchModel>("SearchData");

        // oBilet Business Api'den BusLocations verisi alýnýyor.
        var busLocations = await _oBiletService.GetBusLocations();
        // BusLocations verisi yoksa boþ liste dönerek sayfanýn açýlmasý saðlanýyor.
        //ViewBag.BusLocations = busLocations ?? new List<BusLocationDto>();

        if (searchData is null)
        {
                // Default "Nereden" - "Nereye" bilgisi için BusLocations datasýnýn ilk iki elemaný alýnýyor.
                var defaultDepertureCity = busLocations?.FirstOrDefault();
                var defaultDestinationCity = busLocations?.Skip(1).FirstOrDefault();
                
                // DepartureDate "Yarýn" olacak þekilde veri modelimiz set ediliyor.
                searchData = new()
                {
                    DepartureCity = defaultDepertureCity?.Name,
                    DepartureCityId = defaultDepertureCity?.Id,
                    DestinationCity = defaultDestinationCity?.Name,
                    DestinationCityId = defaultDestinationCity?.Id,
                    DepartureDate = DateTime.Now.AddDays(1),
                };

        }

        return View(searchData);
    }


    /* GET Methodu BusSearch-Index'teki þehir seçimlerindeki arama iþlevi için oluþturuldu.
       Query'den searchText parametresi ile oBilet Business Api ye istek yapýlarak sonuç
       Json olarak istenilen formatta geri dönülüyor.*/
    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] string searchText)
    {
        var busLocations = await _oBiletService.GetBusLocations(searchText);

        if(busLocations is null) return NotFound();

        return Json(busLocations);
    }

    /* POST Methodu BusSearch-Index'teki seçimler yapýldýktan sonra BusJourney aramasý yaparak 
       sonuç ile birlikte ilgili sayfayý yükler. */
    [HttpPost]
    public async Task<IActionResult> Results(BusSearchModel model)
    {
        //Store SearchData on Cookie (Client-Side)
        CookieHelper.Set("SearchData", model);

        var busResults = await _oBiletService.GetJourneys(new GetBusJourneysDto
        {
            OriginId = model.DepartureCityId.GetValueOrDefault(0),
            DestinationId = model.DestinationCityId.GetValueOrDefault(0),
            DepartureDate = model.DepartureDate
        });

        var busSearchResult = new BusSearchResult
        {
            DepartureCity = model.DepartureCity,
            DestinationCity = model.DestinationCity,
            DepartureDate = model.DepartureDate.ToString("D"),
            DepartureDateString = model.DepartureDate.ToString("yyyy-MM-dd"),
            DepartureCityId = model.DepartureCityId.GetValueOrDefault(0),
            DestinationCityId = model.DestinationCityId.GetValueOrDefault(0),
            BusJourneys = busResults.OrderBy(p => p.Departure)
        };

        return View(busSearchResult);
    }
}