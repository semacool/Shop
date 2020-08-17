using System;
using System.Collections.Generic;
using System.Text;

namespace ShopDesktop.Models
{
    class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
