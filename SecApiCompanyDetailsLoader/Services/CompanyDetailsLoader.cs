using SecApiCompanyDetailsLoader.Models;
using SecApiCompanyDetailsLoader.Repositories;
using System.Threading.Tasks;

namespace SecApiCompanyDetailsLoader.Services
{
    public class CompanyDetailsLoader
    {
        private CompanyDetailsRepository _companyDetailsRepository;

        public CompanyDetailsLoader()
        {
            _companyDetailsRepository = new CompanyDetailsRepository();
        }

        public async Task Load(CompanyDetailsDto companyDetailsDto)
        {
            //await _companyDetailsRepository.SaveLookupItem_CikNumberByTickerSymbol(companyDetailsDto);
            //await _companyDetailsRepository.SaveLookupItem_CompanyDetailsByCikNumber(companyDetailsDto);
            await _companyDetailsRepository.SaveToListOfCompanies(companyDetailsDto);
        }
    }
}
