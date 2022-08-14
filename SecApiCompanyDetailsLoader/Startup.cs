using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SecApiCompanyDetailsLoader.IServices;
using SecApiCompanyDetailsLoader.Services;

namespace SecApiCompanyDetailsLoader
{
    public static class Startup
    {
        internal static IHostBuilder SetupHostForLambda(this IHostBuilder hostBuilder) => hostBuilder
            .AddRuntimeDependenciesBinding();

        private static IHostBuilder AddRuntimeDependenciesBinding(this IHostBuilder hostBuilder) => hostBuilder
            .ConfigureServices((context, serviceCollection) => serviceCollection
                .AddScoped<ICompanyDetailsLoader, CompanyDetailsLoader>()
                .AddSingleton<ISecApiClient, SecApiClient>()
                .AddSingleton<IDynamoDBContext>(provider =>
                {
                    var client = new AmazonDynamoDBClient(RegionEndpoint.USWest2);
                    return new DynamoDBContext(client, new DynamoDBContextConfig()
                    {
                        ConsistentRead = false
                    });
                }));
    }
}
