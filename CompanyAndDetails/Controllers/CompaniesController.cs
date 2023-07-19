using CompanyAndDetails.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAndDetails.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ICompaniesService _companiesService;

        public CompaniesController(ICompaniesService companiesService)
        {
            _companiesService = companiesService;
        }
        public async Task<IActionResult> Index()
        {
            var companies = await _companiesService.GetAllCompanies();
            return View(companies);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            
            var company = await _companiesService.GetCompanyById(id.Value);

            if (company == null)
            {
                return NotFound();
            }
            
            return View(company);
        }
    }
}
