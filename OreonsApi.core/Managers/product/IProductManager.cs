using OreonsApi.core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OreonsApi.core.Managers.product
{
    public interface IProductManager
    {
        #region Create
        /// <summary>
        /// Insere um objeto do tipo Produto no banco de dados
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task CreateProduct(Product product);
        #endregion

        #region Update
        /// <summary>
        /// Atualiza objeto do tipo Produto no banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        Task UpdateProduct(string id, Product product);
        #endregion

        #region Read
        /// <summary>
        /// Retorna todos os objetos do tipo Produto cadastrados no banco de dados
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Product>> GetAllProducts();

        /// <summary>
        /// Retorna objeto do tipo Produto por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Product> GetProductById(string id);
        #endregion

        #region Delete
        /// <summary>
        /// Deleta Produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteProduct(string id);
        #endregion
    }
}
