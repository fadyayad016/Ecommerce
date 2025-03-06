using AutoMapper;
using Ecommerce.Application.Common;
using MediatR;

namespace Ecommerce.Application.Handlers.BarCodes.Commands
{
    public class DeleteBarcodeCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteBarcodeCommandHandler : IRequestHandler<DeleteBarcodeCommand, bool>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;

        public DeleteBarcodeCommandHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteBarcodeCommand request, CancellationToken cancellationToken)
        {
            var barcode = await _db.Barcodes.FindAsync(request.Id);
            if (barcode == null) return false;

            _db.Barcodes.Remove(barcode);
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
