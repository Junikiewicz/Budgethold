using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgethold.Application.Queries.Wallet.GetUserWalletQuery
{
    public class GetUserWalletQueryHandler : IRequestHandler<GetUserWalletQuery, Result<WalletResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserWalletQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<WalletResponse>> Handle(GetUserWalletQuery query, CancellationToken cancellationToken)
        {
            var wallet = await _unitOfWork.WalletsRepository.GetUserWalletAsync(query.WalletId, query.UserId, cancellationToken);

            return new Result<WalletResponse>(wallet);
        }
    }
}
