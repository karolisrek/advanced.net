using Catalog.BLL.Entities;
using Catalog.BLL.Interfaces.Managers;
using Catalog.BLL.Interfaces.Repository;

namespace Catalog.BLL.Managers
{
    public class CategoryManager : ICategoryManager
    {
        public ICategoryRepository _categoryRepository;
        public IProductRepository _productRepository;

        public CategoryManager(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public IEnumerable<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Category Add(Category category)
        {
            var categoryId = _categoryRepository.Add(category);
            category.Id = categoryId;

            return category;
        }

        public Category Update(Category category)
        {
            _categoryRepository.Update(category);

            return category;
        }

        public void Delete(long category)
        {
            var categoryId = _categoryRepository.Delete(category);
            _productRepository.DeleteForCategory(category);
        }
    }
}
