using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OreonsApi.core.Managers.product;
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
    [Route("products")]
    public class ProductController : BaseController
    {
        #region Objects
        private readonly IProductManager _productManager;
        #endregion

        #region Constructor
        public ProductController(IProductManager productManager, IMapper mapper) : base(mapper)
        {
            this._productManager = productManager;
        }
        #endregion

        [HttpPost]
        [ValidateModel]
        [Produces(typeof(ProductCreateDTO))]
        [SwaggerResponse((int)HttpStatusCode.Created, Description = "Inserido com sucesso", Type = typeof(ProductDTO))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Requisição mal-formatada")]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Description = "Conflito")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Erro na API")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDTO product)
        {
            await _productManager.CreateProduct(_mapper.Map<Product>(product));
            return Ok();
        }

        [HttpPut("update/{id}")]
        [ValidateModel]
        [Produces(typeof(ProductUpdateDTO))]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Alterado com sucesso", Type = typeof(ProductDTO))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Requisição mal-formatada")]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Description = "Conflito")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Erro na API")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] ProductUpdateDTO product)
        {
            await _productManager.UpdateProduct(id, _mapper.Map<Product>(product));
            return Ok();
        }

        [HttpGet("all")]
        [Produces(typeof(List<ProductDTO>))]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "OK", Type = typeof(List<ProductDTO>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Recurso não encontrado")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Erro na API")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productManager.GetAllProducts();

            return Ok(_mapper.Map<IEnumerable<ProductListDTO>>(result));
        }

        [HttpGet("{id}")]
        [Produces(typeof(List<ProductDTO>))]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "OK", Type = typeof(List<ProductDTO>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Recurso não encontrado")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Erro na API")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var result = await _productManager.GetProductById(id);

            return Ok(_mapper.Map<ProductDetailDTO>(result));
        }

        [HttpDelete("{id}")]
        [Produces(typeof(OkResult))]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Removido com sucesso")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Recurso não encontrado")]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, Description = "Recurso não autorizado")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Erro na API")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productManager.DeleteProduct(id);

            return Ok();
        }
    }
}