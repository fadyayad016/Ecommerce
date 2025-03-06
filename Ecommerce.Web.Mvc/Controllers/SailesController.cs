using AutoMapper;
using Ecommerce.Application.Handlers.Categories.Commands;
using Ecommerce.Application.Handlers.Categories.Queries;
using Ecommerce.Domain.Identity.Permissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.Web.Mvc.Controllers
{
    public class SailesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public SailesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

      
        public async Task<IActionResult> Create()
        {
          
            return View();
        }
    }
}
