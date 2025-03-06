using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Brand.Commands
{
    public class CreateBrandCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Response<string>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public CreateBrandCommandHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var existingBrand = _db.Brands.FirstOrDefault(b => b.Name == request.Name);

            if (existingBrand != null)
            {
                return Response<string>.Fail($"Brand with name [{existingBrand.Name}] already exists.");
            }

            try
            {
                var brand = _mapper.Map<Ecommerce.Domain.Entities.Brand>(request);
                await _db.Brands.AddAsync(brand);
                await _db.SaveChangesAsync(cancellationToken);
                return Response<string>.Success(brand.Name, "Successfully created");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Response<string>.Fail("Failed to add brand!");
            }
        }
    }

}
