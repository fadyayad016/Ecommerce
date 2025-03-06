using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Handlers.Currencies.Commands;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Entities;
using MediatR;


namespace Ecommerce.Application.Handlers.Stores.Commands
{
    public class CreateStoreCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int CityId { get; set; }
        public String Address { get; set; }
        public String Phone { get; set; }
        public String TelPhone { get; set; }
        public String Description { get; set; }
        public Boolean IsActive { get; set; } = true;
    }
    public class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommand, Response<string>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public CreateStoreCommandHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var store = _mapper.Map<Store>(request);
                var addstore = await _db.Stores.AddAsync(store);
                await _db.SaveChangesAsync(cancellationToken);
                return Response<string>.Success(store.Name, "Successfully created");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Response<string>.Fail("Failed to add item!");
            }
        }
    }

}
