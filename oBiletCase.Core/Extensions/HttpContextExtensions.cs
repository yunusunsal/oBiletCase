using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oBiletCase.Core.Extensions
{
    //HttpContext Extension'larını içerir.
    public static class HttpContextExtensions
    {
        private const string NullIpAddress = "::1";

        // Son kullanıcı ip adresini alır.
        public static string GetClientIp(this HttpRequest request)
        {
            var ipAddress = request.HttpContext?.Connection?.RemoteIpAddress;

            if (ipAddress != null)
            {
                if (ipAddress.ToString().Equals(NullIpAddress))
                    return "127.0.0.1";

                ipAddress = ipAddress.MapToIPv4();

                return ipAddress.ToString();
            }

            return "127.0.0.1";
        }

        // Request Scheme-Host-Path-Query bilgileri ile full Uri döner. 
        private static Uri GetAbsoluteUri(this HttpContext httpContext)
        {
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = httpContext.Request.Scheme;
            uriBuilder.Host = httpContext.Request.Host.ToString();
            uriBuilder.Path = httpContext.Request.Path.ToString();
            uriBuilder.Query = httpContext.Request.QueryString.ToString();
            return uriBuilder.Uri;
        }

        public static string GetAbsolutePath(this HttpContext httpContext)
        {
            return httpContext.GetAbsoluteUri().AbsolutePath;
        }

        // Reqeust'ten header değerini(varsa) alır.
        public static bool GetHeader(this HttpContext httpContext, string key, out string value)
        {
            StringValues headerValues;
            value = string.Empty;
            var hasValue = httpContext.Request.Headers.TryGetValue(key, out headerValues);
            if (hasValue)
            {
                try
                {
                    value = headerValues.Count > 0 ? headerValues[0].ToString().Trim() : string.Empty;
                }
                catch { }
            }

            return hasValue;
        }


    }
}
