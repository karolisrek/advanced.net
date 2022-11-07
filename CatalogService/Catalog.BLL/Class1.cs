using Catalog.DAL.Repository;

namespace Catalog.BLL
{
    public class Class1
    {
        public void Test()
        {
            var catRep = new ProductRepository();

            var c = new DAL.Models.Product()
            {
                Id = 2,
                Name = "name4",
                Image = "image",
                CategoryId = 2,
                Price = 9.5M,
                Amount = 5,
                Description = "desc",
            };
            var a = catRep.Delete(3);
        }
    }
}