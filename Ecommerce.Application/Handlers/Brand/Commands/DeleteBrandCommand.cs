using Ecommerce.Application.Common;
using Ecommerce.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Brand.Commands
{
    public class DeleteBrandCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }

    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, Response<string>>
    {
        private readonly IDataContext _db;
        public DeleteBrandCommandHandler(IDataContext db)
        {
            _db = db;
        }

        public async Task<Response<string>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _db.Brands.FindAsync(request.Id);
            if (brand == null)
            {
                return Response<string>.Fail("Brand not found.");
            }

            _db.Brands.Remove(brand);
            await _db.SaveChangesAsync(cancellationToken);
            return Response<string>.Success(request.Id.ToString(), "Successfully deleted");
        }
    }
}
