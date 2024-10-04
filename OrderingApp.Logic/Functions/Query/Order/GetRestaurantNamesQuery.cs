using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;

namespace OrderingApp.Logic.Functions.Query.Order
{
    public class GetRestaurantNamesQuery : IRequest<IDictionary<Guid, string>>
    {

    }
    public class GetRestaurantNamesQueryHandler : IRequestHandler<GetRestaurantNamesQuery, IDictionary<Guid, string>>
    {
        private readonly OrderingDbContext _dbContext;

        public GetRestaurantNamesQueryHandler(OrderingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IDictionary<Guid, string>> Handle(GetRestaurantNamesQuery request, CancellationToken cancellationToken)
        {
            var result =  await _dbContext.Restaurants
                .Select(x => new { x.Id, x.Name })
                .OrderBy(x => x.Name)
                .ToDictionaryAsync(x => x.Id, x => x.Name, cancellationToken);  

            return result;
        }
    }
}
