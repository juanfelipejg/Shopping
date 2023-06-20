namespace Shopping.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Shopping.Application.Services.Products;
	using Shopping.Domain.Models.Products;

	[ApiController]
	[Route( "api/products" )]
	public class ProductController: ControllerBase
	{
		private readonly IProductService _productService;

		public ProductController( IProductService productService )
		{
			this._productService = productService;
		}

		[HttpGet]
		public IEnumerable<Product> Get()
		{
			return this._productService.Get();
		}

		[HttpGet]
		public Product Get( int id )
		{
			return this._productService.GetById( id );
		}

		[HttpPost]
		public Product Create( Product product )
		{
			return this._productService.Add( product );
		}

		[HttpPut]
		public Product Update( Product product )
		{
			return this._productService.Update( product );
		}

		[HttpDelete]
		public void Delete( int id )
		{
			this._productService.Delete( id );
		}
	}
}