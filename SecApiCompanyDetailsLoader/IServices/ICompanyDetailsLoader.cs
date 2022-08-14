using System;
using System.Threading.Tasks;

namespace SecApiCompanyDetailsLoader.IServices
{
    public interface ICompanyDetailsLoader
    {
        public Task Load(Action<string> logger);
    }
}
