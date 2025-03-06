using AutoMapper;
using Ecommerce.Application.Common;
using MediatR;


namespace Ecommerce.Application.Handlers.Currencies.Commands
{
    public class DeleteCurrencyCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }

    public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand, Unit>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;

        public DeleteCurrencyCommandHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
        {
            var currency = await _db.Currencies.FindAsync(request.Id);
            if (currency == null)
            {
                throw new Exception("Currency not found");
            }

            _db.Currencies.Remove(currency);
            await _db.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
