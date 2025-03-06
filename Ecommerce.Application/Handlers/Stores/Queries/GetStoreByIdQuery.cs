using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Handlers.Stores.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Stores.Queries
{
    public class GetStoreByIdQuery : IRequest<StoreDto>
    {
        public int Id { get; set; }
    }
    public class GetStoreByIdQueryHandler : IRequestHandler<GetStoreByIdQuery, StoreDto>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public GetStoreByIdQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<StoreDto> Handle(GetStoreByIdQuery request, CancellationToken cancellationToken)
        {
            var store = await _db.Stores.FindAsync(request.Id);
            var result = _mapper.Map<StoreDto>(store);
            return result;
        }
    }
}
