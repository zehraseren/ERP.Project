using ERPServer.Domain.Enums;
using ERPServer.Domain.Abstractions;

namespace ERPServer.Domain.Entities;

public sealed class Order : Entity
{
    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public int OrderNumberYear { get; set; }
    public int OrderNumber { get; set; }
    public string Number => SetNumber();
    public DateOnly Date { get; set; }
    public DateOnly DeliveryDate { get; set; }
    public OrderStatusEnum Status { get; set; } = OrderStatusEnum.Pending;
    public List<OrderDetail>? OrderDetails { get; set; }
    public string SetNumber()
    {
        string prefix = "ZS";

        string initialString = prefix + OrderNumberYear.ToString() + OrderNumber.ToString();
        int targetLength = 16;
        int missingLength = targetLength - initialString.Length;
        string finalString = prefix + OrderNumberYear.ToString() + new string('0', missingLength) + OrderNumber.ToString();

        return finalString;
    }
}
