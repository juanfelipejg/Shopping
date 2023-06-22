using Moq;
using Shopping.Application.Services.Products;
using Shopping.Domain.Models.Products;
using Shopping.Infrastructure.Data;
using Shopping.Infrastructure.Repository;

namespace Shopping.Tests.Products
{
	public class ProductTests
	{
		private readonly ProductService _productService;
		private readonly Mock<IRepositoryProduct> _mockRepository;
		private readonly Mock<ShoppingContext> _context;

		public ProductTests()
		{
			this._mockRepository = new Mock<IRepositoryProduct>();
			this._context = new Mock<ShoppingContext>();
			this._productService = new ProductService( this._context.Object, this._mockRepository.Object );
		}

		[Fact]
		public void GetProducts()
		{
			// Arrange
			int pageNumber = 1;
			int pageSize = 10;
			var expectedProducts = new List<Product>
			{
				new Product
				{
					Id = 1,
					Name = "Producto 1",
					Inventory = 10,
					Enabled = true,
					Min = 1,
					Max = 100
				},
				new Product
				{
					Id = 2,
					Name = "Producto 2",
					Inventory = 5,
					Enabled = true,
					Min = 2,
					Max = 50
				},
			};
			this._mockRepository.Setup( repo => repo.GetProducts( pageNumber, pageSize ) ).Returns( expectedProducts );

			// Act
			var actualProducts = _productService.GetProducts( pageNumber, pageSize );

			// Assert
			Assert.Equal( expectedProducts, actualProducts );
		}
	}
}
