
namespace BusinessSafe.Infrastructure.Security
{
    public interface IEncryptor
    {
        string Encrypt(string plainVersion);
        string Decrypt(string encryptedVersion);
    }
}
