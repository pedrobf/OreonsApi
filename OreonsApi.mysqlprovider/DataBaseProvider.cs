using Dapper;
using MySql.Data.MySqlClient;
using OreonsApi.core.Models;
using OreonsApi.core.Models.DataBase;
using OreonsApi.core.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OreonsApi.mysqlprovider
{
    public class DataBaseProvider : IDataBaseProvider
    {
        #region Product

        public async Task CreateProduct(Product product)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionMySql.ConnectionString))
                {
                    #region Query - Inser Product

                    await conn.ExecuteScalarAsync(
                       @"  INSERT INTO `products` (" +
                        "  `id`, " +
                        "  `name`, " +
                        "  `description`, " +
                        "  `price`, " +
                        "  `id_category`, " +
                        "  `id_subcategory`) " +
                        "  VALUES ( " +
                        "  @ID, " +
                        "  @NAME, " +
                        "  @DESCRIPTION, " +
                        "  @PRICE, " +
                        "  @ID_CATEGORY, " +
                        "  @ID_SUBCATEGORY); ",
                       new
                       {
                           ID = new Random().Next().ToString(),
                           NAME = product.Name,
                           DESCRIPTION = product.Description,
                           PRICE = product.SellingPrice,
                           ID_CATEGORY = product.CategoryId,
                           ID_SUBCATEGORY = product.ChildrenCategoryId
                       });

                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateProduct(string id, Product product)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionMySql.ConnectionString))
                {
                    #region Query - Update Product

                    await conn.QueryAsync(
                        @" UPDATE `products` " +
                         " SET `name` = @name, " +
                         "     `description` = @description," +
                         "     `price` = @price, " +
                         "     `id_category` = @id_category, " +
                         "     `id_subcategory` = @id_subcategory " +
                         " WHERE id = @id;",
                        new
                        {
                            name = product.Name,
                            description = product.Description,
                            price = product.SellingPrice,
                            id_category = product.CategoryId,
                            id_subcategory = product.ChildrenCategoryId,
                            id
                        });

                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionMySql.ConnectionString))
                {
                    #region Query - Select All Products

                    return (await conn.QueryAsync<Product>
                           (GetAllProduct())).Distinct().ToList();

                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Product> GetProductById(string id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionMySql.ConnectionString))
                {
                    return (await conn.QueryAsync<Product>
                        (GetById(id), new { id })).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteProduct(string id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionMySql.ConnectionString))
                {
                    #region Query - Delete Product

                    await conn.ExecuteAsync(
                            @"  DELETE FROM products " +
                             "  WHERE id = @id; ",
                            new { id });

                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string GetAllProduct()
        {
            var sql = new StringBuilder();

            sql.AppendLine(" SELECT ");
            sql.AppendLine(" p.id AS Id, ");
            sql.AppendLine(" p.name AS `Name`,");
            sql.AppendLine(" p.description AS Description, ");
            sql.AppendLine(" p.price AS SellingPrice, ");
            sql.AppendLine(" c.id AS CategoryId, ");
            sql.AppendLine(" c.description AS CategoryName ");
            sql.AppendLine(" FROM products p ");
            sql.AppendLine(" INNER JOIN category c ON p.id_category = c.id ");

            return sql.ToString();
        }

        private string GetById(string id)
        {
            var sql = new StringBuilder();

            sql.AppendLine(" SELECT ");
            sql.AppendLine(" p.id AS Id, ");
            sql.AppendLine(" p.name AS Name, ");
            sql.AppendLine(" p.description AS Description, ");
            sql.AppendLine(" p.price AS SellingPrice, ");
            sql.AppendLine(" p.id_subcategory AS ChildrenCategoryId, ");
            sql.AppendLine(" c.id AS CategoryId, ");
            sql.AppendLine(" c.description AS CategoryName, ");
            sql.AppendLine(" s.description AS SubCategoryName, ");
            sql.AppendLine(" s.level AS Level ");
            sql.AppendLine(" FROM products p ");
            sql.AppendLine(" INNER JOIN category c ON p.id_category = c.id ");
            sql.AppendLine(" LEFT outer JOIN subcategory s ON p.id_subcategory = s.id ");
            sql.AppendLine(" WHERE p.id = @id ");
            sql.AppendLine(" ORDER BY s.level DESC LIMIT 1; ");

            return sql.ToString();
        }

        #endregion

        #region Category

        public async Task<string> CreateCategory(Category category)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionMySql.ConnectionString))
                {
                    #region Query - Inser Category

                    return (await conn.ExecuteScalarAsync<string>(
                        @"  INSERT INTO `category` ( " +
                         "  `id`, " +
                         "  `description`) " +
                         "  VALUES ( " +
                         "  @ID, " +
                         "  @DESCRIPTION); " +
                         " SELECT id AS IdString FROM category WHERE id = @ID;",
                        new
                        {
                            ID = new Random().Next().ToString(),
                            DESCRIPTION = category.Description
                        }));

                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CreateSubCategory(SubCategory subcategory)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionMySql.ConnectionString))
                {
                    #region Query - Inser SubCategory

                    await conn.ExecuteScalarAsync(
                        @"  INSERT INTO `subcategory` ( " +
                         "  `id`, " +
                         "  `description`, " +
                         "  `level`, " +
                         "  `id_category`) " +
                         "  VALUES ( " +
                         "  @ID, " +
                         "  @DESCRIPTION, " +
                         "  @LEVEL, " +
                         "  @ID_CATEGORY); ",
                        new
                        {
                            ID = subcategory.SubCategoryId,
                            DESCRIPTION = subcategory.Description,
                            LEVEL = subcategory.Level,
                            ID_CATEGORY = subcategory.CategoryId
                        });

                    #endregion
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateCategory(string id, string description)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionMySql.ConnectionString))
                {
                    #region Query - Update Category

                    await conn.QueryAsync(
                        @"  UPDATE `category` " +
                         "  SET `description` = @description " +
                         "  WHERE id = @id;",
                        new { description, id });

                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateSubCategory(string id, string description, int level)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionMySql.ConnectionString))
                {
                    #region Query - Update SubCategory

                    await conn.QueryAsync(
                        @"  UPDATE `subcategory` " +
                         "  SET `description` = @description " +
                         "  WHERE id = @id " +
                         "  AND `level` = @level;",
                        new { description, id, level });

                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            try
            {
                IEnumerable<Category> categories;

                using (MySqlConnection conn = new MySqlConnection(ConnectionMySql.ConnectionString))
                {
                    #region Query - Select All Categories and childrens

                    var subcategories = new Dictionary<string, Category>();

                    categories = (await conn.QueryAsync<Category, SubCategory, Category>
                                 (GetAll(null), (catM, subM) =>
                                 {
                                     Category result;

                                     if (!subcategories.TryGetValue(catM.Id, out result))
                                     {
                                         result = catM;
                                         result.ChildrensCategory = new List<SubCategory>();
                                         subcategories.Add(result.Id, result);
                                     }

                                     if (result.ChildrensCategory.Count() == 0)
                                         result.ChildrensCategory.Add(subM);

                                     else
                                     {
                                         result.ChildrensCategory.Any(s => s.SubCategoryId.Equals(subM.SubCategoryId));
                                         result.ChildrensCategory.Add(subM);
                                     }
                                     return result;
                                 }, splitOn: "SubCategoryId")).Distinct().ToList();

                    #endregion
                }

                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Category> GetCategoryById(string id)
        {
            try
            {

                using (MySqlConnection conn = new MySqlConnection(ConnectionMySql.ConnectionString))
                {
                    #region Query - Select Categories and childrens by Id

                    var subcategories = new Dictionary<string, Category>();

                    return (await conn.QueryAsync<Category, SubCategory, Category>
                                 (GetAll(id), (catM, subM) =>
                                 {
                                     Category result;

                                     if (!subcategories.TryGetValue(catM.Id, out result))
                                     {
                                         result = catM;
                                         result.ChildrensCategory = new List<SubCategory>();
                                         subcategories.Add(result.Id, result);
                                     }

                                     if (result.ChildrensCategory.Count() == 0)
                                         result.ChildrensCategory.Add(subM);

                                     else
                                     {
                                         result.ChildrensCategory.Any(s => s.SubCategoryId.Equals(subM.SubCategoryId));
                                         result.ChildrensCategory.Add(subM);
                                     }
                                     return result;
                                 }, new { id }, splitOn: "SubCategoryId")).FirstOrDefault();

                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> GetRelatedProducts(string id)
        {
            try
            {
                int relatedProducts;

                using (MySqlConnection conn = new MySqlConnection(ConnectionMySql.ConnectionString))
                {
                    #region Query - Get related products in categories

                    relatedProducts = (await conn.ExecuteScalarAsync<int>(
                            @"  SELECT COUNT(*)" +
                             "  FROM category c " +
                             "  INNER JOIN products p ON c.id = p.id_category " +
                             "  WHERE c.id = @id;",
                            new { id }));

                    #endregion
                }
                return relatedProducts;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteCategory(string id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionMySql.ConnectionString))
                {
                    #region Query - Delete Category and SubCategories

                    await conn.ExecuteAsync(
                            @"  DELETE FROM category " +
                             "  WHERE id = @id; ",
                            new { id });

                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteSubCategory(string id, int level)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionMySql.ConnectionString))
                {
                    #region Query - Delete SubCategories

                    await conn.ExecuteAsync(
                            @"  DELETE FROM subcategory " +
                             "  WHERE id = @id " +
                             "  AND level = @level; ",
                            new { id, level });

                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string GetAll(string id = null)
        {
            var sql = new StringBuilder();

            sql.AppendLine(@" SELECT ");
            sql.AppendLine("c.id AS Id,");
            sql.AppendLine("c.description AS Description,");
            sql.AppendLine("s.id AS SubCategoryId,");
            sql.AppendLine("s.id_category AS CategoryId,");
            sql.AppendLine("s.description AS Description,");
            sql.AppendLine("s.`level` AS `Level`");
            sql.AppendLine("FROM category c");
            sql.AppendLine("LEFT JOIN subcategory s ON c.id = s.id_category");

            if (id != null)
                sql.AppendLine("WHERE c.id = @id");

            sql.AppendLine("ORDER BY s.`level`;");

            return sql.ToString();
        }

        #endregion
    }
}
