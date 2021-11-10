using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common;

namespace Budgethold.Persistance
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public async Task<Result> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return new Result<int>(await _context.SaveChangesAsync(cancellationToken));
        }
    }
}
