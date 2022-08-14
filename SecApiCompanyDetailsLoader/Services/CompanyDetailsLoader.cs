using Amazon.DynamoDBv2.DataModel;
using Newtonsoft.Json;
using SecApiCompanyDetailsLoader.IServices;
using SecApiCompanyDetailsLoader.Models;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SecApiCompanyDetailsLoader.Services
{
    public class CompanyDetailsLoader : ICompanyDetailsLoader
    {
        private readonly ISecApiClient _secApiClient;
        private readonly IDynamoDBContext _dynamoDbContext;

        private int _counter = 0;

        public CompanyDetailsLoader(
            ISecApiClient secApiClient,
            IDynamoDBContext dynamoDbContext)
        {
            _secApiClient = secApiClient;
            _dynamoDbContext = dynamoDbContext;

            _counter = 0;
        }

        public async Task Load(Action<string> logger)
        {
            using (Stream stream = await _secApiClient.GetCompanyTickersFileStream())
            using (StreamReader streamReader = new StreamReader(stream))
            using (JsonTextReader reader = new JsonTextReader(streamReader))
            {
                _counter = 0;
                CompanyDetailsJsonBuilder companyJsonBuilder = new CompanyDetailsJsonBuilder();
                string companyJsonStr = string.Empty;

                while (reader.Read())
                {
                    if (_counter > 500)
                    {
                        return;
                    }

                    if (companyJsonBuilder.HandleJsonToken(reader, out companyJsonStr)){
                        try
                        {
                            await ProcessCompanyJsonStr(companyJsonStr);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Something went wrong during processign company's json: Company's json" + companyJsonStr.ToString());
                            Console.WriteLine("Something went wrong during processign company's json; Exception: " + ex.ToString());
                        }
                    }
                }
            }
        }

        private async Task ProcessCompanyJsonStr(string companyJsonStr)
        {
            // Exclude American Depository Receipts
            if (companyJsonStr.Contains("ADR"))
            {
                return;
            }

            CompanyDetailsDto companyDetailsDto = JsonConvert.DeserializeObject<CompanyDetailsDto>(companyJsonStr);

            if (companyDetailsDto.CikNumber != null && companyDetailsDto.TickerSymbol != null)
            {
                await _dynamoDbContext
                    .SaveAsync(new CompanyDynamoItem()
                    {
                        PartitionKey = "LIST_OF_COMPANIES",
                        SortKey = companyDetailsDto.TickerSymbol,
                        IsActive = false,
                        CikNumber = companyDetailsDto.CikNumber,
                        Title = companyDetailsDto.Title
                    });
            }

            _counter++;
        }
    }
}
