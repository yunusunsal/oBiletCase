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
        //Kullan�c�n�n kay�tl� son arama verileri cookie'den al�n�yor.
        var searchData = CookieHelper.Get<BusSearchModel>("SearchData");

        // oBilet Business Api'den BusLocations verisi al�n�yor.
        var busLocations = await _oBiletService.GetBusLocations();
        // BusLocations verisi yoksa bo� liste d�nerek sayfan�n a��lmas� sa�lan�yor.
        //ViewBag.BusLocations = busLocations ?? new List<BusLocationDto>();

        if (searchData is null)
        {
                // Default "Nereden" - "Nereye" bilgisi i�in BusLocations datas�n�n ilk iki eleman� al�n�yor.
                var defaultDepertureCity = busLocations?.FirstOrDefault();
                var defaultDestinationCity = busLocations?.Skip(1).FirstOrDefault();
                
                // DepartureDate "Yar�n" olacak �ekilde veri modelimiz set ediliyor.
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


    /* GET Methodu BusSearch-Index'teki �ehir se�imlerindeki arama i�levi i�in olu�turuldu.
       Query'den searchText parametresi ile oBilet Business Api ye istek yap�larak sonu�
       Json olarak istenilen formatta geri d�n�l�yor.*/
    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] string searchText)
    {
        var busLocations = await _oBiletService.GetBusLocations(searchText);

        if(busLocations is null) return NotFound();

        return Json(busLocations);
    }

    /* POST Methodu BusSearch-Index'teki se�imler yap�ld�ktan sonra BusJourney aramas� yaparak 
       sonu� ile birlikte ilgili sayfay� y�kler. */
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