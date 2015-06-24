using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using BusinessSafe.Infrastructure.Attributes;

namespace BusinessSafe.Application.Common
{
    [CoverageExclude]
    public class SessionClosingDispatchMessageInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            //SessionProvider.CloseSession();
        }
    }
}
