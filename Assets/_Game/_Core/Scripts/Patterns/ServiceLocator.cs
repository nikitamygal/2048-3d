
using System;
using System.Collections.Generic;

namespace SoloGames.Patterns
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new();

        public static void Register(object service)
        {
            var type = service.GetType();
            _services[type] = service;
        }

        public static T Get<T>()
        {
            var type = typeof(T);
            if (_services.TryGetValue(type, out var service))
                return (T)service;

            throw new Exception($"Service {type} not found");
        }
    }
}
