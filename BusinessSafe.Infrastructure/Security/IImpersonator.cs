using System;

namespace BusinessSafe.Infrastructure.Security
{
    public interface IImpersonator : IDisposable
    {
        void ImpersonateValidUser(string userName, string domain, string encryptedPassword);
    }
}
