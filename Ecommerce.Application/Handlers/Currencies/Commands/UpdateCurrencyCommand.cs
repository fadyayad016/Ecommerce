using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Domain.Common;
using MediatR;

namespace Ecommerce.Application.Handlers.Currencies.Commands
{
    public class UpdateCurrencyCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }  // اسم العملة
        public string Symbol { get; set; }  // اسم العملة
        public bool MainCurrency { get; set; }  // هل هي العملة الرئيسية؟
        public bool IsActive { get; set; } = true; // هل هي العملة الرئيسية؟
    }

    public class UpdateCurrencyCommandHandler : IRequestHandler<UpdateCurrencyCommand, Response<string>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;

        public UpdateCurrencyCommandHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var currency = await _db.Currencies.FindAsync(request.Id);
                if (currency == null)
                {
                    return Response<string>.Fail("Currency not found");
                }

                _mapper.Map(request, currency);
                _db.Currencies.Update(currency);
                await _db.SaveChangesAsync(cancellationToken);

                return Response<string>.Success(currency.Name, "Successfully updated");
            }
            catch (Exception)
            {
                return Response<string>.Fail("Failed to update");
            }
        }
    }
}
