using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using oBiletCase.Business.Interfaces;
using oBiletCase.Business.Services;
using oBiletCase.Business.Utilities.Configurations;
using oBiletCase.Core.Http;
using Polly;
using Polly.Extensions.Http;
using System.Net.Http.Headers;

namespace oBiletCase.Business.Utilities.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
       public static IServiceCollection AddDIServices(this IServiceCollection services)
        {
            //HttpClient added
            services.AddHttpClient();

            // Http istekleri için kullanılan HttpHelper yapısı Singleton olarak DI'a eklenir.
            services.AddSingleton<IHttpHelper, HttpHelper>();

            // ObiletService DI'a eklenir.
            services.AddScoped<IOBiletService, OBiletService>();

            return services;
        }

        public static IServiceCollection AddOBiletApi(this IServiceCollection services, IConfiguration config)
        {
            var configuration = new OBiletApiConfiguration();

            config.Bind(nameof(OBiletApiConfiguration), configuration);
            services.AddSingleton(configuration);

            services.AddHttpClient(name: "oBiletApi", client =>
            {
                client.BaseAddress = new Uri(configuration.BaseUrl); // BaseUrl set edilir.
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", configuration.ApiClientToken); //Header'a token bilgisi eklenir.
            }).AddPolicyHandler(GetRetryPolicy()); // Polly handler ile policy eklenir.

            return services;
        }

        // Polly için Policy döner. 
        // Eğer hata alınmışsa 2 kere deneme yapar
        // her deneme arasında deneme index'inin 2 ye göre katını alarak bekleme süresi ayarlar
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError() // Http 5xx hataları
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound) // Http 404 hatası
                .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
