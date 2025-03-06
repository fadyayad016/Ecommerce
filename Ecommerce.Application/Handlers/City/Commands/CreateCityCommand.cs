using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Entities;
using MediatR;

namespace Ecommerce.Application.Handlers.City.Commands
{
    using CityEntity = Ecommerce.Domain.Entities.City;

    public class CreateCityCommand : IRequest<Response<string>>
    {
        public String Name { get; set; }
        public string Description { get; set; }
        public Boolean Status { get; set; }
    }

    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, Response<string>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public CreateCityCommandHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var city = _mapper.Map<CityEntity>(request);
                var addcolor = await _db.Cities.AddAsync(city);
                await _db.SaveChangesAsync(cancellationToken);
                return Response<string>.Success(city.Name, "Successfully created");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Response<string>.Fail("Failed to add item!");
            }
        }
    }

}
