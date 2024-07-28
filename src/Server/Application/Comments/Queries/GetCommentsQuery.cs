using Application.Comments.Dtos;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Comments.Queries
{
    public record GetCommentsQuery(Guid ActivityId) : IRequest<Result<List<CommentDto>>>;

    public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, Result<List<CommentDto>>>
    {
        private readonly DataContext _context;

        public GetCommentsQueryHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<List<CommentDto>>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        
        {
            var comments = await _context.Comments
                .Include(a => a.Author)
                .ThenInclude(p => p.Photos)
                .Where(x => x.Activity.Id == request.ActivityId)
                .OrderBy(x => x.CreatedAt)
                .Select(x => (CommentDto)x)
                .ToListAsync(cancellationToken);

            return Result<List<CommentDto>>.Success(comments);
        }
    }
}
