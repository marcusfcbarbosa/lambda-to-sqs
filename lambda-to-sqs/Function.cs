

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
using Amazon.Lambda.Core;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using lambda_to_sqs.Extensions;
using lambda_to_sqs.services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace lambda_to_sqs
{
    public interface IBookingRepository
    {
        Task Adiciona();
    }
    public class BookingRepository : IBookingRepository
    {
        public async Task Adiciona()
        {
            var teste = "wqwree";
        }
    }
    public class Function
    {
        private static IServiceProvider services;
        private readonly IBookingRepository bookingRepository;

        public Function()
        {
            ConfigureServices();
            this.bookingRepository = services.GetRequiredService<IBookingRepository>();
        }
        private void ConfigureServices()
        {
            // Add dependencies here.
            var serviceCollection = new ServiceCollection();
            //serviceCollection.AddDbContext<BookingContext>(options =>
            //    options.UseMySQL("ConnectionString..."));

            serviceCollection.AddTransient<IBookingRepository, BookingRepository>();
            serviceCollection.AddHostedService<ReceiverService>();
            services = serviceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<string> FunctionHandler(string input, ILambdaContext context)
        {
            //await this.bookingRepository.Adiciona();
            
            var request = new SendMessageRequest()
            {
                QueueUrl = Acceess.GetRoute(),
                MessageBody = JsonSerializer.Serialize(input),
                DelaySeconds = 5 
            };
            var response = await Acceess.GetCredentials().SendMessageAsync(request);
            return $" { input } - { response.HttpStatusCode }";
        }
    }


}
