using AutoMapper;
using Ecommerce.Application.Handlers.City.Commands;
using Ecommerce.Application.Handlers.City.Queries;
using Ecommerce.Application.Handlers.Colors.Commands;
using Ecommerce.Application.Handlers.Colors.Queries;
using Ecommerce.Domain.Identity.Permissions;
using Ecommerce.Web.Mvc.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Ecommerce.Web.Mvc.Controllers
{
    [Authorize]
    public class CityController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public CityController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize(Permissions.Permissions_City_View)]
        public IActionResult Index() => View();

        [Authorize(Permissions.Permissions_City_View)]
        public async Task<IActionResult> RenderView()
        {
            var paging = new PageRequest().GetPageResponse(Request);
            var result = await _mediator.Send(new GetCitiesWithPagingQuery { page = paging.PageIndex, length = paging.Length, searchValue = paging.SearchValue, sortColumn = paging.SortColumnName, sortOrder = paging.SortOrder });
            var jsonData = new { data = result.Items, draw = paging.Draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount };
            return Json(jsonData);
        }

        [Authorize(Permissions.Permissions_City_Create)]
        public IActionResult Create() => View();

        [Authorize(Permissions.Permissions_City_Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCityCommand command)
        {
            var isCityExists = await _mediator.Send(new IsCityNameExistQuery { Name = command.Name });
            if (isCityExists) ModelState.AddModelError(string.Empty, "City Name already exist.");

            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(command);
                if (response.Succeeded) return RedirectToAction(nameof(Index));
            }
            return View(command);
        }

        [Authorize(Permissions.Permissions_City_Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var city = await _mediator.Send(new GetCityByIdQuery { Id = id });
            if (city == null)
            {
                return NotFound();
            }
            var cityCommand = _mapper.Map<UpdateCityCommand>(city);
            return View(cityCommand);
        }

        [Authorize(Permissions.Permissions_City_Edit)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateCityCommand command)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }

        [Authorize(Permissions.Permissions_City_Delete)]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteCityCommand { Id = id });
            return Json(response);
        }
    }
}
