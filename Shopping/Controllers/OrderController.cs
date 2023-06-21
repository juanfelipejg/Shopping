using Microsoft.AspNetCore.Mvc;
using Shopping.Application.Services.Orders;
using Shopping.Domain.Models.Orders;

namespace Shopping.Controllers
{
	[ApiController]
	[Route( "api/orders" )]
	public class OrderController: ControllerBase
	{
		private readonly IOrderService _orderService;

		public OrderController( IOrderService orderService )
		{
			this._orderService = orderService;
		}

		[HttpGet]
		public IEnumerable<Order> GetOrders()
		{
			return this._orderService.GetOrders();
		}

		[HttpGet( "{id}" )]
		public async Task<Order> GetOrder( int id )
		{
			return await this._orderService.GetOrderAsync( id );
		}

		[HttpPost]
		public Task<Order> PostOrder( Order order )
		{
			return this._orderService.CreateOrderAsync( order );
		}
	}
}
