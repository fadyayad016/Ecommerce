using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Handlers.Colors.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.City.Commands
{
    public class DeleteCityCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand, Unit>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public DeleteCityCommandHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var city = await _db.Cities.FindAsync(request.Id);
            _db.Cities.Remove(city);
            await _db.SaveChangesAsync(cancellationToken);
            var citydto = _mapper.Map<CityDto>(city);
            return Unit.Value;
        }
    }

}
