using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CustomerPetshop.Extensions
{
    public static class SessionExtensions
    {
        
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var json = session.GetString(key);
            return json == null ? default : JsonConvert.DeserializeObject<T>(json);
        }

       
       public static void SetObjectAsJson(this ISession session, string key, object value)
            => SetObject(session, key, value);

        public static void SetObjectAsJson<T>(this ISession session, string key, T value)
          => SetObject(session, key, value);

        public static T GetObjectFromJson<T>(this ISession session, string key)
           => GetObject<T>(session, key);
    

    
        
    }
}
