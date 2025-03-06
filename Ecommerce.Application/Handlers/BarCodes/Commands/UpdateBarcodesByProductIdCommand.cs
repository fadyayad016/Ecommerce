using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.BarCodes.Commands
{
    public class UpdateBarcodesByProductIdCommand : IRequest<Response<string>>
    {
        public long ProductId { get; set; }
        public List<string> Barcodes { get; set; } = new();
    }

    // Handler (UpdateProductBarcodesCommandHandler)

    public class UpdateBarcodesByProductIdCommandHandler : IRequestHandler<UpdateBarcodesByProductIdCommand, Response<string>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;

        public UpdateBarcodesByProductIdCommandHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(UpdateBarcodesByProductIdCommand request, CancellationToken cancellationToken)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);
            if (product == null)
                return Response<string>.Fail("Product not found");

            // Remove existing barcodes
          //  _db.Barcodes.RemoveRange(product.Barcodes);

            // Add new barcodes
            foreach (var barcode in request.Barcodes)
            {
                if (!string.IsNullOrWhiteSpace(barcode))
                {
                    _db.Barcodes.Add(new Barcode
                    {
                        BarcodeName = barcode,
                        ProductId = request.ProductId,
                        IsActive = true
                    });
                }
            }

            await _db.SaveChangesAsync(cancellationToken);
            return Response<string>.Success("Barcodes updated successfully");
        }
    }
}
