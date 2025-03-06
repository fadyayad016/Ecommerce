using Ecommerce.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.Application.Handlers.Currencies.Queries
{
    public class IsCurrencyNameExistQuery : IRequest<bool>
    {
        public required string Name { get; set; }
    }

    public class IsCurrencyNameExistQueryHandler : IRequestHandler<IsCurrencyNameExistQuery, bool>
    {
        private readonly IDataContext _db;

        public IsCurrencyNameExistQueryHandler(IDataContext db)
        {
            _db = db;
        }

        public async Task<bool> Handle(IsCurrencyNameExistQuery request, CancellationToken cancellationToken)
        {
            return await _db.Currencies.AnyAsync(p => p.Name.ToLower() == request.Name.ToLower());
        }
    }
}
