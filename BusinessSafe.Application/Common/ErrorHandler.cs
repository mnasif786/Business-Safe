using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Infrastructure.Attributes;

namespace BusinessSafe.Application.Common
{
    /// <summary>
    ///   Logs the error and throws a corresponding application fault exception. Ensures session is closed.
    /// </summary>
    [LogIgnore]
    [CoverageExclude]
    public class ErrorHandler : IErrorHandler
    {
        #region IErrorHandler Members

        public bool HandleError(Exception error)
        {
            //try
            //{
            //    SessionProvider.CloseSession();
            //}
            //catch { }

            return false;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (error.GetType() != typeof (ValidationFaultException))
            {
                string errorCode = Log.Add(error);
                FaultException faultException = new FaultException<ApplicationFault>(new ApplicationFault(errorCode),
                    "An application error occured with error code: " + errorCode);
                MessageFault messageFault = faultException.CreateMessageFault();
                fault = Message.CreateMessage(version, messageFault, faultException.Action);
            }
        }

        #endregion
    }
}