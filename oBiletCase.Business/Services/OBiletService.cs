using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using oBiletCase.Business.Interfaces;
using oBiletCase.Business.Models.RequestDtos;
using oBiletCase.Business.Models.ResponseDtos;
using oBiletCase.Business.Utilities.Configurations;
using oBiletCase.Business.Utilities.Extensions;
using oBiletCase.Core.App;
using oBiletCase.Core.Extensions;
using oBiletCase.Core.Http;
using oBiletCase.Core.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oBiletCase.Business.Services
{
    public class OBiletService : IOBiletService
    {
        private readonly ILogger<OBiletService> _logger;
        private readonly IHttpHelper _http;
        private readonly OBiletApiConfiguration _apiConfiguration;

        public OBiletService(ILogger<OBiletService> logger, IHttpHelper http, OBiletApiConfiguration apiConfiguration)
        {
            _logger = logger;
            _http = http;
            _apiConfiguration = apiConfiguration;
        }

        // Obilet Business Api için Session değerini verir.
        public async Task<DeviceSession> GetSession()
        {
            // Kullanıcının Cookie değerinde önceden kaydı varsa alınır.
            var deviceSessionCache = CookieHelper.Get<DeviceSession>("deviceSession");

            // Client'ın kayıtlı session'ı varsa bu değer doğrudan dönülür.
            if (deviceSessionCache is not null) return deviceSessionCache;

            // Request için browserName ve borwserVersion değerleri Request'ten alınır.
            var (BrowserName, BrowserVersion) = AppHttpContext.Current.Request.GetBrowserInfo();

            // IP, browserName ve borwserVersion değerleri ile request model oluşturulur.
            var requestModel = new GetSessionRequestDto();
            requestModel.Connection.IpAddress = AppHttpContext.Current.Request.GetClientIp();
            requestModel.Browser.Name = BrowserName;
            requestModel.Browser.Version = BrowserVersion;

            try
            {
                //Obilet business api'ye istek yapılır.
                var response = await _http.GetSession(requestModel, _apiConfiguration);

                // Alınan cevaptaki session bilgisi model'e set edilir.
                var deviceSession = new DeviceSession
                {
                    DeviceId = response.Data.DeviceId,
                    SessionId = response.Data.SessionId
                };

                // Sonraki istekler için bu değer Cookie'e kaydedilir.
                CookieHelper.Set("deviceSession", deviceSession);

                return deviceSession;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetSession Error");
                return null;
            }

        }


        public async Task<IEnumerable<BusLocationDto>> GetBusLocations(string searchText = null)
        {
            // Session bilgisi alınır.
            var session = await GetSession();

            // Session bilgisi ile Request Model oluşturulur.
            var requestModel = new ObiletApiRequestBase<string>
            {
                DeviceSession = new DeviceSession
                {
                    DeviceId = session.DeviceId,
                    SessionId = session.SessionId
                },
                Data = searchText // aranan şehir kelimesi
            };

            try
            {
                //Obilet business api'ye istek yapılır ve BusLocations listesi alınır.
                var response = await _http.GetBusLocations(requestModel, _apiConfiguration);

                // response status değeri kontrolü ile başarısız ise boş liste dönüyoruz ( hata yönetimi )
                if (response.Status != Enums.ResponseStatus.Success) return Enumerable.Empty<BusLocationDto>();

                // Gelen data ilgili Dto'ya set edilerek geri dönülür.
                return response.Data.Select(p => new BusLocationDto
                {
                    Id = p.Id,
                    Name = p.Name
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetBusLocations Error");
                return null;
            }
        }

        public async Task<IEnumerable<BusJourneyDto>> GetJourneys(GetBusJourneysDto getJourneysDto)
        {
            // Session bilgisi alınır.
            var session = await GetSession();

            // Session bilgisi ile Request Model oluşturulur.
            var requestModel = new ObiletApiRequestBase<GetBusJourneysRequestDataDto>
            {
                DeviceSession = new DeviceSession
                {
                    DeviceId = session.DeviceId,
                    SessionId = session.SessionId
                },
                Data = new GetBusJourneysRequestDataDto
                {
                    OriginId = getJourneysDto.OriginId, //nereden
                    DestinationId = getJourneysDto.DestinationId, //nereye
                    DepartureDate = getJourneysDto.DepartureDate //ne zaman
                }
            };
            try
            {
                //Obilet business api'ye istek yapılır ve BusJourneys listesi alınır.
                var response = await _http.GetBusJourneys(requestModel, _apiConfiguration);

                // response status değeri kontrolü ile başarısız ise boş liste dönüyoruz ( hata yönetimi )
                if (response.Status != Enums.ResponseStatus.Success || response.Data is null) return Enumerable.Empty<BusJourneyDto>();

                // Gelen data ilgili Dto'ya set edilerek geri dönülür.
                return response.Data.Select(p => new BusJourneyDto
                {
                    Id = p.Id,
                    Origin = p.Journey.Origin,
                    Destination = p.Journey.Destination,
                    Departure = p.Journey.Departure,
                    Arrival = p.Journey.Arrival,
                    Price = p.Journey?.InternetPrice.GetValueOrDefault(p.Journey.OriginalPrice.GetValueOrDefault(decimal.Zero)),
                });

            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetBusJourneys Error");
                return null;
            }
        }
    }
}
