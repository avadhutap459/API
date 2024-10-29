

namespace QuestaAdminApi.ServiceLayer
{
    public interface IAwsConsole
    {
        byte[] DownloadFileFromAwsS3Bucket(string bucketName, string SubbucketName, string FileName);
        void Dispose();
    }
}
