namespace OrderingApp.Logic.Services.Interface
{
    public interface IOrderService
    {
        public Task<bool> OrderNameExist(string name, CancellationToken cancellationToken);
    }
}
