using QuickOrder.Domain.Model;
using System.Security.Cryptography;

namespace QuickOrder.Domain.Models;

public class Order : BaseEntity
{
    public string SerialNumber { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string SenderCity { get; set; } = string.Empty;
    public string SenderAddress { get; set; } = string.Empty;
    public string RecipientCity { get; set; } = string.Empty;
    public string RecipientAddress { get; set; } = string.Empty;
    public decimal Weight { get; set; }
    public DateTime PickupDate { get; set; }
    public string Status { get; set; }
    public Order(bool isNew = true)
    {
        Id = Guid.CreateVersion7();
        CreatedAt = DateTime.UtcNow;
        string shortGuid = Id.ToString("N").Substring(28, 4).ToUpper();
        var randomNumber = RandomNumberGenerator.GetInt32(10000000, 99999999);
        SerialNumber = $"ORD-{randomNumber}-{shortGuid}";
        Status = "Created";
    }
    private Order() { }

}
