using Microsoft.AspNetCore.Mvc;
using Catalog.BLL.Entities;
using Catalog.BLL.Interfaces.Managers;
using CatalogService.Dtos;

namespace CatalogService.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private IProductManager _productManager;
        private ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger, IProductManager productManager) : base()
        {
            _logger = logger;
            _productManager = productManager;
        }

        [HttpGet()]
        public IEnumerable<Product> GetAll(long? page, long? category)
        {
            return _productManager.GetAll(page, category); 
        }

        [HttpPost]
        public ProductDTO Add(Product product)
        {
            return new ProductDTO()
            {
                Data = _productManager.Add(product),
                Links = new List<Link>
                {
                    new Link("update", "/products", HttpType.Put),
                    new Link("delete", $"/products?product={product.Id}", HttpType.Delete)
                }
            };
        }

        [HttpPut]
        public Product Update(Product product)
        {
            return _productManager.Update(product);
        }

        [HttpDelete]
        public void Delete(long product)
        {
            _productManager.Delete(product);
        }
    }
}