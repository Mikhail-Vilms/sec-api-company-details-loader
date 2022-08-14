using Newtonsoft.Json;
using SecApiCompanyDetailsLoader.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SecApiCompanyDetailsLoader.Services
{
    public class SecApiJsonReader
    {
        //private readonly string _company_tickers = "https://www.sec.gov/files/company_tickers.json";

        //private readonly HttpClient _httpClient;
        //private readonly CompanyDetailsLoader _companyDetailsLoader;

        //private int _counter = 0;

        //public SecApiJsonReader()
        //{
        //    _httpClient = new HttpClient();

        //    _httpClient
        //        .DefaultRequestHeaders
        //        .UserAgent
        //        .Add(new ProductInfoHeaderValue("ScraperBot", "1.0"));
        //    _httpClient
        //        .DefaultRequestHeaders
        //        .UserAgent
        //        .Add(new ProductInfoHeaderValue("(+http://www.example.com/ScraperBot.html)"));

        //    _companyDetailsLoader = new CompanyDetailsLoader();

        //    _counter = 0;
        //}

        ///// <summary>
        ///// https://www.newtonsoft.com/json/help/html/Performance.htm
        ///// https://stackoverflow.com/questions/32227436/parsing-large-json-file-in-net
        ///// https://stackoverflow.com/questions/30163316/httpclient-getstreamasync-and-http-status-codes
        ///// </summary>
        //public async Task ReadAndProcess()
        //{
        //    using (Stream stream = await _httpClient.GetStreamAsync(_company_tickers))
        //    using (StreamReader streamReader = new StreamReader(stream))
        //    using (JsonTextReader reader = new JsonTextReader(streamReader))
        //    {
        //        var serializer = new JsonSerializer();
        //        StringBuilder companyDetailsJsonString = new StringBuilder();
        //        _counter = 0;

        //        while (reader.Read())
        //        {
        //            if (_counter > 500)
        //            {
        //                return;
        //            }

        //            if (reader.TokenType == JsonToken.StartObject)
        //            {
        //                companyDetailsJsonString.Clear().Append("{");
        //            }

        //            if (reader.TokenType == JsonToken.PropertyName)
        //            {
        //                companyDetailsJsonString.Append("\"" + reader.Value.ToString() + "\"" + ":");
        //            }

        //            if (reader.TokenType == JsonToken.Integer || reader.TokenType == JsonToken.String)
        //            {
        //                companyDetailsJsonString.Append("\"" + reader.Value.ToString() + "\"" + ",");
        //            }

        //            if (reader.TokenType == JsonToken.EndObject)
        //            {
        //                companyDetailsJsonString.Append("}");

        //                try
        //                {
        //                    await HandleJsonStringObject(companyDetailsJsonString.ToString());
        //                }
        //                catch(Exception ex)
        //                {
        //                    Console.WriteLine("something wrong: " + ex.ToString());
        //                }
        //                companyDetailsJsonString.Clear();
        //            }
        //        }
        //    }
        //}

        //private async Task HandleJsonStringObject(string jsonStringObj)
        //{
        //    // Exclude Ameriacn Depository Receipts
        //    if (jsonStringObj.Contains("ADR"))
        //    {
        //        return;
        //    }
            
        //    _counter++;

        //    CompanyDetailsDto companyDetails = JsonConvert.DeserializeObject<CompanyDetailsDto>(jsonStringObj);
        //    if (companyDetails.CikNumber != null && companyDetails.TickerSymbol != null)
        //    {
        //        await _companyDetailsLoader.Load(companyDetails);
        //    }
        //}
    }
}
