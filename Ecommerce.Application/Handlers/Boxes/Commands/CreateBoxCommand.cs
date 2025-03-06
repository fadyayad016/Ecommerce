using AutoMapper;
using Ecommerce.Application.Common;
using MediatR;
using Ecommerce.Domain.Common;

namespace Ecommerce.Application.Handlers.Boxes.Commands
{
    public class CreateBoxCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

    // Handlers
    public class CreateBoxHandler : IRequestHandler<CreateBoxCommand, Response<string>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;

        public CreateBoxHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateBoxCommand request, CancellationToken cancellationToken)
        {
            var existingBox = _db.Boxes.FirstOrDefault(b => b.Name == request.Name);

            if (existingBox != null)
            {
                return Response<string>.Fail($"Box with name [{existingBox.Name}] already exists.");
            }

            try
            {
                var box = _mapper.Map<Ecommerce.Domain.Entities.Boxes>(request);
                await _db.Boxes.AddAsync(box);
                await _db.SaveChangesAsync(cancellationToken);
                return Response<string>.Success(box.Name, "Successfully created");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Response<string>.Fail("Failed to add box!");
            }
        }
    }
}
