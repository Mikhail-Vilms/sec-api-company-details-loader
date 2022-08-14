using Newtonsoft.Json;
using System.Text;

namespace SecApiCompanyDetailsLoader.Services
{
    public class CompanyDetailsJsonBuilder
    {
        private StringBuilder _jsonStrBuilder;

        public CompanyDetailsJsonBuilder()
        {
            _jsonStrBuilder = new StringBuilder();
        }

        public bool HandleJsonToken(JsonTextReader reader, out string finalJsonStr_Out)
        {
            if (reader.TokenType == JsonToken.EndObject)
            {
                _jsonStrBuilder.Append("}");

                finalJsonStr_Out = _jsonStrBuilder.ToString();
                
                _jsonStrBuilder.Clear();
                
                return true;
            }

            if (reader.TokenType == JsonToken.StartObject)
            {
                _jsonStrBuilder.Clear().Append("{");
            }

            if (reader.TokenType == JsonToken.PropertyName)
            {
                _jsonStrBuilder.Append("\"" + reader.Value.ToString() + "\"" + ":");
            }

            if (reader.TokenType == JsonToken.Integer ||reader.TokenType == JsonToken.String)
            {
                _jsonStrBuilder.Append("\"" + reader.Value.ToString() + "\"" + ",");
            }

            finalJsonStr_Out = string.Empty;
            return false;
        }
    }
}
