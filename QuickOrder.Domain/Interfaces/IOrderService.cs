using QuickOrder.Domain.Models;

namespace QuickOrder.Domain.Interfaces;

public interface IOrderService
{
    Task<ICollection<Order>> GetOrdersAsync(int page, CancellationToken cancellationToken);
    Task<Order?> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddOrderAsync(Order order);
    Task DeleteOrderByIdAsync(Guid id);
}
