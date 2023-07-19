namespace CompanyAndDetails.Models;

public class Company
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Phone { get; set; }
    public List<Employee> Employees { get; set; }
    public List<Order> Orders { get; set; }
    public List<Note> Notes { get; set; }
}