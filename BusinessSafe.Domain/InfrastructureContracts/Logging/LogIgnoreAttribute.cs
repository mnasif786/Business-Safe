using System;

namespace BusinessSafe.Domain.InfrastructureContracts.Logging
{
    /// <summary>
    /// Adding this attribute to a class prevents it from being recorded in the stack trace of
    /// a log event.
    /// </summary>
    /// <remarks>
    /// Author  :   Paul Davies
    /// Date    :   26th December 2010
    /// </remarks>
    public class LogIgnoreAttribute : Attribute { }
}
