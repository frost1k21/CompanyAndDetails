using CompanyAndDetails.Models;

namespace CompanyAndDetails.Services
{
    public interface ICompaniesService
    {
        public Task<List<Company>> GetAllCompanies();
        public Task<Company?> GetCompanyById(int id);
    }
}
