using Catalog.BLL.Entities;

namespace Catalog.BLL.Interfaces.Repository
{
    public interface IProductRepository
    {
        public Product Get(long id);
        public List<Product> GetAll(long categoryId);
        public long Add(Product product);
        public long Update(Product product);
        public long Delete(long id);
    }
}
