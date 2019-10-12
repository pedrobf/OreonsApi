using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OreonsApi.core.Managers.category;
using OreonsApi.core.Models;
using OreonsApi.Filters;
using OreonsApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace OreonsApi.Controllers
{
    /// <summary>
    /// Controller de Categoria
    /// </summary>
    /// 
    [Route("categories")]
    public class CategoryController : BaseController
    {
        #region Objects
        private readonly ICategoryManager _categoryManager;
        #endregion

        #region Constructor
        public CategoryController(ICategoryManager categoryManager, IMapper mapper) : base(mapper)
        {
            this._categoryManager = categoryManager;
        }
        #endregion

        [HttpPost]
        [ValidateModel]
        [Produces(typeof(CategoryDTO))]
        [SwaggerResponse((int)HttpStatusCode.Created, Description = "Inserido com sucesso", Type = typeof(CategoryDTO))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Requisição mal-formatada")]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Description = "Conflito")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Erro na API")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO category)
        {
            var newCategory = _mapper.Map<Category>(category);
            await _categoryManager.CreateCategory(newCategory);
            return Ok();
        }

        [HttpGet("all")]
        [Produces(typeof(List<CategoryDTO>))]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "OK", Type = typeof(List<CategoryDTO>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Recurso não encontrado")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Erro na API")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryManager.GetAllCategories();

            return Ok((result));
        }

        [HttpGet("{id}")]
        [Produces(typeof(List<CategoryDTO>))]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "OK", Type = typeof(List<CategoryDTO>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Recurso não encontrado")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Erro na API")]
        public async Task<IActionResult> GetCategory(string id)
        {
            var result = await _categoryManager.GetCategoryById(id);

            return Ok(result);
        }

        [HttpPut("update/{id}")]
        [ValidateModel]
        [Produces(typeof(CategoryDTO))]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Alterado com sucesso", Type = typeof(CategoryDTO))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Requisição mal-formatada")]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Description = "Conflito")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Erro na API")]
        public async Task<IActionResult> UpdateCategory(string id, [FromBody] CategoryDTO category)
        {
            await _categoryManager.UpdateCategory(id, _mapper.Map<Category>(category));

            return Ok();
        }

        [HttpDelete("{id}")]
        [Produces(typeof(OkResult))]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Removido com sucesso")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Recurso não encontrado")]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, Description = "Recurso não autorizado")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Erro na API")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
           await _categoryManager.DeleteCategory(id);

            return Ok();
        }

        [HttpDelete("subcategory/{id}/{level}")]
        [Produces(typeof(OkResult))]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Removido com sucesso")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Recurso não encontrado")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Erro na API")]
        public IActionResult DeleteSubCategory(string id, int level)
        {
            _categoryManager.DeleteChildrenCategory(id, level);

            return Ok();
        }
    }
}