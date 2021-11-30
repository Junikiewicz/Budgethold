﻿using Budgethold.Domain.Common;
using MediatR;

namespace Budgethold.Application.Commands.Transaction.DeleteTransaction
{
    public record DeleteTransactionCommand : IRequest<Result>
    {
        public DeleteTransactionCommand(int userId, int transactionId, int walletId)
        {
            UserId = userId;
            TransactionId = transactionId;
            WalletId = walletId;
        }

        public int UserId { get; init; }
        public int TransactionId { get; init; }
        public int WalletId { get; init; }
    }
}
