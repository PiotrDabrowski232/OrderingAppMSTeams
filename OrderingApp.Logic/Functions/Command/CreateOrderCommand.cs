using AutoMapper;
using MediatR;
using OrderingApp.Data.DBConfig;
using OrderingApp.Data.Models;
using OrderingApp.Logic.DTO;
using OrderingApp.Logic.Services.Interface;
using OrderingApp.Shared.Exceptions;

namespace OrderingApp.Logic.Functions.Command
{
    public class CreateOrderCommand(OrderDto order) : IRequest<Guid>
    {
        public OrderDto Order { get; set; } = order;
    }

    public class CreateOrderCommandHandler : BaseRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderService _orderService;

        public CreateOrderCommandHandler(OrderingDbContext dbContext, IMapper mapper, IOrderService orderService) : base(dbContext, mapper)
        {
            _orderService = orderService;
        }

        public override async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request.Order);

            try
            {
                if (await _orderService.OrderNameExist(order.Name, cancellationToken))
                    throw new EntityExistException("There is Active Order Named this way");

                await _dbContext.Orders.AddAsync(order, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return order.Id;
            }
            catch
            {
                throw;
            }
        }
    }
}
