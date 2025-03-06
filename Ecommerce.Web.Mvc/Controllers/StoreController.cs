using AutoMapper;
using Ecommerce.Application.Handlers.Categories.Commands;
using Ecommerce.Application.Handlers.Categories.Queries;
using Ecommerce.Application.Handlers.City.Queries;
using Ecommerce.Application.Handlers.Stores.Commands;
using Ecommerce.Application.Handlers.Stores.Queries;
using Ecommerce.Domain.Identity.Permissions;
using Ecommerce.Web.Mvc.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.Web.Mvc.Controllers
{
    [Authorize]
    public class StoreController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public StoreController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [Authorize(Permissions.Permissions_Store_View)]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Permissions.Permissions_Store_View)]
        public async Task<IActionResult> RenderView()
        {
            var paging = new PageRequest().GetPageResponse(Request);
            var result = await _mediator.Send(new GetStoresWithPagingQuery { page = paging.PageIndex, length = paging.Length, searchValue = paging.SearchValue, sortColumn = paging.SortColumnName, sortOrder = paging.SortOrder });

            var jsonData = new { data = result.Items, draw = paging.Draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount };
            return Json(jsonData);
        }

        

        [Authorize(Permissions.Permissions_Store_Create)]
        public async Task<IActionResult> Create()
        {
            ViewData["CityId"] = new SelectList(await _mediator.Send(new GetAllActiveCitiesQuery()), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Permissions_Store_Create)]
        public async Task<IActionResult> Create(CreateStoreCommand command)
        {
            if (!ModelState.IsValid) return View(command);
            var response = await _mediator.Send(command);
            if (response.Succeeded) return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, response.Message);

            ViewData["CityId"] = new SelectList(await _mediator.Send(new GetAllActiveCitiesQuery()), "Id", "Name");
            return View(command);
        }


        [Authorize(Permissions.Permissions_Store_Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var store = await _mediator.Send(new GetStoreByIdQuery { Id = id });
            if (store == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(await _mediator.Send(new GetAllActiveCitiesQuery()), "Id", "Name");

            var updateStoreCommand = _mapper.Map<UpdateStoreCommand>(store);
            return View(updateStoreCommand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Permissions_Store_Edit)]
        public async Task<IActionResult> Edit(UpdateStoreCommand command)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(await _mediator.Send(new GetAllActiveCitiesQuery()), "Id", "Name");
            return View(command);
        }
    }
}
