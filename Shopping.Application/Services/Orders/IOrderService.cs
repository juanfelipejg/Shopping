using Shopping.Domain.Models.Orders;

namespace Shopping.Application.Services.Orders
{
	public interface IOrderService
	{
		IEnumerable<Order> Get();

		Order GetById( int id );

		Order Add( Order product );

		Order Update( Order product );

		void Delete( string id );
	}
}
