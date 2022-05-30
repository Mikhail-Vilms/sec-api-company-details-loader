
using Amazon.Lambda.Core;
using SecApiCompanyDetailsLoader.Services;
using System.Threading.Tasks;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace SecApiCompanyDetailsLoader
{
    public class Function
    {
        private readonly SecApiJsonReader secApiJsonReader;

        public Function()
        {
            secApiJsonReader = new SecApiJsonReader();
        }

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<string> FunctionHandler(string input, ILambdaContext context)
        {
            await secApiJsonReader.ReadAndProcess();
            return input?.ToUpper();
        }
    }
}
