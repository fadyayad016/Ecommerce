using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.BarCodes.Queries
{
    public class GetBarcodeByNameQuery : IRequest<Barcode>
    {
        public string BarcodeName { get; set; }
    }
    public class GetBarcodeByNameQueryHandler : IRequestHandler<GetBarcodeByNameQuery, Barcode>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;

        public GetBarcodeByNameQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Barcode> Handle(GetBarcodeByNameQuery request, CancellationToken cancellationToken)
        {
            return await _db.Barcodes.AsNoTracking().FirstOrDefaultAsync(b => b.BarcodeName == request.BarcodeName, cancellationToken);
        }
    }
}
