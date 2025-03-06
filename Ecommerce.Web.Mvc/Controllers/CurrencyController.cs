using AutoMapper;
using Ecommerce.Application.Handlers.City.Commands;
using Ecommerce.Application.Handlers.City.Queries;
using Ecommerce.Application.Handlers.Currencies.Commands;
using Ecommerce.Application.Handlers.Currencies.Queries;
using Ecommerce.Application.Handlers.Stores.Queries;
using Ecommerce.Domain.Identity.Permissions;
using Ecommerce.Web.Mvc.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Mvc.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public CurrencyController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize(Permissions.Permissions_Currency_View)]
        public IActionResult Index() => View();

        [Authorize(Permissions.Permissions_Currency_View)]
        public async Task<IActionResult> RenderView()
        {
            var paging = new PageRequest().GetPageResponse(Request);
            var result = await _mediator.Send(new GetCurrenciesWithPagingQuery { page = paging.PageIndex, length = paging.Length, searchValue = paging.SearchValue, sortColumn = paging.SortColumnName, sortOrder = paging.SortOrder });
            var jsonData = new { data = result.Items, draw = paging.Draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount };
            return Json(jsonData);
        }
        [Authorize(Permissions.Permissions_Currency_Create)]
        public IActionResult Create() => View();

        [Authorize(Permissions.Permissions_Currency_Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCurrencyCommand command)
        {
            var isCurrencyExists = await _mediator.Send(new IsCurrencyNameExistQuery { Name = command.Name });
            if (isCurrencyExists) ModelState.AddModelError(string.Empty, "Currency Name already exist.");

            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(command);
                if (response.Succeeded) return RedirectToAction(nameof(Index));
            }
            return View(command);
        }

        [Authorize(Permissions.Permissions_Currency_Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var currency = await _mediator.Send(new GetCurrencyByIdQuery { Id = id });
            if (currency == null)
            {
                return NotFound();
            }
            var currencyCommand = _mapper.Map<UpdateCurrencyCommand>(currency);
            return View(currencyCommand);
        }

        [Authorize(Permissions.Permissions_Currency_Edit)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateCurrencyCommand command)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }

        [Authorize(Permissions.Permissions_Currency_Delete)]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteCurrencyCommand { Id = id });
            return Json(response);
        }
    }
}
