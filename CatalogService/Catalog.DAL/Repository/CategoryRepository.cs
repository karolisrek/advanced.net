using Catalog.BLL.Entities;
using Catalog.BLL.Interfaces.Repository;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Catalog.DAL.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _connString;

        public CategoryRepository(string connString)
        {
            _connString = connString;
        }

        public Category Get(long id)
        {
            using var connection = new SqliteConnection(_connString);
            connection.Open();

            var sql = @"
                SELECT id, name, image, parent_category parentCategory
                FROM categories
                WHERE id = @id";
            var categories = connection.QuerySingle<Category>(sql, new { id });

            return categories;
        }

        public List<Category> GetAll(long? parentCategoryId = null)
        {
            using var connection = new SqliteConnection(_connString);
            connection.Open();

            var command = connection.CreateCommand();
            var sql = @$"
                SELECT id, name, image, parent_category parentCategory
                FROM categories
                {(parentCategoryId is null ?  "" : "WHERE parentCategory = @parentCategory")}";
            var categories = connection.Query<Category>(sql, new { parentCategory = parentCategoryId });

            return categories.ToList();
        }

        public long Add(Category catalog)
        {
            using var connection = new SqliteConnection(_connString);
            connection.Open();

            var sql = @"
                INSERT INTO categories (name, image, parent_category)
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

        public long Update(Category catalog)
        {
            using var connection = new SqliteConnection(_connString);
            connection.Open();

            var command = connection.CreateCommand();
            var sql = @"
                UPDATE categories
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
            using var connection = new SqliteConnection(_connString);
            connection.Open();

            var command = connection.CreateCommand();
            var sql = "DELETE FROM categories WHERE id = @id";

            return connection.Execute(sql, new { id });
        }
    }
}
