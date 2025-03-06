﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Helpers;
using MediatR;
using System.Linq.Dynamic.Core;
namespace Ecommerce.Application.Handlers.Brand.Queries
{
    public class GetBrandsWithPagingQuery : IRequest<PaginatedList<BrandDto>>
    {
        public int? page { get; set; }
        public int length { get; set; }
        public string searchValue { get; set; } = "";
        public string sortColumn { get; set; } = "Id";
        public string sortOrder { get; set; } = "Desc";
    }
    public class GetAllBrandQueryHandler : IRequestHandler<GetBrandsWithPagingQuery, PaginatedList<BrandDto>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public GetAllBrandQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<PaginatedList<BrandDto>> Handle(GetBrandsWithPagingQuery request, CancellationToken cancellationToken)
        {
            var brands = _db.Brands.OrderByDescending(o => o.LastModifiedDate).AsQueryable();
            var getbrands =
                    brands
                    .Where(a => a.Name.ToLower().Contains(request.searchValue))
                    .OrderBy($"{request.sortColumn} {request.sortOrder}")
                    .ProjectTo<BrandDto>(_mapper.ConfigurationProvider);

            var data = await PaginatedList<BrandDto>.CreateAsync(getbrands, request.page ?? 1, request.length);
            return data;
        }
    }

}
