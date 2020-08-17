using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopRESTAPI.Models;
using ShopRESTAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopRESTAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private ILogger _logger;
        private IProductService _service;

        public ProductsController(ILogger<ProductsController> logger, IProductService service) 
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("/api/products")]
        public ActionResult<List<Product>> GetProducts() 
        {
            return _service.GetProducts();
        }

        [HttpPost("/api/products")]
        public ActionResult<Product> AddProduct(Product product)
        {
            _service.AddProduct(product);
            return product;
        }


        [HttpPut("/api/products/{id}")]
        public ActionResult<Product> UpdateProduct(int id, Product product)
        {
            _service.UpdateProduct(id, product);
            return product;

        }

        [HttpDelete("/api/products/{id}")]
        public ActionResult<string> DeleteProduct(int id)
        {
            _service.DeleteProduct(id);
            return id.ToString();
        }


    }
}
