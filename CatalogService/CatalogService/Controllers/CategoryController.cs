using Microsoft.AspNetCore.Mvc;
using Catalog.BLL.Entities;
using Catalog.BLL.Interfaces.Managers;
using CatalogService.Dtos;

namespace CatalogService.Controllers
{
    [ApiController]
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        private ICategoryManager _catalogManager;
        private ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger, ICategoryManager catalogManager) : base()
        {
            _logger = logger;
            _catalogManager = catalogManager;
        }

        [HttpGet]
        public IEnumerable<Category> GetAll()
        {
            return _catalogManager.GetAll(); 
        }

        [HttpPost]
        public CategoryDTO Add(Category category)
        {
            var data = _catalogManager.Add(category);

            return new CategoryDTO() { 
                Data = data,
                Links = new List<Link>()
                {
                    new Link("update", "/categories", HttpType.Put),
                    new Link("delete", $"/categories?category={category.Id}", HttpType.Delete)
                }
            };
        }

        [HttpPut]
        public Category Update(Category category)
        {
            return _catalogManager.Update(category);
        }

        [HttpDelete]
        public void Delete(long category)
        {
            _catalogManager.Delete(category);
        }
    }
}