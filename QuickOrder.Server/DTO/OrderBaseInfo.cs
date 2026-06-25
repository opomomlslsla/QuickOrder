namespace QuickOrder.Server.DTO;

public record OrderBaseInfo(
Guid Id,
string SenderCity,
string RecipientCity,
DateTime PickupDate,
string Status,
string SerialNumber);
