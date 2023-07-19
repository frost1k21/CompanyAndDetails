namespace CompanyAndDetails.Models;

public class Note
{
    public int NoteId { get; set; }
    public int InvoiceNumber { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
}