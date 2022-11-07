using Dapper;
using Microsoft.Data.Sqlite;

namespace Catalog.DAL.Repository
{
    public class CatalogRepository
    {
        const string connString = @"Data Source=C:\Users\Karolis_Rekasius\Desktop\Advanced .net\CatalogService\CatalogDb\catalog.db";

        public Models.Catalog Get(long id)
        {
            using var connection = new SqliteConnection(connString);
            connection.Open();

            var sql = @"
                SELECT id, name, image, parent_category parentCategory
                FROM catalogs
                WHERE id = @id";
            var product = connection.QuerySingle<Models.Catalog>(sql, new { id });

            return product;
        }

        public List<Models.Catalog> GetAll(long? parentCategoryId = null)
        {
            using var connection = new SqliteConnection(connString);
            connection.Open();

            var command = connection.CreateCommand();
            var sql = @$"
                SELECT id, name, image, parent_category parentCategory
                FROM catalogs
                WHERE parentCategory {(parentCategoryId is null ?  "is NULL" : "= @parentCategory")}";
            var product = connection.Query<Models.Catalog>(sql, new { parentCategory = parentCategoryId });

            return product.ToList();
        }

        public long Add(Models.Catalog catalog)
        {
            using var connection = new SqliteConnection(connString);
            connection.Open();

            var sql = @"
                INSERT INTO catalogs (name, image, parent_category)
                VALUES (@name, @image, @parentCategory)
                RETURNING Id";
            var parameters = new
            {
                name = catalog.Name,
                image = catalog.Image,
                parentCategory = catalog.ParentCategory
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

        public long Update(Models.Catalog catalog)
        {
            using var connection = new SqliteConnection(connString);
            connection.Open();

            var command = connection.CreateCommand();
            var sql = @"
                UPDATE catalogs
                SET name = @name, image = @image, parent_category = @parentCategory
                WHERE id = @id";
            var parameters = new
            {
                name = catalog.Name,
                image = catalog.Image,
                parentCategory = catalog.ParentCategory,
                id = catalog.Id,
            };

            return connection.Execute(sql, parameters);
        }

        public long Delete(long id)
        {
            using var connection = new SqliteConnection(connString);
            connection.Open();

            var command = connection.CreateCommand();
            var sql = "DELETE FROM catalogs WHERE id = @id";

            return connection.Execute(sql, new { id });
        }
    }
}
