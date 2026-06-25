using QuickOrder.Domain.Models;
using QuickOrder.Infrastructure.Data;
using QuickOrder.Infrastructure.Repositories.Common;

namespace QuickOrder.Infrastructure.Repositories;

internal sealed class OrderRepository(Context context) : BaseRepository<Order>(context);
