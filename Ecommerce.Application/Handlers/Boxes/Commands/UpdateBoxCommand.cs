using AutoMapper;
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
    public class UpdateBoxCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateBoxCommandHandler : IRequestHandler<UpdateBoxCommand, Response<string>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;

        public UpdateBoxCommandHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(UpdateBoxCommand request, CancellationToken cancellationToken)
        {
            var box = await _db.Boxes.FindAsync(request.Id);
            if (box == null)
            {
                return Response<string>.Fail("Box not found");
            }

            var existingBox = _db.Boxes.FirstOrDefault(b => b.Name == request.Name && b.Id != request.Id);
            if (existingBox != null)
            {
                return Response<string>.Fail($"A box with the name '{request.Name}' already exists.");
            }

            try
            {
                _mapper.Map(request, box);
                _db.Boxes.Update(box);
                await _db.SaveChangesAsync(cancellationToken);
                return Response<string>.Success(box.Name, "Successfully updated");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Response<string>.Fail("Failed to update box!");
            }
        }
    }

}
