using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using lambda_to_sqs.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace lambda_to_sqs.services
{
    public class ReceiverService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Lendo as mensagens");
            while (!stoppingToken.IsCancellationRequested)
            {
                var request = new ReceiveMessageRequest()
                {
                    QueueUrl = Acceess.GetRoute(),
                    WaitTimeSeconds = 50,
                    VisibilityTimeout = 10
                };
                var response = await Acceess.GetCredentials().ReceiveMessageAsync(request);
                foreach (var message in response.Messages)
                {
                    Console.WriteLine(message);
                    if (message.Body.Contains("Exception")) continue;
                    await Acceess.GetCredentials().DeleteMessageAsync(Acceess.GetRoute(), message.ReceiptHandle);
                }
            }
        }
    }
}
