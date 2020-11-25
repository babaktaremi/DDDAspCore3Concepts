using System.Threading;
using System.Threading.Tasks;
using Application.Common.Marks;
using Infrastructure.Persistence;
using MediatR.Pipeline;

namespace Application.Common
{
    public class CommitCommandPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse> where TResponse : class
    {
        private readonly ApplicationDbContext _db;

        public CommitCommandPostProcessor(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            if (request is ICommittable)
            {
                if(!response.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(OperationResult<>)))
                {
                    await _db.SaveChangesAsync(cancellationToken);
                    return;
                }


                dynamic result = response;

                if (result.IsSuccess)
                    await _db.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
