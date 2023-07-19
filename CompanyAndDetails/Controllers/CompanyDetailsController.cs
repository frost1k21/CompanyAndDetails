using CompanyAndDetails.Models;
using CompanyAndDetails.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAndDetails.Controllers;

[ApiController]
[Route("api/company-details")]
public class CompanyDetailsController: ControllerBase
{
    private readonly ICompaniesService _companiesService;

    public CompanyDetailsController(ICompaniesService companiesService)
    {
        _companiesService = companiesService;
    }
    [HttpGet]
    public async Task<ActionResult<Company>> GetCompanyDetails([FromQuery]int? id)
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

        return Ok(company);
    }
}