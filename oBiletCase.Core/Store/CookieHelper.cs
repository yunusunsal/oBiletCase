using oBiletCase.Core.App;
using Microsoft.AspNetCore.Http;
using System.Text;
using oBiletCase.Core.Cryptography;
using Newtonsoft.Json;

namespace oBiletCase.Core.Store
{
    //Cookie yönetimi için oluşturulan yardımcı static class.
    //Get ve Set işlemlerini içerir.
    public static class CookieHelper
    {
        public static string GetString(string key)
        {
            var hasValue = AppHttpContext.Current.Request.Cookies.TryGetValue(key, out var value);
            return hasValue ? Encryption64.Decrypt(value) : string.Empty;
        }

        public static T Get<T>(string key) where T : class
        {
            try
            {
                var hasValue = AppHttpContext.Current.Request.Cookies.TryGetValue(key, out var value);
                return hasValue ? JsonConvert.DeserializeObject<T>(Encryption64.Decrypt(value)) : null;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public static void SetString(string key, string value, CookieOptions options = null)
        {
            if (options == null)
            {
                options = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(30)
                };
            }
            AppHttpContext.Current.Response.Cookies.Append(key, Encryption64.Encrypt(value), options);
        }

        public static void Set<T>(string key, T value, CookieOptions options = null) where T : class, new()
        {
            try
            {
                if (options == null)
                {
                    options = new CookieOptions
                    {
                        Expires = DateTime.Now.AddMinutes(30)
                    };
                }

                var dataString = JsonConvert.SerializeObject(value);

                AppHttpContext.Current.Response.Cookies.Append(key, Encryption64.Encrypt(dataString), options);
            }
            catch (Exception)
            { }

        }

        public static void Remove(string key)
        {
            AppHttpContext.Current.Response.Cookies.Delete(key);
        }
    }
}
