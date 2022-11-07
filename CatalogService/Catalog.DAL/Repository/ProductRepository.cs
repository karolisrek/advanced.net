using Dapper;
using Microsoft.Data.Sqlite;

namespace Catalog.DAL.Repository
{
    public class ProductRepository
    {
        const string connString = @"Data Source=C:\Users\Karolis_Rekasius\Desktop\Advanced .net\CatalogService\CatalogDb\catalog.db";

        public Models.Product Get(long id)
        {
            using var connection = new SqliteConnection(connString);
            connection.Open();

            var sql = @"
                SELECT id, image, category CategoryId, price, amount, name, description
                FROM products
                WHERE id = @id";
            var product = connection.QuerySingle<Models.Product>(sql, new { id });

            return product;
        }

        public List<Models.Product> GetAll(long categoryId)
        {
            using var connection = new SqliteConnection(connString);
            connection.Open();

            var command = connection.CreateCommand();
            var sql = @$"
                SELECT id, image, category CategoryId, price, amount, name, description
                FROM Products
                WHERE CategoryId = @categoryId";
            var product = connection.Query<Models.Product>(sql, new { categoryId });

            return product.ToList();
        }

        public long Add(Models.Product product)
        {
            using var connection = new SqliteConnection(connString);
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

        public long Update(Models.Product product)
        {
            using var connection = new SqliteConnection(connString);
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
            using var connection = new SqliteConnection(connString);
            connection.Open();

            var command = connection.CreateCommand();
            var sql = "DELETE FROM products WHERE id = @id";

            return connection.Execute(sql, new { id });
        }
    }
}
