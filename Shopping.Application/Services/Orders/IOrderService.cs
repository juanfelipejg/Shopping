using Shopping.Domain.Models.Orders;

namespace Shopping.Application.Services.Orders
{
	public interface IOrderService
	{
		IEnumerable<Order> GetOrders();

		Task<Order> GetOrder( int id );

		Order CreateOrder( Order product );
	}
}
