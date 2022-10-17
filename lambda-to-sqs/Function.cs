using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace lambda_to_sqs
{
    public class Function
    {
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string FunctionHandler(string input, ILambdaContext context)
        {
            
            var credential = new BasicAWSCredentials("AKIATQA5AYNZU4HY3PTV", "jvAWZlfV1BtYrYGQMyspJgrbypyY7svNZHmgkCcU");
            var client = new AmazonSQSClient(credentials: credential, region:Amazon.RegionEndpoint.SAEast1);
            var request = new SendMessageRequest() {

                QueueUrl = "https://sqs.sa-east-1.amazonaws.com/240579036019/TesteEnvioFila",
                MessageBody = JsonSerializer.Serialize(input)
            };
            client.SendMessageAsync(request);

            return $" {input?.ToUpper()} - {client} ";
        }
    }
}
