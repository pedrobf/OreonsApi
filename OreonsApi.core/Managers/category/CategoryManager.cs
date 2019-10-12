using Microsoft.Extensions.Logging;
using OreonsApi.core.Models;
using OreonsApi.core.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OreonsApi.core.Managers.category
{
    public class CategoryManager : ICategoryManager
    {
        #region Objects
        private IDataBaseProvider _dataBaseProvider { get; set; }
        private ILogger Logger { get; set; }
        #endregion

        #region Constructor
        public CategoryManager(IDataBaseProvider dataBaseProvider, ILoggerFactory logger)
        {
            _dataBaseProvider = dataBaseProvider;
            Logger = logger.CreateLogger("CategoriesManager");
        }
        #endregion

        public async Task CreateCategory(Category category)
        {
            try
            {
                Logger.LogInformation($"[{DateTime.Now:s}] - Creating Category with description [{category.Description}] and id [{category.Id}] to Oreons");

                if (category.ChildrensCategory.Any() && category.ChildrensCategory.Count() > 0)
                {
                    var resultCateogry = await _dataBaseProvider.CreateCategory(category);
                    var subCategoryId = new Random().Next().ToString();

                    foreach (var children in category.ChildrensCategory)
                    {
                        children.CategoryId = resultCateogry;

                        children.SubCategoryId = subCategoryId;

                        await _dataBaseProvider.CreateSubCategory(children);
                    }
                }
                else
                {
                    var result = await _dataBaseProvider.CreateCategory(category);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"[{DateTime.Now:s}] - Error when creating Category to Oreons \n MSG:{ex.Message} \n EXCEPTION:{ex.StackTrace}");
                throw new Exception("Erro ao criar categoria.");
            }
        }

        public async Task UpdateCategory(string id, Category category)
        {
            try
            {
                Logger.LogInformation($"[{DateTime.Now:s}] - Updating Category with description [{category.Description}] and id [{category.Id}] to Oreons");

                var returnSubCategory = (await GetCategoryById(id)).ChildrensCategory;

                if (returnSubCategory.FirstOrDefault() == null)
                {
                    foreach (var childrens in category.ChildrensCategory)
                    {
                        childrens.SubCategoryId = new Random().Next().ToString();
                        childrens.CategoryId = id;

                        await _dataBaseProvider.CreateSubCategory(childrens);
                    }
                }

                if (returnSubCategory.Count() != category.ChildrensCategory.Count())
                {
                    for (var x = returnSubCategory.Count(); x < category.ChildrensCategory.Count(); x++)
                    {
                        category.ChildrensCategory[x].SubCategoryId = new Random().Next().ToString();
                        category.ChildrensCategory[x].CategoryId = id;

                        await _dataBaseProvider.CreateSubCategory(category.ChildrensCategory[x]);
                    }
                }
                    

                if (returnSubCategory.Count() == category.ChildrensCategory.Count())
                {
                    foreach (var childrens in category.ChildrensCategory)
                    {
                        await _dataBaseProvider.UpdateSubCategory(id, childrens.Description, childrens.Level);
                    }
                }
                else
                    await _dataBaseProvider.UpdateCategory(id, category.Description);
            }
            catch (Exception ex)
            {
                Logger.LogError($"[{DateTime.Now:s}] - Error when updating Category to Oreons \n MSG:{ex.Message} \n EXCEPTION:{ex.StackTrace}");
                throw new Exception("Erro ao atualizar categoria.");
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            try
            {
                Logger.LogInformation($"Getting all categories.");

                return (await _dataBaseProvider.GetAllCategories());
            }
            catch (Exception ex)
            {
                Logger.LogError($"[{DateTime.Now:s}] - Was not possible to list all categories.\n MSG: {ex.Message} \n STACKTRACE: {ex.StackTrace}");
                throw new Exception("Erro ao listar todas categorias.");
            }
        }

        public async Task<Category> GetCategoryById(string id)
        {
            try
            {
                Logger.LogInformation($"Getting category.");

                var result = await _dataBaseProvider.GetCategoryById(id);

                if (result == null)
                    throw new KeyNotFoundException("Categoria não encontrada");

                return result;
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError($"[{DateTime.Now:s}] - Was not possible to get category.\n MSG: {ex.Message} \n STACKTRACE: {ex.StackTrace}");
                throw new Exception("Erro ao buscar categoria.");
            }
        }

        public async Task DeleteCategory(string id)
        {
            try
            {
                var category = await GetCategoryById(id);

                if (category == null)
                    throw new KeyNotFoundException("Categoria não encontrada");

                var relatedProducts = await _dataBaseProvider.GetRelatedProducts(id);

                if (relatedProducts > 0)
                    throw new ArgumentException("Esta categoria já esta vinculada a produto");

                await _dataBaseProvider.DeleteCategory(id);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError($"{DateTime.Now:s} - Error when Deleting category by Id: {id} \n MSG:{ex.Message} \n EXCEPTION:{ex.StackTrace}");
                throw new Exception("Erro ao remover a categoria.");
            }
        }

        public async Task DeleteChildrenCategory(string id, int level)
        {
            try
            {
                await _dataBaseProvider.DeleteSubCategory(id, level);
            }
            catch (Exception ex)
            {
                Logger.LogError($"{DateTime.Now:s} - Error when Deleting subcategory by Id: {id} \n MSG:{ex.Message} \n EXCEPTION:{ex.StackTrace}");
                throw new Exception("Erro ao remover a subcategoria.");
            }
        }
    }
}
