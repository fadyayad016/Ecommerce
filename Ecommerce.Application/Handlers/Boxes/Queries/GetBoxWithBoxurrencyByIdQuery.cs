using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Dto.Box;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Boxes.Queries
{
    public class GetBoxWithBoxCurrencyByIdQuery : IRequest<BoxesDto>
    {
        public int Id { get; set; }
    }

    public class GetBoxWithBoxCurrencyByIdQueryHandler : IRequestHandler<GetBoxWithBoxCurrencyByIdQuery, BoxesDto>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public GetBoxWithBoxCurrencyByIdQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<BoxesDto> Handle(GetBoxWithBoxCurrencyByIdQuery request, CancellationToken cancellationToken)
        {
            var boxCurrencyVm = await (from pv in _db.BoxesCurrencies
                                       where pv.BoxId == request.Id
                                          select new BoxesCurrenciesDto
                                          {
                                              Id = pv.Id,
                                              IsActive = pv.IsActive,
                                              StartValue = pv.StartValue,
                                              BoxId = pv.BoxId,
                                              CurrencyId = pv.CurrencyId
                                          }).ToListAsync(cancellationToken);

            var boxVm = await (from p in _db.Boxes
                                   where p.Id == request.Id
                                   select new BoxesDto
                                   {
                                       Id = p.Id,
                                       IsActive = p.IsActive,
                                       Name = p.Name,
                                       BoxesCurrencies = boxCurrencyVm.Any() ? boxCurrencyVm : null
                                   }).FirstOrDefaultAsync(cancellationToken);


            return boxVm;
        }
    }

}
