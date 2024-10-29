

namespace QuestaAdminApi.ServiceLayer
{
    public interface IAesOperation
    {
        string EncryptString(string key, string plainText);
        string DecryptString(string key, string cipherText);
        void Dispose();
    }
}
