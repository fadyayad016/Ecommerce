﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto.Box;
using Ecommerce.Application.Helpers;
using MediatR;
using System.Linq.Dynamic.Core;

namespace Ecommerce.Application.Handlers.Boxes.Queries
{
    public class GetBoxWithPagingQuery : IRequest<PaginatedList<BoxesDto>>
    {   
    public int? page { get; set; }
    public int length { get; set; }
    public string searchValue { get; set; } = "";
    public string sortColumn { get; set; } = "Id";
    public string sortOrder { get; set; } = "Desc";
    }

    public class GetAllBoxQueryHandler : IRequestHandler<GetBoxWithPagingQuery, PaginatedList<BoxesDto>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public GetAllBoxQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<PaginatedList<BoxesDto>> Handle(GetBoxWithPagingQuery request, CancellationToken cancellationToken)
        {
            var boxes = _db.Boxes.OrderByDescending(o => o.LastModifiedDate).AsQueryable();
            var getboxes =
                    boxes
                    .Where(a => a.Name.ToLower().Contains(request.searchValue))
                    .OrderBy($"{request.sortColumn} {request.sortOrder}")
                    .ProjectTo<BoxesDto>(_mapper.ConfigurationProvider);

            var data = await PaginatedList<BoxesDto>.CreateAsync(getboxes, request.page ?? 1, request.length);
            return data;
        }
    }
}
