using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Entities;
using MediatR;

namespace Ecommerce.Application.Handlers.Barcodes.Commands
{
    public class CreateBarcodeCommand : IRequest<Response<string>>
    {
        public string BarcodeName { get; set; }
        public long ProductId { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class CreateBarcodeCommandHandler : IRequestHandler<CreateBarcodeCommand, Response<string>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;

        public CreateBarcodeCommandHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateBarcodeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var barcode = _mapper.Map<Barcode>(request);
                await _db.Barcodes.AddAsync(barcode);
                await _db.SaveChangesAsync(cancellationToken);
                return Response<string>.Success(barcode.BarcodeName, "Successfully created");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Response<string>.Fail("Failed to add brand!");
            }

        }
    }
}