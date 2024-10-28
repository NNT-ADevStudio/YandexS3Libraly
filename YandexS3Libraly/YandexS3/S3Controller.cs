using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;
using System.Threading.Tasks;

namespace YandexS3Libraly.YandexS3
{
    public class S3Controller
    {
        private const string EndpointUrl = "https://storage.yandexcloud.net";

        private string AccessKey { get; }
        private string SecretKey { get; }
        private string BucketName { get; }

        private readonly static AmazonS3Config config = new AmazonS3Config
        {
            ServiceURL = EndpointUrl,
            ForcePathStyle = true,
            SignatureVersion = "4",
            SignatureMethod = SigningAlgorithm.HmacSHA256
        };

        public S3Controller(string accessKey, string secretKey, string bucketName)
        {
            AccessKey = accessKey;
            SecretKey = secretKey;
            BucketName = bucketName;
        }

        public async Task<bool> PostImage(string filePath)
        {
            try
            {
                using (var client = new AmazonS3Client(AccessKey, SecretKey, config))
                {
                    PutObjectRequest request = new PutObjectRequest
                    {
                        BucketName = BucketName,
                        FilePath = filePath,
                        Key = Path.GetFileName(filePath)
                    };

                    PutObjectResponse response = await client.PutObjectAsync(request).ConfigureAwait(false);
                }

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> PostImage(string filePath, string fileName)
        {
            try
            {
                using (var client = new AmazonS3Client(AccessKey, SecretKey, config))
                {
                    PutObjectRequest request = new PutObjectRequest
                    {
                        BucketName = BucketName,
                        FilePath = filePath,
                        Key = fileName
                    };

                    PutObjectResponse response = await client.PutObjectAsync(request).ConfigureAwait(false);
                }

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> PostImage(string filePath, string fileName, string folderPathInBucket)
        {
            try
            {
                using (var client = new AmazonS3Client(AccessKey, SecretKey, config))
                {
                    PutObjectRequest request = new PutObjectRequest
                    {
                        BucketName = BucketName,
                        FilePath = filePath,
                        Key = $"{folderPathInBucket}/{fileName}"
                    };

                    PutObjectResponse response = await client.PutObjectAsync(request).ConfigureAwait(false);
                }
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
