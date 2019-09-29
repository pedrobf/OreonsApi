using OreonsApi.core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OreonsApi.core.Provider
{
    public interface IDataBaseProvider
    {
        #region Categories

        #region Insert
        /// <summary>
        /// Insere objeto Categoria no banco de dados
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<string> CreateCategory(Category category);

        /// <summary>
        /// Insere objeto SubCategoria no banco de dados
        /// </summary>
        /// <param name="subcategory"></param>
        /// <returns></returns>
        Task CreateSubCategory(SubCategory subcategory);
        #endregion

        #region Read
        /// <summary>
        /// Retorna todas as categorias do banco de dados
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Category>> GetAllCategories();

        /// <summary>
        /// Retorna categoria pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Category> GetCategoryById(string id);

        /// <summary>
        /// Retorna quantos produtos vinculados a categoria
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> GetRelatedProducts(string id);
        #endregion

        #region Update
        /// <summary>
        /// Atualiza Categoria
        /// </summary>
        /// <param name="id"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        Task UpdateCategory(string id, string description);

        /// <summary>
        /// Atualiza SubCategoria
        /// </summary>
        /// <param name="id"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        Task UpdateSubCategory(string id, string description, int level);
        #endregion

        #region Delete
        /// <summary>
        /// Deleta Categoria e SubCategorias
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task DeleteCategory(string id);

        /// <summary>
        /// Delete SubCategoria
        /// </summary>
        /// <param name="id"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        Task DeleteSubCategory(string id, int level);
        #endregion

        #endregion

        #region Products

        #region Insert
        /// <summary>
        /// Insere objeto Produto no banco de dados
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task CreateProduct(Product product);
        #endregion

        #region Update
        /// <summary>
        /// Atualiza objeto Produto no banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        Task UpdateProduct(string id, Product product);
        #endregion

        #region Read
        /// <summary>
        /// Retorna todos os produtos
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Product>> GetAllProducts();

        /// <summary>
        /// Retorna o producto pelo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Product> GetProductById(string id);
        #endregion

        #region Delete
        /// <summary>
        /// Deleta Producto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteProduct(string id);
        #endregion

        #endregion
    }
}
