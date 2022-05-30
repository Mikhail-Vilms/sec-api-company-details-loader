using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecApiCompanyDetailsLoader.Models
{
    public class CompanyDetailsDto
    {
        private string _cikNumber;
        private string _tickerSymbol;
        private string _title;

        [JsonProperty("cik_str")]
        [Required]
        public string CikNumber
        {
            get
            {
                return _cikNumber;
            }
            set
            {
                _cikNumber = new StringBuilder()
                    .Append("CIK")
                    .Append(value.PadLeft(10, '0'))
                    .ToString();
            }
        }

        [JsonProperty("ticker")]
        [Required]
        public string TickerSymbol
        {
            get
            {
                return _tickerSymbol;
            }
            set
            {
                _tickerSymbol = value.ToUpper();
            }
        }

        [JsonProperty("title")]
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value.Replace("/", "");
            }
        }
    }
}
