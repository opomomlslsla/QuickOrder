using Microsoft.Extensions.Logging;
using QuickOrder.Domain.Interfaces;
using QuickOrder.Domain.Models;

namespace QuickOrder.Application.Services
{
    public class OrderService(IRepository<Order> orders, ILogger<OrderService> logger) : IOrderService
    {
        public async Task AddOrderAsync(Order order)
        {
            logger.LogInformation($"Создание заказа: Id: {order.Id} Номер: {order.SerialNumber}");
            order.PickupDate = order.PickupDate.ToUniversalTime();
            await orders.AddAsync(order);
            await orders.SaveChangesAsync();
            logger.LogInformation($"Заказ создан: Id: {order.Id} Номер: {order.SerialNumber}");
        }

        public async Task DeleteOrderByIdAsync(Guid id)
        {
            logger.LogInformation($"Удаление заказа: Id: {id}");
            var order = await orders.FirstAsync(x => x.Id == id, CancellationToken.None);
            if (order != null)
                orders.Delete(order);
            await orders.SaveChangesAsync();
            logger.LogInformation($"Заказ удален: Id: {id}");
        }

        public async Task<ICollection<Order>> GetOrdersAsync(int page, CancellationToken cancellationToken)
        {
            if(page < 1) page = 1;
            return await orders.GetWithPagination(page, 100, cancellationToken);
        }

        public async Task<Order?> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await orders.FirstAsync(x => x.Id == id, cancellationToken);
        }
    }
}
