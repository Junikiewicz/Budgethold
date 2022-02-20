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
        private ICategoriesRepository? _categoriesRepository;
        private ITransactionRepository? _transactionRepository;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public ITransactionRepository TransactionRepository
        {
            get
            {
                if (_transactionRepository == null)
                {
                    _transactionRepository = new TransactionRepository(_context);
                }

                return _transactionRepository;
            }
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

        public ICategoriesRepository CategoriesRepository
        {
            get
            {
                if (_categoriesRepository == null)
                {
                    _categoriesRepository = new CategoriesRepository(_context);
                }

                return _categoriesRepository;
            }
        }

        public async Task<Result> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return new Result<int>(await _context.SaveChangesAsync(cancellationToken));
        }
    }
}
