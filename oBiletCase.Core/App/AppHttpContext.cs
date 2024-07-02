using oBiletCase.Core.Extensions;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace oBiletCase.Core.App
{
    public static class AppHttpContext
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static HttpContext Current => _httpContextAccessor.HttpContext;

        //Request'ten header değeri almak için oluşturuldu.
        public static bool GetHeader(string key, out string value)
        {
            return Current.GetHeader(key, out value);
        }

        //Request'ten UserAgent değeri almak için oluşturuldu.
        public static string GetUserAgent(HttpRequest? request = null)
        {
            request ??= Current.Request;
            return request.Headers["User-Agent"];
        }

        public static (string BrowserName, string BrowserVersion) GetBrowserInfo(this HttpRequest request)
        {
            //UserAgent değeri alınır.
            var userAgent = GetUserAgent(request);

            var browserName = "";
            var browserVersion = "";

            // UserAgent string'i içinde ilgili tarayıcı ismi geçiyor mu?
            if (userAgent.Contains("Chrome"))
            {
                // BrowserName set edilir.
                browserName = "Chrome";
                //Regex ile tarayıcı adı ile başlayan kelimeden tarayıcı versiyonu tespit edilir ve browserVersion set edilir.
                var versionMatch = Regex.Match(userAgent, @"Chrome\/(\d+(\.\d+)+)");
                if (versionMatch.Success)
                {
                    browserVersion = versionMatch.Groups[1].Value;
                }
            }
            else if (userAgent.Contains("Firefox"))
            {
                browserName = "Firefox";
                var versionMatch = Regex.Match(userAgent, @"Firefox\/(\d+(\.\d+)+)");
                if (versionMatch.Success)
                {
                    browserVersion = versionMatch.Groups[1].Value;
                }
            }
            else if (userAgent.Contains("Edge"))
            {
                browserName = "Microsoft Edge";
                var versionMatch = Regex.Match(userAgent, @"Edge\/(\d+(\.\d+)+)");
                if (versionMatch.Success)
                {
                    browserVersion = versionMatch.Groups[1].Value;
                }
            }
            else if (userAgent.Contains("Safari"))
            {
                browserName = "Safari";
                var versionMatch = Regex.Match(userAgent, @"Version\/(\d+(\.\d+)+)");
                if (versionMatch.Success)
                {
                    browserVersion = versionMatch.Groups[1].Value;
                }
            }
            else if (userAgent.Contains("Opera"))
            {
                browserName = "Opera";
                var versionMatch = Regex.Match(userAgent, @"Opera\/(\d+(\.\d+)+)");
                if (versionMatch.Success)
                {
                    browserVersion = versionMatch.Groups[1].Value;
                }
            }
            else if (userAgent.Contains("Vivaldi"))
            {
                browserName = "Vivaldi";
                var versionMatch = Regex.Match(userAgent, @"Vivaldi\/(\d+(\.\d+)+)");
                if (versionMatch.Success)
                {
                    browserVersion = versionMatch.Groups[1].Value;
                }
            }
            else
            {
                browserName = "Unknown";
                browserVersion = "0.0.0.0";
            }

            return (browserName, browserVersion);
        }

    }
}
