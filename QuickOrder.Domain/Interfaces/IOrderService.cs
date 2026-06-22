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
        Task<ICollection<Order>> GetOrdersAsync(int page);
        Task<Order?> GetOrderByIdAsync(Guid id);
        Task AddOrderAsync(Order order);
        void DeleteOrderByIdAsync(Guid id);
    }
}
