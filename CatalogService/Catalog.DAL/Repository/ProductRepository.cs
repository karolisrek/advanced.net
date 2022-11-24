using Catalog.BLL.Entities;
using Catalog.BLL.Interfaces.Repository;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Catalog.DAL.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connString;

        public ProductRepository(string connString)
        {
            _connString = connString;
        }
        public Product Get(long id)
        {
            using var connection = new SqliteConnection(_connString);
            connection.Open();

            var sql = @"
                SELECT id, image, category CategoryId, price, amount, name, description
                FROM products
                WHERE id = @id";
            var product = connection.QuerySingle<Product>(sql, new { id });

            return product;
        }

        public List<Product> GetAll(long offset, long limit, long? categoryId = null)
        {
            using var connection = new SqliteConnection(_connString);
            connection.Open();

            var command = connection.CreateCommand();
            var sql = @$"
                SELECT id, image, category CategoryId, price, amount, name, description
                FROM Products
                ORDER BY id
                {(categoryId.HasValue ? "WHERE CategoryId = @categoryId" : "")}
                Limit @offset,@limit";
            var product = connection.Query<Product>(sql, new { categoryId, offset, limit });

            return product.ToList();
        }

        public long Add(Product product)
        {
            using var connection = new SqliteConnection(_connString);
            connection.Open();

            var sql = @"
                INSERT INTO Products (image, category, price, amount, name, description)
                VALUES (@image, @category, @price, @amount, @name, @description)
                RETURNING id";
            var parameters = new
            {
                name = product.Name,
                description = product.Description,
                image = product.Image,
                category = product.CategoryId,
                price = product.Price,
                amount = product.Amount
            };

            try
            {
                return connection.ExecuteScalar<long>(sql, parameters);
            }
            catch (SqliteException)
            {
                return -1;
            }
        }

        public long Update(Product product)
        {
            using var connection = new SqliteConnection(_connString);
            connection.Open();

            var command = connection.CreateCommand();
            var sql = @"
                UPDATE products
                SET
                    image = @image,
                    category = @category,
                    price = @price,
                    amount = @amount,
                    name = @name,
                    description = @description
                WHERE id = @id";
            var parameters = new
            {
                id = product.Id,
                name = product.Name,
                description = product.Description,
                image = product.Image,
                category = product.CategoryId,
                price = product.Price,
                amount = product.Amount
            };

            return connection.Execute(sql, parameters);
        }

        public long Delete(long id)
        {
            using var connection = new SqliteConnection(_connString);
            connection.Open();

            var command = connection.CreateCommand();
            var sql = "DELETE FROM products WHERE id = @id";

            return connection.Execute(sql, new { id });
        }

        public long GetProductCount()
        {
            using var connection = new SqliteConnection(_connString);
            connection.Open();

            var command = connection.CreateCommand();
            var sql = "SELECT count(1) FROM products";

            return connection.QuerySingle<long>(sql);
        }

        public long DeleteForCategory(long category)
        {
            using var connection = new SqliteConnection(_connString);
            connection.Open();

            var command = connection.CreateCommand();
            var sql = "DELETE FROM products WHERE category = @category";

            return connection.Execute(sql, new { category });
        }
    }
}
