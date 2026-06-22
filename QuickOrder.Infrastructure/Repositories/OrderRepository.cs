using QuickOrder.Domain.Models;
using QuickOrder.Infrastructure.Data;
using QuickOrder.Infrastructure.Repositories.Common;
using System.Linq.Expressions;

namespace QuickOrder.Infrastructure.Repositories
{
    internal sealed class OrderRepository(Context context) : BaseRepository<Order>(context);
}
