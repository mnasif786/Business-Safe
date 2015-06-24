using System;
using System.Collections.Generic;
using System.Web.Mvc;
using StructureMap;

namespace BusinessSafe.WebSite
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        private readonly IContainer _container;


        public StructureMapDependencyResolver(IContainer container)
        {
            _container = container;
        }


        public object GetService(Type serviceType)
        {
            var instance = _container.TryGetInstance(serviceType);
            if (instance == null && !serviceType.IsAbstract)
            {
                instance = AddTypeAndTryGetInstance(serviceType);
            }
            return instance;
        }

        private object AddTypeAndTryGetInstance(Type serviceType)
        {
            _container.Configure(c => c.AddType(serviceType, serviceType));
            return _container.TryGetInstance(serviceType);
        }


        public IEnumerable<object> GetServices(Type serviceType)
        {
            foreach (Object instance in _container.GetAllInstances(serviceType))
            {
                yield return instance;
            }
        }
    }
}