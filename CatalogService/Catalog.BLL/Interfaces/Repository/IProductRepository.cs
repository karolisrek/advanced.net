using Catalog.BLL.Entities;

namespace Catalog.BLL.Interfaces.Repository
{
    public interface IProductRepository
    {
        public Product Get(long id);
        public List<Product> GetAll(long offset, long limit, long? categoryId = null);
        public long Add(Product product);
        public long Update(Product product);
        public long Delete(long id);
        long GetProductCount();
        long DeleteForCategory(long category);
    }
}
