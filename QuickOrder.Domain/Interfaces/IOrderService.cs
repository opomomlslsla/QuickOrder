using QuickOrder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickOrder.Domain.Interfaces
{
    public interface IOrderService
    {
        Task<ICollection<Order>> GetOrdersAsync(int page, CancellationToken cancellationToken);
        Task<Order?> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddOrderAsync(Order order);
        Task DeleteOrderByIdAsync(Guid id);
    }
}
