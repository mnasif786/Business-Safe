using log4net;

namespace BusinessSafe.WebSite.Helpers
{
    public static class Log4NetHelper
    {
        public static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static Log4NetHelper()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}