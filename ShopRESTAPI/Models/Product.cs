using System;

namespace ShopRESTAPI.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public DateTime Date { get; set; }
    }
}
