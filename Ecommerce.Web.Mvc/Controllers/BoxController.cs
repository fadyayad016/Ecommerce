using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Dto.Box;
using Ecommerce.Application.Handlers.Barcodes.Commands;
using Ecommerce.Application.Handlers.BarCodes.Commands;
using Ecommerce.Application.Handlers.BarCodes.Queries;
using Ecommerce.Application.Handlers.Boxes.Commands;
using Ecommerce.Application.Handlers.Boxes.Queries;
using Ecommerce.Application.Handlers.Currency.Queries;
using Ecommerce.Domain.Identity.Permissions;
using Ecommerce.Web.Mvc.Helpers;
using Ecommerce.Web.Mvc.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.Web.Mvc.Controllers
{
    [Authorize]
    public class BoxController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BoxController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [Authorize(Permissions.Permissions_Box_View)]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Permissions.Permissions_Box_View)]
        public async Task<IActionResult> RenderView()
        {
            var paging = new PageRequest().GetPageResponse(Request);
            var result = await _mediator.Send(new GetBoxWithPagingQuery { page = paging.PageIndex, length = paging.Length, searchValue = paging.SearchValue, sortColumn = paging.SortColumnName, sortOrder = paging.SortOrder });

            var jsonData = new { data = result.Items, draw = paging.Draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount };
            return Json(jsonData);
        }

        [Authorize(Permissions.Permissions_Box_Create)]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Permissions_Box_Create)]
        public async Task<IActionResult> Create(CreateBoxCommand command)
        {
            if (!ModelState.IsValid) return View(command);
            var response = await _mediator.Send(command);
            if (response.Succeeded) return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, response.Message);
            return View(command);
        }


        [Authorize(Permissions.Permissions_Box_Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _mediator.Send(new GetBoxWithBoxCurrencyByIdQuery { Id = id });

            var currencies = await _mediator.Send(new GetCurrenciesQuery());

            if (response == null) return NotFound();

            ViewData["CurrencyId"] = new SelectList(currencies.Where(c => c.IsActive), "Id", "Name");

            return View(response);
        }

        [HttpPost]
        [Authorize(Permissions.Permissions_Box_Edit)]
        public async Task<IActionResult> UpdateBox(BoxesDto data)
        {
            if (data == null)
            {
                return Json(new { success = false, message = "Invalid Box data." });
            }

            var response = await _mediator.Send(new UpdateBoxWithBoxCurrencyCommand { BoxesDto = data });

            if (response.Succeeded)
            {
                return Json(new { success = true, data = response.Data });
            }
            return Json(new { success = false, message = "Failed to update box." });
        }

        [HttpPost]
        [Authorize(Permissions.Permissions_Box_Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteBoxCommand { Id = id });
            return Json(response);
        }

        [AllowAnonymous]
        public async Task<IActionResult> getCurrency()
        {
            var res = await _mediator.Send(new GetCurrenciesQuery());
            return Json(res.Where(c => c.IsActive).Select(o => new DropdownSelectVM { Value = o.Id, Text = o.Name }));
        }

    }
}
