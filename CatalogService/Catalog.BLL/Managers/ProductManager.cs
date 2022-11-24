using Catalog.BLL.Entities;
using Catalog.BLL.Interfaces.Managers;
using Catalog.BLL.Interfaces.Repository;

namespace Catalog.BLL.Managers
{
    public class ProductManager : IProductManager
    {
        public IProductRepository _productRepository;
        private const long _pageSize = 5;

        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetAll(long? page, long? category)
        {
            page = page ?? 1;

            var offset = (page.Value - 1) * _pageSize;

            return _productRepository.GetAll(offset, _pageSize, category);
        }

        public Product Add(Product product)
        {
            long productId = _productRepository.Add(product);
            product.Id = productId;

            return product;
        }

        public void Delete(long product)
        {
            _productRepository.Delete(product);
        }

        public Product Update(Product product)
        {
            _productRepository.Update(product);

            return product;
        }
    }
}
