using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using OrderingApp.Data.Models;
using OrderingApp.Logic.DTO;
using OrderingApp.Shared.Exceptions;

namespace OrderingApp.Logic.Functions.Command
{
    public class CreateOrderCommand(OrderDto order) : IRequest<Guid>
    {
        public OrderDto Order { get; set; } = order;
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly OrderingDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(OrderingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request.Order);
            order.CreationDate = DateTime.Now;

            try
            {
                if (await _dbContext.Orders.AnyAsync(x => x.Name == order.Name, cancellationToken))
                    throw new EntityExistException("There is Order Named this way");

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
