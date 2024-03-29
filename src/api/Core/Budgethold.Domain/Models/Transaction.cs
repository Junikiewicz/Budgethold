﻿using Budgethold.Domain.Common;

namespace Budgethold.Domain.Models
{
    public class Transaction : Entity
    {
        public Transaction()
        {
            Name = null!;
            Description = null!;
            Wallet = null!;
            Category = null!;
        }

        public Transaction(string name, string description, int walletId, int categoryId, decimal amount, DateTime date)
        {
            Name = name;
            Description = description;
            WalletId = walletId;
            CategoryId = categoryId;
            Amount = amount;
            Wallet = null!;
            Category = null!;
            Date = date;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public int WalletId { get; private set; }

        public int CategoryId { get; private set; }

        public decimal Amount { get; private set; }

        public DateTime Date { get; private set; }

        public Wallet Wallet { get; private set; }

        public Category Category { get; private set; }

        public void EditTransaction(string name, string description, DateTime date, decimal amount, int categoryId, int walletId)
        {
            CategoryId = categoryId;
            WalletId = walletId;
            Amount = amount;
            Name = name;
            Description = description;
            Date = date;
        }
    }
}
