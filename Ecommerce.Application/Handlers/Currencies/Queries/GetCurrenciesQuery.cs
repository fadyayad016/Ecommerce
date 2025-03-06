using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.Handlers.Currency.Queries
{
    public class GetCurrenciesQuery : IRequest<IEnumerable<CurrencyDto>>
    {
    }

    public class GetCurrenciesQueryHandler : IRequestHandler<GetCurrenciesQuery, IEnumerable<CurrencyDto>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public GetCurrenciesQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CurrencyDto>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
        {
            var currencies = await _db.Currencies.OrderByDescending(o => o.LastModifiedDate).ToListAsync();

            var result = _mapper.Map<List<CurrencyDto>>(currencies);
            return result.AsReadOnly();
        }
    }

}
