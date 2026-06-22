namespace QuickOrder.Server.DTO
{
    public record OrderFullInfo(
    Guid Id,
    string SenderCity,
    string SenderAddress,
    string RecipientCity,
    string RecipientAddress,
    int Weight,
    DateTime PickupDate,
    string Status,
    string SerialNumber
);
}
