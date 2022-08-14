using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SecApiCompanyDetailsLoader.IServices;
using System.Threading.Tasks;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace SecApiCompanyDetailsLoader
{
    public class Function
    {
        private readonly ICompanyDetailsLoader _companyDetailsLoader;

        public Function()
        {
            var host = new HostBuilder()
               .SetupHostForLambda()
               .Build();

            var serviceProvider = host.Services;

            _companyDetailsLoader = serviceProvider
                .GetRequiredService<ICompanyDetailsLoader>();
        }

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<string> FunctionHandler(string input, ILambdaContext context)
        {
            void Log(string logMessage)
            {
                context.Logger.LogLine($"[RequestId: {context.AwsRequestId}]: {logMessage}");
            }

            await _companyDetailsLoader.Load(Log);

            return "List of companies has been loaded <<<<<";
        }
    }
}
