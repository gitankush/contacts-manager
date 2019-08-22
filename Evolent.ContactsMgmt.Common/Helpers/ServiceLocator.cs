using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolent.ContactsMgmt.Common.Helpers
{
    public static class ServiceLocator
    {
        private static Dictionary<Type, object> services = new Dictionary<Type, object>();

        public static void Register<TInterface>(TInterface implementation)
        {
            //register service
            services.Add(typeof(TInterface), implementation);
        }

        public static TInterface Resolve<TInterface>()
        {
            if (services.ContainsKey(typeof(TInterface)))
            {
                return (TInterface)services[typeof(TInterface)];
            }
            else
            {
                return default(TInterface);
            }
        }

        public static void Unregister<TInterface>()
        {
            //unregister service
            services.Remove(typeof(TInterface));
        }

        public static void Clear()
        {
            services.Clear();
        }

        public static bool IsRegistered<TInterface>()
        {
            return services.ContainsKey(typeof(TInterface));
        }
    }
}
