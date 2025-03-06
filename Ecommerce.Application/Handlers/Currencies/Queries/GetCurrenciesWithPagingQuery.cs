using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Helpers;
using MediatR;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Currencies.Queries
{
    public class GetCurrenciesWithPagingQuery : IRequest<PaginatedList<CurrencyDto>>
    {
        public int? page { get; set; }
        public int length { get; set; }
        public string searchValue { get; set; } = "";
        public string sortColumn { get; set; } = "Id";
        public string sortOrder { get; set; } = "Desc";
    }

    public class GetCurrenciesWithPagingQueryHandler : IRequestHandler<GetCurrenciesWithPagingQuery, PaginatedList<CurrencyDto>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;

        public GetCurrenciesWithPagingQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<PaginatedList<CurrencyDto>> Handle(GetCurrenciesWithPagingQuery request, CancellationToken cancellationToken)
        {
            var currencies = _db.Currencies.OrderByDescending(o => o.LastModifiedDate).AsQueryable();

            var filteredCurrencies =
                currencies
                .Where(a => a.Name.ToLower().Contains(request.searchValue.ToLower()))
                .OrderBy($"{request.sortColumn} {request.sortOrder}")
                .ProjectTo<CurrencyDto>(_mapper.ConfigurationProvider);

            var data = await PaginatedList<CurrencyDto>.CreateAsync(filteredCurrencies, request.page ?? 1, request.length);
            return data;
        }
    }
}
