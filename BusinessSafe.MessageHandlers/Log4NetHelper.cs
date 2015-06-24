using System.Reflection;
using log4net;

namespace BusinessSafe.MessageHandlers
{
    public static class Log4NetHelper
    {
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static Log4NetHelper()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}