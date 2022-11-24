using Catalog.BLL.Entities;

namespace CatalogService.Dtos
{
    public class CategoryDTO
    {
        public Category Data { get; set; }
        public List<Link> Links { get; set; }
    }
}
