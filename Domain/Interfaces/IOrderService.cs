using QuickOrder.Domain.Models;

namespace QuickOrder.Domain.Interfaces
{
    public interface IOrderService
    {
        Task<ICollection<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(Guid id);
        void AddOrderAsync(Order order);
        void DeleteOrderByIdAsync(Guid id);
    }
}
