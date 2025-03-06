using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.Handlers.BarCodes.Queries
{
    public class GetBarcodesByProductIdQuery : IRequest<List<Barcode>>
    {
        public long ProductId { get; set; }
    }

    public class GetBarcodesByProductIdQueryHandler : IRequestHandler<GetBarcodesByProductIdQuery, List<Barcode>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;

        public GetBarcodesByProductIdQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<Barcode>> Handle(GetBarcodesByProductIdQuery request, CancellationToken cancellationToken)
        {
            return await _db.Barcodes.Where(b => b.ProductId == request.ProductId).ToListAsync(cancellationToken);
        }
    }

}
