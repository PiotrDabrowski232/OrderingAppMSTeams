using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using OrderingApp.Logic.DTO;
using OrderingApp.Logic.Services.Interface;

namespace OrderingApp.Logic.Functions.Query.Order
{
    public class GetAllActiveOrdersQuery : IRequest<IEnumerable<OrderDetails>>;

    public class GetAllActiveOrdersQueryHandler : BaseRequestHandler<GetAllActiveOrdersQuery, IEnumerable<OrderDetails>>
    {
        private readonly IUserProfileService _userProfileService;
        private readonly Guid Id;
        public GetAllActiveOrdersQueryHandler(OrderingDbContext dbContext, IMapper mapper, IUserProfileService userProfileService) : base(dbContext, mapper)
        {
            _userProfileService = userProfileService;
        }

        public override async Task<IEnumerable<OrderDetails>> Handle(GetAllActiveOrdersQuery request, CancellationToken cancellationToken)
        {
            var userProfileId = await _userProfileService.GetUserProfileIdAsync();

            var ordersQuery = await _dbContext.Orders
                .Where(x => x.IsActive && x.CreatedBy != userProfileId)
                .Include(x => x.Restaurant)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<OrderDetails>>(ordersQuery);

        }
    }
}
