using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Helpers;
using MediatR;
using System.Linq.Dynamic.Core;

namespace Ecommerce.Application.Handlers.BoxesCurrencies.Queries
{
    public class GetBoxesCurrenciesWithPagingQuery : IRequest<PaginatedList<BoxesCurrenciesDto>>
    {
        public int? page { get; set; }
        public int length { get; set; }
        public string searchValue { get; set; } = "";
        public string sortColumn { get; set; } = "Id";
        public string sortOrder { get; set; } = "Desc";
    }

    public class GetAllBoxesCurrenciesQueryHandler : IRequestHandler<GetBoxesCurrenciesWithPagingQuery, PaginatedList<BoxesCurrenciesDto>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public GetAllBoxesCurrenciesQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<PaginatedList<BoxesCurrenciesDto>> Handle(GetBoxesCurrenciesWithPagingQuery request, CancellationToken cancellationToken)
        {
            var boxesCurrencies = _db.BoxesCurrencies.OrderByDescending(o => o.LastModifiedDate).AsQueryable();
            var getboxesCurrencies =
                    boxesCurrencies
                    .Where(a => a.Currencies.Name.ToLower().Contains(request.searchValue))
                    .OrderBy($"{request.sortColumn} {request.sortOrder}")
                    .ProjectTo<BoxesCurrenciesDto>(_mapper.ConfigurationProvider);

            var data = await PaginatedList<BoxesCurrenciesDto>.CreateAsync(getboxesCurrencies, request.page ?? 1, request.length);
            return data;
        }
    }
}
