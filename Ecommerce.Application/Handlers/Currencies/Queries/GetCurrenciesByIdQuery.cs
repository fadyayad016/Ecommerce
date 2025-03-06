using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Currencies.Queries
{
    public class GetCurrencyByIdQuery : IRequest<CurrencyDto>
    {
        public int Id { get; set; }
    }

    public class GetCurrencyByIdQueryHandler : IRequestHandler<GetCurrencyByIdQuery, CurrencyDto>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;

        public GetCurrencyByIdQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CurrencyDto> Handle(GetCurrencyByIdQuery request, CancellationToken cancellationToken)
        {
            var currency = await _db.Currencies.FindAsync(request.Id);
            if (currency == null)
            {
                throw new Exception("Currency not found");
            }

            var result = _mapper.Map<CurrencyDto>(currency);
            return result;
        }
    }
}
