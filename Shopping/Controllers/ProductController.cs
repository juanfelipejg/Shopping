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

		[HttpGet( "{pageNumber}/{pageSize}" )]
		public IEnumerable<Product> GetProducts( int pageNumber = 1, int pageSize = 10 )
		{
			return this._productService.GetProducts( pageNumber, pageSize );
		}

		[HttpGet( "{id}" )]
		public async Task<Product> GetProduct( int id )
		{
			return await this._productService.GetProductAsync( id );
		}

		[HttpPost]
		public Product CreateProduct( Product product )
		{
			return this._productService.CreateProduct( product );
		}

		[HttpPut]
		public Product UpdateProduct( Product product )
		{
			return this._productService.UpdateProduct( product );
		}

		[HttpDelete]
		public void DeleteProduct( int id )
		{
			this._productService.DeleteProduct( id );
		}
	}
}