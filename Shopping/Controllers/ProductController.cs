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
		public IEnumerable<Product> GetProducts()
		{
			return this._productService.GetProducts();
		}

		[HttpGet( "{id}" )]
		public async Task<Product> GetProduct( int id )
		{
			return await this._productService.GetProduct( id );
		}

		[HttpPost]
		public Product Create( Product product )
		{
			return this._productService.AddProduct( product );
		}

		[HttpPut]
		public Product Update( Product product )
		{
			return this._productService.UpdateProduct( product );
		}

		[HttpDelete]
		public void Delete( int id )
		{
			this._productService.DeleteProduct( id );
		}
	}
}