using System.IO;
using System.Threading.Tasks;

namespace SecApiCompanyDetailsLoader.IServices
{
    public interface ISecApiClient
    {
        public Task<Stream> GetCompanyTickersFileStream();
    }
}
