using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace oBiletCase.Core.Http
{
    public static class RequestModelExtensions
    {
        public static string ConvertToCamelCaseJson(this IRequestModel model)
        {
            var jsonData = JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            return jsonData;
        }
    }
}
