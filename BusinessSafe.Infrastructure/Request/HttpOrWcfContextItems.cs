using System;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.Web;

using BusinessSafe.Infrastructure.Attributes;

namespace BusinessSafe.Infrastructure.Request
{
    [CoverageExclude]
    public class HttpOrWcfContextItems : IContextItems
    {
        #region IContextItems Members

        public object Get(string key)
        {
            if (OperationContext.Current != null)
            {
                return CallContext.GetData(key);
            }
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Items[key];
            }
            throw new Exception("No WCF or Http request context available.");
        }

        public void Set(string key, object data)
        {
            if (OperationContext.Current != null)
            {
                CallContext.SetData(key, data);
            }
            else if (HttpContext.Current != null)
            {
                HttpContext.Current.Items[key] = data;
            }
            else
            {
                throw new Exception("No WCF or Http request context available.");
            }
        }

        public void Remove(string key)
        {
            if (OperationContext.Current != null)
            {
                CallContext.SetData(key, null);
            }
            else if (HttpContext.Current != null)
            {
                HttpContext.Current.Items.Remove(key);
            }
            else
            {
                throw new Exception("No WCF or Http request context available.");
            }
        }

        #endregion
    }
}