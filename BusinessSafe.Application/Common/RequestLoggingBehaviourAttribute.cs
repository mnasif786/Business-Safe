using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

using BusinessSafe.Infrastructure.Attributes;

namespace BusinessSafe.Application.Common
{
    /// <remarks>
    ///   Not yet complete. This would be good, ibecause it would avoid the Log.Add() at the beggining of each operation. Maybe comine it with error handing behaviour, and perhaps log the return value?
    /// </remarks>
    [CoverageExclude]
    public class RequestLoggingBehaviourAttribute : Attribute, IServiceBehavior
    {
        public void AddBindingParameters(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase)
        {
            foreach (var chanDispBase in serviceHostBase.ChannelDispatchers)
            {
                var channelDispatcher = chanDispBase as ChannelDispatcher;
                if (channelDispatcher == null)
                {
                    continue;
                }

                foreach (var endpointDispatcher in channelDispatcher.Endpoints)
                {
                    var inspector = new RequestLoggingDispatchMessageInspector();
                    endpointDispatcher.DispatchRuntime.MessageInspectors.Add(inspector);
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }
}