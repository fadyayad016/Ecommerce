using AutoMapper;
using Ecommerce.Application.Handlers.Brand.Commands;
using Ecommerce.Application.Handlers.Brand.Queries;
using Ecommerce.Domain.Identity.Permissions;
using Ecommerce.Web.Mvc.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Ecommerce.Web.Mvc.Controllers
{
    [Authorize]
    public class BrandsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public BrandsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize(Permissions.Permissions_Brand_View)]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Permissions.Permissions_Brand_View)]
        public async Task<IActionResult> RenderView()
        {
            var paging = new PageRequest().GetPageResponse(Request);
            var result = await _mediator.Send(new GetBrandsWithPagingQuery { page = paging.PageIndex, length = paging.Length, searchValue = paging.SearchValue, sortColumn = paging.SortColumnName, sortOrder = paging.SortOrder });

            var jsonData = new { data = result.Items, draw = paging.Draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount };
            return Json(jsonData);
        }

        [Authorize(Permissions.Permissions_Brand_Create)]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Permissions_Brand_Create)]
        public async Task<IActionResult> Create(CreateBrandCommand command)
        {
            if (!ModelState.IsValid) return View(command);
            var response = await _mediator.Send(command);
            if (response.Succeeded) return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, response.Message);
            return View(command);
        }

        [Authorize(Permissions.Permissions_Brand_Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var brand = await _mediator.Send(new GetBrandByIdQuery { Id = id });
            if (brand == null)
            {
                return NotFound();
            }
            var updateBrandCommand = _mapper.Map<UpdateBrandCommand>(brand);
            return View(updateBrandCommand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Permissions_Brand_Edit)]
        public async Task<IActionResult> Edit(UpdateBrandCommand command)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }

        [HttpPost]
        [Authorize(Permissions.Permissions_Brand_Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteBrandCommand { Id = id });
            return Json(response);
        }
    }

}
