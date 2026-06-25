namespace QuickOrder.Server.DTO;

public record CreateOrderRequest(
string SenderCity,
string SenderAddress,
string RecipientCity,
string RecipientAddress,
decimal Weight,
DateTime PickupDate);
