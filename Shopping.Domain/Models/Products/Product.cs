namespace Shopping.Domain.Models.Products
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Inventory { get; set; }

        public bool Enabled { get; set; }

        public int Min { get; set; }

        public int Max { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }
    }
}