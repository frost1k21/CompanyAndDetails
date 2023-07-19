namespace CompanyAndDetails.Models;

public class Order
{
    public int OrderId { get; set; }
    public int CompanyId { get; set; }
    public DateOnly OrderDate { get; set; }
    public string StoreCity { get; set; }
}