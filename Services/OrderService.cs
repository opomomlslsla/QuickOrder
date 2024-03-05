using Microsoft.EntityFrameworkCore;
using QuickOrder.Data;
using QuickOrder.Domain.Interfaces;
using QuickOrder.Domain.Models;

namespace QuickOrder.Services
{
    public class OrderService : IOrderService
    {
        private readonly Context _context;
        public OrderService(Context context)
        {
            _context = context;
        }

        public void AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public async void DeleteOrderByIdAsync(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        public async Task<ICollection<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.OrderByDescending(x => x.SerialNumber).ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(Guid id)
        {
            return await _context.Orders.FindAsync(id);
        }
    }
}
