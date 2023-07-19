using Bogus;
using CompanyAndDetails.DbContexts;
using CompanyAndDetails.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyAndDetails.Services
{
    public class CompaniesService : ICompaniesService
    {
        private readonly CompaniesContext _companiesContext;

        public CompaniesService(CompaniesContext companiesContext)
        {
            _companiesContext = companiesContext;
        }
        public async Task<List<Company>> GetAllCompanies()
        {
            return await _companiesContext.Companies.ToListAsync();
        }

        public async Task<Company?> GetCompanyById(int id)
        {
            var result =  await _companiesContext.Companies
                .Include(c => c.Employees)
                .Include(c => c.Orders)
                .Include(c => c.Notes)
                .FirstOrDefaultAsync(c => c.CompanyId == id);

            return result;
        }
    }
}
