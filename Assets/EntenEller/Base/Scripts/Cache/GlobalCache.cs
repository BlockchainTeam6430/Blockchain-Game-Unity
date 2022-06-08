using System.Collections.Generic;

namespace Plugins.EntenEller.Base.Scripts.Cache
{
    public static class GlobalCache
    {
        private static readonly Dictionary<object, object> cache = new Dictionary<object, object>();

        public static object Get(object key)
        {
            return cache.TryGetValue(key, out var obj) ? obj : null;
        }

        public static void Set(object key, object obj)
        {
            if (Get(key) == null) cache.Add(key, obj);
            else cache[key] = obj;
        }

        public static void CleanAll()
        {
            cache.Clear();
        }
    }
}
