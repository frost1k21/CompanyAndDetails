using Bogus;
using CompanyAndDetails.DbContexts;
using CompanyAndDetails.Models;

namespace CompanyAndDetails.Data;

public static class DbInitializer
{
    public static void Initialize(CompaniesContext context)
    {
        context.Database.EnsureCreated();

        if (context.Companies.Any())
        {
            return;
        }
        
        var companyRules = new Faker<Company>()
            .RuleFor(c => c.CompanyName, f => f.Company.CompanyName())
            .RuleFor(c => c.Address, f => f.Address.StreetAddress())
            .RuleFor(c => c.City, f => f.Address.City())
            .RuleFor(c => c.State, f => f.Address.State())
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber());

        var companies = companyRules.Generate(12);

        context.Companies.AddRange(companies);
        context.SaveChanges();


        var positions = new[] {"CEO", "IT-Administrator", "Manager", "Accountant", "Lawyer"};
        var titles = new[] { "Mr.", "Ms."};
        var employeeRules = new Faker<Employee>()
            .RuleFor(e => e.FirstName, f => f.Person.FirstName)
            .RuleFor(e => e.LastName, f => f.Person.LastName)
            .RuleFor(e => e.Title, f => f.PickRandom(titles))
            .RuleFor(e => e.Position, f => f.PickRandom(positions))
            .RuleFor(e => e.BirthDate, f => f.Date.BetweenDateOnly(new DateOnly(1976, 1, 1), new DateOnly(2000, 12, 31)));

        var employees = employeeRules.Generate(48);
        context.Employees.AddRange(employees);
        context.SaveChanges();

        var orderRules = new Faker<Order>()
            .RuleFor(o => o.StoreCity, f => f.Address.City())
            .RuleFor(o => o.OrderDate,
                f => f.Date.BetweenDateOnly(new DateOnly(2013, 1, 1), new DateOnly(2014, 12, 31)));

        var orders = orderRules.Generate(72).OrderBy(o => o.OrderDate).ToList();
        context.Orders.AddRange(orders);
        context.SaveChanges();

        var noteRules = new Faker<Note>()
            .RuleFor(n => n.InvoiceNumber, f => f.Random.Number(35000, 37500));

        var notes = noteRules.Generate(72).OrderByDescending(n => n.InvoiceNumber).ToList();
        context.Notes.AddRange(notes);
        context.SaveChanges();
        
        var skipEmployees = 0;
        var skipOrdersAndNotes = 0;
        foreach (var company in companies)
        {
            var employeesForCompany = employees.Skip(skipEmployees).Take(4).ToList();
            var ordersForCompany = orders.Skip(skipOrdersAndNotes).Take(6).ToList();
            var notesForCompany = notes.Skip(skipOrdersAndNotes).Take(6).ToList();

            foreach (var order in ordersForCompany)
            {
                order.CompanyId = company.CompanyId;
            }
            
            company.Employees = employeesForCompany;
            company.Orders = ordersForCompany;

            foreach (var note in notesForCompany)
            {
                var rndEmployeeIndex = Random.Shared.Next(company.Employees.Count - 1);
                var rndEmployee = company.Employees[rndEmployeeIndex];
                note.Employee = rndEmployee;
                note.EmployeeId = rndEmployee.EmployeeId;
            }

            company.Notes = notesForCompany;
            
            skipEmployees+=4;
            skipOrdersAndNotes += 6;
        }
        context.SaveChanges();
    }
}