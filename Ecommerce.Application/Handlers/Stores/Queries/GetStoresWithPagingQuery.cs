using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Ecommerce.Application.Handlers.Stores.Queries
{
    public class GetStoresWithPagingQuery : IRequest<PaginatedList<StoreDto>>
    {
        public int? page { get; set; }
        public int length { get; set; }
        public string searchValue { get; set; } = "";
        public string sortColumn { get; set; } = "Id";
        public string sortOrder { get; set; } = "Desc";
    }
    public class GetStoresWithPagingQueryHandler : IRequestHandler<GetStoresWithPagingQuery, PaginatedList<StoreDto>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public GetStoresWithPagingQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<PaginatedList<StoreDto>> Handle(GetStoresWithPagingQuery request, CancellationToken cancellationToken)
        {
            var stores = _db.Stores.Include(c=>c.Cities).OrderByDescending(o => o.LastModifiedDate).AsQueryable();
            var getstores =
                    stores
                    .Where(a => a.Name.ToLower().Contains(request.searchValue))
                    .OrderBy($"{request.sortColumn} {request.sortOrder}")
                    .ProjectTo<StoreDto>(_mapper.ConfigurationProvider);

            var data = await PaginatedList<StoreDto>.CreateAsync(getstores, request.page ?? 1, request.length);
            return data;
        }
    }

}
