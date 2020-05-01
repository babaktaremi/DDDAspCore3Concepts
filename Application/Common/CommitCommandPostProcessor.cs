using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Persistence;
using MediatR.Pipeline;

namespace Application.Common
{
    public class CommitCommandPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        private readonly ApplicationDbContext _db;

        public CommitCommandPostProcessor(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
