using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Brand.Queries
{
    public class GetBrandByIdQuery : IRequest<Response<BrandDto>>
    {
        public int Id { get; set; }
    }

    public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, Response<BrandDto>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public GetBrandByIdQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Response<BrandDto>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var brand = await _db.Brands.FindAsync(request.Id);
            if (brand == null)
            {
                return Response<BrandDto>.Fail("Brand not found.");
            }

            var brandDto = _mapper.Map<BrandDto>(brand);
            return Response<BrandDto>.Success(brandDto, "Successfully retrieved");
        }
    }

}
