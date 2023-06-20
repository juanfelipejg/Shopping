using Shopping.Domain.Models.Products;

namespace Shopping.Application.Services.Products
{
	public interface IProductService
	{
		IEnumerable<Product> Get();

		Product GetById( int id );

		Product Add( Product product );

		Product Update( Product product );

		void Delete( int id );
	}
}
