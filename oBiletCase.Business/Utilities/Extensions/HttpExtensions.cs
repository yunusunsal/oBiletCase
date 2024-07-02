using oBiletCase.Business.Models.RequestDtos;
using oBiletCase.Business.Models.ResponseDtos;
using oBiletCase.Business.Utilities.Configurations;
using oBiletCase.Core.Http;

namespace oBiletCase.Business.Utilities.Extensions
{
    // Obilet Business Api için oluşturulan static HttpClient Extension'ları.
    public static class HttpExtensions
    {

        private static readonly string _oBiletApiClientName = "oBiletApi"; // DI'a verilen ClientName bilgisi

        public static async Task<GetSessionResponseDto> GetSession(this IHttpHelper http, GetSessionRequestDto requestModel, OBiletApiConfiguration configuration)
        {
            string url = $"{configuration.BaseUrl}/client/getsession";

            return await http.ConnectPost<GetSessionRequestDto, GetSessionResponseDto>(requestModel, _oBiletApiClientName, url, camelCaseJson: false);
        }


        public static async Task<GetBusLocationsResponseDto> GetBusLocations(this IHttpHelper http, ObiletApiRequestBase<string> requestModel, OBiletApiConfiguration configuration)
        {
            string url = $"{configuration.BaseUrl}/location/getbuslocations";

            return await http.ConnectPost<ObiletApiRequestBase<string>, GetBusLocationsResponseDto>(requestModel, _oBiletApiClientName, url, camelCaseJson: false);
        }

        public static async Task<GetBusJourneysResponseDto> GetBusJourneys(this IHttpHelper http, ObiletApiRequestBase<GetBusJourneysRequestDataDto> requestModel, OBiletApiConfiguration configuration)
        {
            string url = $"{configuration.BaseUrl}/journey/getbusjourneys";

            return await http.ConnectPost<ObiletApiRequestBase<GetBusJourneysRequestDataDto>, GetBusJourneysResponseDto>(requestModel, _oBiletApiClientName, url, camelCaseJson: false);
        }


    }
}
