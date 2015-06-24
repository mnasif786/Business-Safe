using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Infrastructure.Attributes;

namespace BusinessSafe.Application.Common
{
    [CoverageExclude]
    public class RequestLoggingDispatchMessageInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            string logCode = null;

            try
            {
                //Work out how to do something more useful here.
                logCode = Log.Add(request.Headers.Action);
            }
            catch (Exception exception)
            {
                Log.Add(exception.Message);
            }

            return logCode;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            //SessionProvider.CloseSession();
        }
    }
}
