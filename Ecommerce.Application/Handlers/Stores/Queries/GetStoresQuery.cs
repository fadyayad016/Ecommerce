using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.Handlers.Stores.Queries
{
    public class GetStoresQuery : IRequest<IEnumerable<StoreDto>>
    {
    }
    public class GetStoresQueryHandler : IRequestHandler<GetStoresQuery, IEnumerable<StoreDto>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public GetStoresQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StoreDto>> Handle(GetStoresQuery request, CancellationToken cancellationToken)
        {
            var stores = await _db.Stores.Include(c=>c.Cities).OrderByDescending(o => o.LastModifiedDate).ToListAsync();

            var result = _mapper.Map<List<StoreDto>>(stores);
            return result.AsReadOnly();
        }
    }
}
