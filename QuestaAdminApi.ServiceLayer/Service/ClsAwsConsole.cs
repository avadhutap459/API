using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using QuestaAdminApi.DatabaseLayer;

namespace QuestaAdminApi.ServiceLayer.Service
{
    public class ClsAwsConsole : IDisposable, IAwsConsole
    {
        ClsDbConnection Connectionmgr;
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        private bool isDisposed = false;
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }

        public ClsAwsConsole()
        {
            Connectionmgr = ClsDbConnection.Instance;
        }

        ~ClsAwsConsole()
        {
            Dispose(false);
        }


        public byte[] DownloadFileFromAwsS3Bucket(string bucketName, string SubbucketName, string FileName)
        {
            try
            {
                byte[] ManagingSelfImgByte = null;

                if(CheckFileExitsOnAwsS3BucketAsync(bucketName, SubbucketName, FileName))
                {
                    string FullbucketPath = bucketName + @"/" + SubbucketName;

                    AmazonS3Client s3 = new AmazonS3Client(new BasicAWSCredentials(AccessKey, SecretKey), Amazon.RegionEndpoint.APSouth1);
                    // GetObjectRequest getObjectRequest = new GetObjectRequest();
                    //getObjectRequest.BucketName = FullbucketPath;//"h1modulewisescorecard";
                    GetObjectResponse response = new GetObjectResponse();
                    GetObjectRequest request = new GetObjectRequest
                    {
                        BucketName = bucketName,
                        Key = SubbucketName + "/" + FileName
                    };

                    // getObjectRequest.Key = FileName;//UserModel.UserTestId + "_14_5.png";
                    response = s3.GetObjectAsync(request).Result;
                    MemoryStream memoryStream = new MemoryStream();
                    using (Stream responseStream = response.ResponseStream)
                    {
                        responseStream.CopyTo(memoryStream);
                    }
                    ManagingSelfImgByte = memoryStream.ToArray();
                    memoryStream.Close();
                }

                

                return ManagingSelfImgByte;
            }
            catch (AmazonS3Exception s3Exception)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private bool CheckFileExitsOnAwsS3BucketAsync(string bucketName, string SubbucketName, string FileName)
        {
            try
            {
                string FullbucketPath = bucketName + @"/" + SubbucketName;
                using (AmazonS3Client client = new AmazonS3Client(new BasicAWSCredentials(AccessKey, SecretKey), Amazon.RegionEndpoint.APSouth1))
                {

                    var listS3Objects = client.ListObjectsV2Async(new ListObjectsV2Request
                    {
                        BucketName = bucketName,
                        Prefix = SubbucketName + "/" + FileName, // eg myfolder/myimage.jpg (no / at start)
                        MaxKeys = 1
                    }).Result;

                    if (listS3Objects.S3Objects.Any() == false || listS3Objects.S3Objects.All(x => x.Key != SubbucketName + "/" + FileName))
                    {
                        // S3 object doesn't exist
                        return false;
                    }

                    // S3 object exists
                    return true;

                }
            }
            catch (AmazonS3Exception exception)
            {
                if (exception.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        #region Dispose


        protected void Dispose(bool disposing)
        {
            if (disposing)
            {

                // Code to dispose the managed resources of the class
            }
            // Code to dispose the un-managed resources of the class
            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
