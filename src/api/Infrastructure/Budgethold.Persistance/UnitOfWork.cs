using Budgethold.Application.Contracts.Persistance;
using Budgethold.Application.Contracts.Persistance.Repositories;
using Budgethold.Domain.Common;
using Budgethold.Persistance.Repositories;

namespace Budgethold.Persistance
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private IWalletsRepository? _walletsRepository;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IWalletsRepository WalletsRepository
        {
            get
            {
                if (_walletsRepository == null)
                {
                    _walletsRepository = new WalletsRepository(_context);
                }

                return _walletsRepository;
            }
        }

        public async Task<Result> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return new Result<int>(await _context.SaveChangesAsync(cancellationToken));
        }
    }
}
