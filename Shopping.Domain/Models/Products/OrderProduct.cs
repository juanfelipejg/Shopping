﻿namespace Shopping.Domain.Models.Products
{
    public class OrderProduct
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}