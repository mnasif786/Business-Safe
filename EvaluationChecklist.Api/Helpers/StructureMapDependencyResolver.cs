using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using StructureMap;

namespace EvaluationChecklist.Helpers
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            var instance = ObjectFactory.TryGetInstance(serviceType);
            if (instance == null && !serviceType.IsAbstract)
            {
                instance = AddTypeAndTryGetInstance(serviceType);
            }
            return instance;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return ObjectFactory.GetAllInstances(serviceType).Cast<object>();
        }

        private object AddTypeAndTryGetInstance(Type serviceType)
        {
            ObjectFactory.Configure(c => c.AddType(serviceType, serviceType));
            return ObjectFactory.TryGetInstance(serviceType);
        }

        public void Dispose()
        {
        }
    }

}