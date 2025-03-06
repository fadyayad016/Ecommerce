using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Handlers.Colors.Commands;
using Ecommerce.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.City.Commands
{
    public class UpdateCityCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }

    public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, Response<string>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public UpdateCityCommandHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var city = await _db.Cities.FindAsync(request.Id);
                _mapper.Map(request, city);
                _db.Cities.Update(city);
                await _db.SaveChangesAsync(cancellationToken);
                return Response<string>.Success(city.Name, "Successfully updated");
            }
            catch (Exception e)
            {
                return Response<string>.Fail("Failed to update");
            }
        }
    }

}
