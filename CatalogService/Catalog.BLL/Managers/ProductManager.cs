using Catalog.BLL.Interfaces.Managers;
using Catalog.BLL.Interfaces.Repository;

namespace Catalog.BLL.Managers
{
    public class ProductManager : IProductManager
    {
        public IProductRepository _productRepository;
        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
    }
}
