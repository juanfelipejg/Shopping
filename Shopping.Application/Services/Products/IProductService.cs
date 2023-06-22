using Shopping.Domain.Models.Products;

namespace Shopping.Application.Services.Products
{
	public interface IProductService
	{
		IEnumerable<Product> GetProducts( int pagepageNumber = 1, int pageSize = 10);

		Task<Product> GetProductAsync( int id );

		Product CreateProduct( Product product );

		Product UpdateProduct( Product product );

		void DeleteProduct( int id );
	}
}
