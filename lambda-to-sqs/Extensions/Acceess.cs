using Amazon.Runtime;
using Amazon.SQS;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace lambda_to_sqs.Extensions
{
    public static class Acceess
    {
        public static string GetRoute()
        {
            return GetSeetings().GetValue<string>("Queue");
        }
        public static IConfigurationRoot GetSeetings()
        {
            return new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsetttings.json")
               .Build();
        }

        public static AmazonSQSClient GetCredentials()
        {
            var accessKey = GetSeetings().GetValue<string>("AccessKey");
            var secret = GetSeetings().GetValue<string>("Secret");
            var credential = new BasicAWSCredentials(accessKey, secret);
            return new AmazonSQSClient(credentials: credential, region: Amazon.RegionEndpoint.SAEast1);
        }


    }
}
