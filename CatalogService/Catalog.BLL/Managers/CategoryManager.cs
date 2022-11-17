using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.BLL.Entities;
using Catalog.BLL.Interfaces.Managers;
using Catalog.BLL.Interfaces.Repository;

namespace Catalog.BLL.Managers
{
    public class CategoryManager : ICategoryManager
    {
        public ICategoryRepository _categoryRepository;
        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categoryRepository.GetAll();
        }
    }
}
