using Microsoft.EntityFrameworkCore;
using Shopping.Domain.Models.Orders;
using Shopping.Infrastructure.Data;

namespace Shopping.Application.Services.Orders
{
	public class OrderService: IOrderService
	{
		private readonly ShoppingContext _context;

		public OrderService( ShoppingContext context )
		{
			this._context = context;
		}

		public IEnumerable<Order> GetOrders()
		{
			return this._context.Orders.Select( o => o ).Include( o => o.OrderProducts );
		}

		public async Task<Order> GetOrder( int id )
		{
			return await this._context.Orders.FirstOrDefaultAsync( o => o.Id == id );
		}

		public Order CreateOrder( Order product )
		{
			var productCreated = this._context.Orders.Add( product ).Entity;
			this._context.SaveChanges();

			return productCreated;
		}
	}
}
