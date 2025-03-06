using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Domain.Common;
using MediatR;


namespace Ecommerce.Application.Handlers.Stores.Commands
{
    public class UpdateStoreCommand : IRequest<Response<string>>
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
    public class UpdateStoreCommandHandler : IRequestHandler<UpdateStoreCommand, Response<string>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;

        public UpdateStoreCommandHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(UpdateStoreCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var store = await _db.Stores.FindAsync(request.Id);
                if (store == null)
                {
                    return Response<string>.Fail("Store not found");
                }

                _mapper.Map(request, store);
                _db.Stores.Update(store);
                await _db.SaveChangesAsync(cancellationToken);

                return Response<string>.Success(store.Name, "Successfully updated");
            }
            catch (Exception)
            {
                return Response<string>.Fail("Failed to update");
            }
        }
    }
}


