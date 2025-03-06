using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Brand.Queries
{
    public class GetBrandsQuery : IRequest<Response<List<BrandDto>>> { }

    public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, Response<List<BrandDto>>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public GetBrandsQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Response<List<BrandDto>>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands = await _db.Brands.ToListAsync(cancellationToken);
            var brandDtos = _mapper.Map<List<BrandDto>>(brands);
            return Response<List<BrandDto>>.Success(brandDtos, "Successfully fetched brands");
        }
    }
}
