using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Helpers;
using MediatR;
using System.Linq.Dynamic.Core;

namespace Ecommerce.Application.Handlers.City.Queries
{
    public class GetCitiesWithPagingQuery : IRequest<PaginatedList<CityDto>>
    {
        public int? page { get; set; }
        public int length { get; set; }
        public string searchValue { get; set; } = "";
        public string sortColumn { get; set; } = "Id";
        public string sortOrder { get; set; } = "Desc";
    }

    public class GetCitiesWithPagingQueryHandler : IRequestHandler<GetCitiesWithPagingQuery, PaginatedList<CityDto>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public GetCitiesWithPagingQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<PaginatedList<CityDto>> Handle(GetCitiesWithPagingQuery request, CancellationToken cancellationToken)
        {
            var cities = _db.Cities.OrderByDescending(o => o.LastModifiedDate).AsQueryable();
            var getcities =
                    cities
                    .Where(a => a.Name.ToLower().Contains(request.searchValue))
                    .OrderBy($"{request.sortColumn} {request.sortOrder}")
                    .ProjectTo<CityDto>(_mapper.ConfigurationProvider);

            var data = await PaginatedList<CityDto>.CreateAsync(getcities, request.page ?? 1, request.length);
            return data;
        }
    }

}
