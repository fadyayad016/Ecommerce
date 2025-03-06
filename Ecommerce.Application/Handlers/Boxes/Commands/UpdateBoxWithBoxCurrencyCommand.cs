using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Dto.Box;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.Application.Handlers.Boxes.Commands
{
    public class UpdateBoxWithBoxCurrencyCommand : IRequest<Response<string>>
    {
        public BoxesDto BoxesDto { get; set; }
    }

    public class UpdateBoxWithBoxCurrencyCommandHandler : IRequestHandler<UpdateBoxWithBoxCurrencyCommand, Response<string>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public UpdateBoxWithBoxCurrencyCommandHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(UpdateBoxWithBoxCurrencyCommand request, CancellationToken cancellationToken)
        {

            #region Experimental Code Refactor

            var pBoxesCurrencies = await _db.BoxesCurrencies.Where(o => o.BoxId == request.BoxesDto.Id).ToListAsync(cancellationToken);
            var addableBoxesCurrencies = request.BoxesDto.BoxesCurrencies.Where(c => c.Id == 0).ToList();
            var updateableBoxesCurrencies = request.BoxesDto.BoxesCurrencies.Where(c => c.Id > 0).ToList();
            var updateableBoxesCurrenciesId = updateableBoxesCurrencies.Select(c => c.Id).ToList();
            var removableBoxesCurrencies = pBoxesCurrencies.Where(c => !updateableBoxesCurrenciesId.Contains(c.Id)).ToList();

            #endregion

            try
            {
                var varRemove = await _db.BoxesCurrencies.Where(o => o.BoxId == request.BoxesDto.Id).ToListAsync(cancellationToken);
                _db.BoxesCurrencies.RemoveRange(varRemove);

                var box = await _db.Boxes.FindAsync(request.BoxesDto.Id);
                if (box == null) return Response<string>.Fail("Sorry! No Box Found To Update");

                box.Name = request.BoxesDto.Name;
                box.IsActive = request.BoxesDto.IsActive;

                _db.Boxes.Update(box);


                if (request?.BoxesDto.BoxesCurrencies != null)
                {
                    foreach (var item in request.BoxesDto.BoxesCurrencies)
                    {

                        var isBoxCurrency = await _db.BoxesCurrencies.Where(o => o.Id == item.Id).FirstOrDefaultAsync(cancellationToken);
                        if (isBoxCurrency != null)
                        {
                            isBoxCurrency.BoxId = request.BoxesDto.Id;
                            isBoxCurrency.CurrencyId = item.CurrencyId;
                            isBoxCurrency.StartValue = item.StartValue;
                            isBoxCurrency.IsActive = item.IsActive;

                            _db.BoxesCurrencies.Update(isBoxCurrency);

                        }
                        else
                        {
                            Domain.Entities.BoxesCurrencies boxesCurrencies = new Domain.Entities.BoxesCurrencies();
                            boxesCurrencies.BoxId = request.BoxesDto.Id;
                            boxesCurrencies.CurrencyId = item.CurrencyId;
                            boxesCurrencies.StartValue = item.StartValue;
                            boxesCurrencies.IsActive = item.IsActive;

                            await _db.BoxesCurrencies.AddAsync(boxesCurrencies, cancellationToken);
                            await _db.SaveChangesAsync(cancellationToken);
                        }
                    }
                }
                else
                {
                    var allBoxesCurrencies = await _db.BoxesCurrencies.Where(o => o.BoxId == request.BoxesDto.Id).ToListAsync(cancellationToken);
                    _db.BoxesCurrencies.RemoveRange(allBoxesCurrencies);
                }

                await _db.SaveChangesAsync(cancellationToken);
                return Response<string>.Success(box.Name, "Box Successfully Updated");
            }
            catch (Exception e)
            {
                return Response<string>.Fail("Failed to Update Box");
            }
        }
    }
}
