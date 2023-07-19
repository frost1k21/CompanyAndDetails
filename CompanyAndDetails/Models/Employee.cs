namespace CompanyAndDetails.Models;

public class Employee
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Title { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Position { get; set; }
}