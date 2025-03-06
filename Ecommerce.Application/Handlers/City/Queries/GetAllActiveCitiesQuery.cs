using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Handlers.Categories.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.City.Queries
{
    public class GetAllActiveCitiesQuery : IRequest<List<CityDto>>
    {
    }

    public class GetAllActiveCitiesQueryHandler : IRequestHandler<GetAllActiveCitiesQuery, List<CityDto>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public GetAllActiveCitiesQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<CityDto>> Handle(GetAllActiveCitiesQuery request, CancellationToken cancellationToken)
        {
            var city = await _db.Cities.Where(c=>c.Status==true)
                .ToListAsync(cancellationToken);

            var result = _mapper.Map<List<CityDto>>(city).ToList();
            return result;
        }
    }
}
