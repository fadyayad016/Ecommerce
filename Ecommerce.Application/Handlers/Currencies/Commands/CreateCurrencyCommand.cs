using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Entities;
using MediatR;
using PayPal.Api;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Currencies.Commands
{
    public class CreateCurrencyCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }  // اسم العملة
        public string Symbol { get; set; }  // اسم العملة
        public bool MainCurrency { get; set; }  // هل هي العملة الرئيسية؟
        public bool IsActive { get; set; } = true; // هل هي العملة الرئيسية؟
    }

    public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, Response<string>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;

        public CreateCurrencyCommandHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var currency = _mapper.Map<Ecommerce.Domain.Entities.Currency>(request); await _db.Currencies.AddAsync(currency);
                await _db.SaveChangesAsync(cancellationToken);

                return Response<string>.Success(currency.Name, "Successfully created");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Response<string>.Fail("Failed to add item!");
            }
        }
    }
}
