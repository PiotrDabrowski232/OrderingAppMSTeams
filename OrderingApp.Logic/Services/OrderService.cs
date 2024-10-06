using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using OrderingApp.Logic.Services.Interface;

namespace OrderingApp.Logic.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderingDbContext _dbContext;
        public OrderService(OrderingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> OrderNameExist(string name, CancellationToken cancellationToken)
        {
            return await _dbContext.Orders.Where(x => x.IsActive).AnyAsync(x => x.Name == name, cancellationToken);
        }
    }
}
