using Ecommerce.Application.Common;
using Ecommerce.Application.Handlers.City.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.Application.Handlers.City.Queries
{
    public class IsCityNameExistQuery : IRequest<bool>
    {
        public required string Name { get; set; }
    }
    public class IsCityNameQueryHandler : IRequestHandler<IsCityNameExistQuery, bool>
    {
        private readonly IDataContext _db;
        public IsCityNameQueryHandler(IDataContext db)
        {
            _db = db;
        }

        public async Task<bool> Handle(IsCityNameExistQuery request, CancellationToken cancellationToken)
        {
            return await _db.Cities.AnyAsync(p => p.Name.ToLower() == request.Name.ToLower());
        }
    }

}
