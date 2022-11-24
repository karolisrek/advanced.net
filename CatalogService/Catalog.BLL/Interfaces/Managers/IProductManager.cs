using Catalog.BLL.Entities;

namespace Catalog.BLL.Interfaces.Managers
{
    public interface IProductManager
    {
        IEnumerable<Product> GetAll(long? page, long? product);
        Product Add(Product product);
        void Delete(long product);
        Product Update(Product product);
    }
}
