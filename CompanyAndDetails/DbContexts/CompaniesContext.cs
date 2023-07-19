using Bogus;
using CompanyAndDetails.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyAndDetails.DbContexts;
public class CompaniesContext: DbContext
{
    public CompaniesContext(DbContextOptions<CompaniesContext> opts): base(opts)
    {
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Note> Notes { get; set; }
}