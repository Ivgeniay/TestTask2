using System;
using System.Collections.Generic;

namespace BarrelHide.Services
{
    internal class ServiceProvider
    {
        private Dictionary<Type, IService> services = new();
         
        public ServiceProvider RegisterService(IService service)
        {
            Type type = service.GetType();
            if (services.TryGetValue(type, out IService serv))
            {
                throw new Exception($"There is alredy exist service type ({type})");
            }

            services[type] = service;
            return this;
        }

        public T GetService<T>()
        {
            Type type = typeof(T);
            if (services.TryGetValue(type, out IService service))
                return (T)service;

            throw new Exception($"There is no service {type} in the {this.GetType().Name}");
        }

        internal void Initialize()
        {
            foreach (IService item in services.Values)
                item.Initialize();
        }
    }
}
