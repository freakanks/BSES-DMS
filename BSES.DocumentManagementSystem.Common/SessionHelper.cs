using Microsoft.AspNetCore.Http;
using System.Text;

namespace BSES.DocumentManagementSystem.Common
{
    public static class SessionHelper
    {
        public static void Add<T>(this ISession session, string key, T instance) where T : class =>
                    session.SetString(key, instance.Serialize());

        public static void Update<T>(this ISession session, string key, T instance) where T : class
        {
            string jsonDATA = instance.Serialize();
            if (session.Keys.Contains(key))
                session.Remove(key);
            session.SetString(key, jsonDATA);
        }
        public static void Remove<T>(this ISession session, string key) where T : class => session.Remove(key);

        public static T? Get<T>(this ISession session, string key) where T : class =>
            session.Keys.Contains(key) ?
            session.GetString(key).Deserialize<T>() :
            default;
    }
}
