using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using OrderingApp.Logic.DTO;

namespace OrderingApp.Logic.Functions.Query.Order
{
    public class GetAllActiveOrdersQuery : IRequest<IEnumerable<OrderDetails>>;

    public class GetAllActiveOrdersQueryHandler : BaseRequestHandler<GetAllActiveOrdersQuery, IEnumerable<OrderDetails>>
    {
        public GetAllActiveOrdersQueryHandler(OrderingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<IEnumerable<OrderDetails>> Handle(GetAllActiveOrdersQuery request, CancellationToken cancellationToken)
        {
            var userProfileId = Guid.NewGuid(); 

            var ordersQuery = await _dbContext.Orders
                .Where(x => x.IsActive && x.CreatedBy != userProfileId)
                .Include(x => x.Restaurant)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<OrderDetails>>(ordersQuery);

        }
    }
}
