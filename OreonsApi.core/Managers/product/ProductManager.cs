using Microsoft.Extensions.Logging;
using OreonsApi.core.Managers.category;
using OreonsApi.core.Models;
using OreonsApi.core.Provider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OreonsApi.core.Managers.product
{
    public class ProductManager : IProductManager
    {
        #region Objects
        private IDataBaseProvider _dataBaseProvider { get; set; }
        private ICategoryManager _categoryManager { get; set; }
        private ILogger Logger { get; set; }
        #endregion

        #region Constructor
        public ProductManager(IDataBaseProvider dataBaseProvider, ICategoryManager categoryManager, ILoggerFactory logger)
        {
            _dataBaseProvider = dataBaseProvider;
            _categoryManager = categoryManager;
            Logger = logger.CreateLogger("ProductsManager");
        }
        #endregion

        public async Task CreateProduct(Product product)
        {
            try
            {
                Logger.LogInformation($"[{DateTime.Now:s}] - Creating Product with name [{product.Name}] and id [{product.Id}] to Oreons");

                if (product.CategoryId == null)
                    throw new ArgumentException("Categoria é obrigatório");

                product.ChildrenCategoryId = (await _categoryManager.GetCategoryById(product.CategoryId)).ChildrensCategory.FirstOrDefault().SubCategoryId;

                //if (!string.IsNullOrEmpty(product.Images.Name) && product.Images.Content != null)
                //{
                //    var
                //}


                await _dataBaseProvider.CreateProduct(product);
            }
            catch (ArgumentException ex)
            {
                Logger.LogWarning($"[{DateTime.Now:s}] - Was not possible to create a Product to Oreons \n MSG:{ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Logger.LogError($"[{DateTime.Now:s}] - Error when creating Product to Oreons \n MSG:{ex.Message} \n EXCEPTION:{ex.StackTrace}");
                throw new Exception("Erro ao criar o produto.");
            }
        }

        public async Task UpdateProduct(string id, Product product)
        {
            try
            {
                Logger.LogInformation($"[{DateTime.Now:s}] - Updating Product with name [{product.Name}] and id [{product.Id}] to Oreons");

                await _dataBaseProvider.UpdateProduct(id, product);
            }
            catch (Exception ex)
            {
                Logger.LogError($"[{DateTime.Now:s}] - Error when updating Product to Oreons \n MSG:{ex.Message} \n EXCEPTION:{ex.StackTrace}");
                throw new Exception("Erro ao atualizar o produto.");
            }
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                Logger.LogInformation($"Getting all products.");

                var result = await _dataBaseProvider.GetAllProducts();

                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError($"[{DateTime.Now:s}] - Was not possible to list all products.\n MSG: {ex.Message} \n STACKTRACE: {ex.StackTrace}");
                throw new Exception("Erro ao listar todas os produtos.");
            }
        }

        public async Task<Product> GetProductById(string id)
        {
            try
            {
                Logger.LogInformation($"Getting product by id = {id}.");

                var result = await _dataBaseProvider.GetProductById(id);

                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError($"[{DateTime.Now:s}] - Was not possible to get product.\n MSG: {ex.Message} \n STACKTRACE: {ex.StackTrace}");
                throw new Exception("Erro ao buscar o produto.");
            }
        }

        public async Task DeleteProduct(string id)
        {
            try
            {
                await _dataBaseProvider.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                Logger.LogError($"{DateTime.Now:s} - Error when Deleting product by Id: {id} \n MSG:{ex.Message} \n EXCEPTION:{ex.StackTrace}");
                throw new Exception("Erro ao remover o produto.");
            }
        }

        private static byte[] ReadFiles(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
