using oBiletCase.Core.App;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace oBiletCase.Core.Store
{
    //Session yönetimi için oluşturulan yardımcı static class.
    //Get ve Set işlemlerini içerir.
    public static class SessionHelper
    {
        public static string GetString(string key)
        {
            var hasValue = AppHttpContext.Current.Session.TryGetValue(key, out var value);
            return hasValue ? Encoding.ASCII.GetString(value) : string.Empty;
        }
        public static void SetString(string key, string value)
        {
            AppHttpContext.Current.Session.Set(key, Encoding.ASCII.GetBytes(value));
            SaveChanges();
        }

        public static void Remove(string key)
        {
            AppHttpContext.Current.Session.Remove(key);
            SaveChanges();
        }

        public static void Clear()
        {
            AppHttpContext.Current.Session.Clear();
            SaveChanges();
        }

        private static void SaveChanges()
        {
            AppHttpContext.Current.Session.CommitAsync().GetAwaiter().GetResult();
        }
    }
}
