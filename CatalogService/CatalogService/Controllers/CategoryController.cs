using Microsoft.AspNetCore.Mvc;
using Catalog.BLL.Entities;
using Catalog.BLL.Interfaces.Managers;

namespace CatalogService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private ICategoryManager _catalogManager;
        private ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger, ICategoryManager catalogManager) : base()
        {
            _logger = logger;
            _catalogManager = catalogManager;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<Category> Get()
        {
            return _catalogManager.GetCategories(); 
        }
    }
}