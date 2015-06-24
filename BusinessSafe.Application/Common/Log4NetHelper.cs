using log4net;

namespace BusinessSafe.Application.Common
{
    public static class Log4NetHelper
    {
        public static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);    
    }
}