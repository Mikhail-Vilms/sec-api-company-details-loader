using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using SecApiCompanyDetailsLoader.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace SecApiCompanyDetailsLoader.Repositories
{
    public class CompanyDetailsRepository
    {
        private AmazonDynamoDBClient _dynamoClient;
        private Table _dynamoTable;
        private string _tableName = "Sec-Api-Data";

        public CompanyDetailsRepository()
        {
            _dynamoClient = new AmazonDynamoDBClient(RegionEndpoint.USEast1);
            _dynamoTable = Table.LoadTable(_dynamoClient, _tableName, true);
        }

        public async Task SaveLookupItem_CikNumberByTickerSymbol(CompanyDetailsDto companyDetailsDto)
        {
            var newItem = new
            {
                PartitionKey = companyDetailsDto.TickerSymbol,
                SortKey = "CIK_NUMBER_LOOKUP",
                companyDetailsDto.CikNumber
            };

            var documentJson = JsonSerializer.Serialize(newItem);
            var document = Document.FromJson(documentJson);

            await _dynamoTable.PutItemAsync(document);
        }

        public async Task SaveLookupItem_CompanyDetailsByCikNumber(CompanyDetailsDto companyDetailsDto)
        {
            var newItem = new
            {
                PartitionKey = companyDetailsDto.CikNumber,
                SortKey = "COMPANY_DETAILS_LOOKUP",
                CompanyDetails = new Dictionary<string, string>()
                {
                    { "TickerSymbol" , companyDetailsDto.TickerSymbol},
                    { "Title", companyDetailsDto.Title}
                }
            };

            var documentJson = JsonSerializer.Serialize(newItem);
            var document = Document.FromJson(documentJson);

            await _dynamoTable.PutItemAsync(document);
        }

        //public async Task SaveToDynamo(
        //    string cikNumber,
        //    Dictionary<string, FinancialStatementNode> financialStatementPositions)
        //{
        //    using var ddbClient = new AmazonDynamoDBClient(RegionEndpoint.USEast1);
        //    var dynamoTable = Table.LoadTable(ddbClient, _tableName, true);

        //    ReportStructureDynamoDbItem newItem = new ReportStructureDynamoDbItem()
        //    {
        //        cik = cikNumber,
        //        tag = "CashFlowStatementStructure",
        //        FinancialPositions = financialStatementPositions.Values.ToList()
        //    };

        //    var documentJson = JsonSerializer.Serialize(newItem);
        //    var document = Document.FromJson(documentJson);

        //    await dynamoTable.PutItemAsync(document);
        //}
    }
}
