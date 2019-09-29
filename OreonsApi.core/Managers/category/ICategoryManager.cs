using OreonsApi.core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OreonsApi.core.Managers.category
{
    public interface ICategoryManager
    {
        #region Create
        /// <summary>
        /// Insere um objeto do tipo Categoria no banco de dados
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task CreateCategory(Category category);
        #endregion

        #region Update
        /// <summary>
        /// Atualiza um objeto do tipo Categoria no banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        Task UpdateCategory(string id, Category category);
        #endregion

        #region Read
        /// <summary>
        /// Retorna todos os objetos do tipo categoria do banco de dados
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Category>> GetAllCategories();

        /// <summary>
        /// Retorna um objeto específico do tipo categoria do banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<Category> GetCategoryById(string id);
        #endregion

        #region Delete
        /// <summary>
        /// Deleta categoria no banco de dados
        /// </summary>
        /// <param name="id"></param>
        Task DeleteCategory(string id);

        /// <summary>
        /// Deleta sub categoria no banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        Task DeleteChildrenCategory(string id, int level);
        #endregion

    }
}
