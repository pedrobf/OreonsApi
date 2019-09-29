using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace OreonsApi.Controllers
{
    /// <summary>
    /// Controller Base
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly IMapper _mapper;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        public BaseController(IMapper mapper)
        {
            this._mapper = mapper;
        }
    }
}