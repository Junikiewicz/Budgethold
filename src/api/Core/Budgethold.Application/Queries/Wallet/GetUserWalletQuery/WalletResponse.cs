﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgethold.Application.Queries.Wallet.GetUserWalletQuery
{
    public record WalletResponse
    {
        public int Id { get; init; }
        public decimal StartingValue { get; init; }
        public decimal CurrentValue { get; init; }
        public string? Name { get; init; }
        public IEnumerable<OwningUser>? OwningUsers { get; init; }
        public int WalletOwnerId { get; set; }

        public record OwningUser
        {
            public int Id { get; init; }
            public string? Name { get; init; }
        }
    }
}
