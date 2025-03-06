using Ecommerce.Application.Common;
using Ecommerce.Application.Handlers.Brand.Commands;
using Ecommerce.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Boxes.Commands
{
    public class DeleteBoxCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }

    public class DeleteBoxCommandHandler : IRequestHandler<DeleteBoxCommand, Response<string>>
    {
        private readonly IDataContext _db;
        public DeleteBoxCommandHandler(IDataContext db)
        {
            _db = db;
        }

        public async Task<Response<string>> Handle(DeleteBoxCommand request, CancellationToken cancellationToken)
        {
            var box = await _db.Boxes.FindAsync(request.Id);
            if (box == null)
            {
                return Response<string>.Fail("Box not found.");
            }

            _db.Boxes.Remove(box);
            await _db.SaveChangesAsync(cancellationToken);
            return Response<string>.Success(request.Id.ToString(), "Successfully deleted");
        }
    }
}
