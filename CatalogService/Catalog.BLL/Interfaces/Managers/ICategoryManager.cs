using Catalog.BLL.Entities;

namespace Catalog.BLL.Interfaces.Managers
{
    public interface ICategoryManager
    {
        IEnumerable<Category> GetAll();
        Category Add(Category category);
        Category Update(Category category);
        void Delete(long category);
    }
}
