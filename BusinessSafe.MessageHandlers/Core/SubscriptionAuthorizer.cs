using System.Collections.Generic;
using NServiceBus;

namespace BusinessSafe.MessageHandlers.Core
{
    public class SubscriptionAuthorizer : IAuthorizeSubscriptions
    {
        public bool AuthorizeSubscribe(string messageType, string clientEndpoint, IDictionary<string, string> headers)
        {
            Log4NetHelper.Log.Info(
                string.Format("SubscriptionAuthorizer AuthorizeSubscribe. Message Type : {0}. ClientEndpoint : {1}",
                              messageType, clientEndpoint));
            return true;
        }

        public bool AuthorizeUnsubscribe(string messageType, string clientEndpoint, IDictionary<string, string> headers)
        {
            Log4NetHelper.Log.Info(
                string.Format("SubscriptionAuthorizer AuthorizeUnsubscribe. Message Type : {0}. ClientEndpoint : {1}",
                              messageType, clientEndpoint));
            return true;
        }
    }
}