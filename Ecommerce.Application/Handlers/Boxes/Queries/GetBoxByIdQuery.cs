using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto.Box;
using Ecommerce.Domain.Common;
using MediatR;

namespace Ecommerce.Application.Handlers.Boxes.Queries
{
    public class GetBoxByIdQuery : IRequest<Response<BoxesDto>>
    {
        public int Id { get; set; }
    }

    public class GetBoxByIdQueryHandler : IRequestHandler<GetBoxByIdQuery, Response<BoxesDto>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public GetBoxByIdQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Response<BoxesDto>> Handle(GetBoxByIdQuery request, CancellationToken cancellationToken)
        {
            var box = await _db.Boxes.FindAsync(request.Id);
            if (box == null)
            {
                return Response<BoxesDto>.Fail("Box not found.");
            }

            var boxDto = _mapper.Map<BoxesDto>(box);
            return Response<BoxesDto>.Success(boxDto, "Successfully retrieved");
        }
    }

}
