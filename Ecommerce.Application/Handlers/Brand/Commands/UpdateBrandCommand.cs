using AutoMapper;
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
    public class UpdateBrandCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, Response<string>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;

        public UpdateBrandCommandHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _db.Brands.FindAsync(request.Id);
            if (brand == null)
            {
                return Response<string>.Fail("Brand not found");
            }

            var existingBrand = _db.Brands.FirstOrDefault(b => b.Name == request.Name && b.Id != request.Id);
            if (existingBrand != null)
            {
                return Response<string>.Fail($"A brand with the name '{request.Name}' already exists.");
            }

            try
            {
                _mapper.Map(request, brand);
                _db.Brands.Update(brand);
                await _db.SaveChangesAsync(cancellationToken);
                return Response<string>.Success(brand.Name, "Successfully updated");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Response<string>.Fail("Failed to update brand!");
            }
        }
    }

}
