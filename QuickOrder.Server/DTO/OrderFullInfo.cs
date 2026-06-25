namespace QuickOrder.Server.DTO;

public record OrderFullInfo(
Guid Id,
string SenderCity,
string SenderAddress,
string RecipientCity,
string RecipientAddress,
decimal Weight,
DateTime PickupDate,
string Status,
string SerialNumber
);
