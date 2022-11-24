using Catalog.BLL.Entities;

namespace CatalogService.Dtos
{
    public class ProductDTO
    {
        public Product Data { get; set; }
        public List<Link> Links { get; set; }
    }
}
