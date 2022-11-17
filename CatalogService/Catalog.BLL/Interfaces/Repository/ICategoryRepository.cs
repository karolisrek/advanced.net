using Catalog.BLL.Entities;

namespace Catalog.BLL.Interfaces.Repository
{
    public interface ICategoryRepository
    {
        public Category Get(long id);
        public List<Category> GetAll(long? parentCategoryId = null);
        public long Add(Category catalog);
        public long Update(Category catalog);
        public long Delete(long id);
    }
}
