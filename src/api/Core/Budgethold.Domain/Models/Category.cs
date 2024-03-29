﻿using Budgethold.Domain.Common;

namespace Budgethold.Domain.Models
{
    public class Category : Entity
    {
        public Category()
        {
            TransactionType = null!;
            User = null!;
            ChildCategories = null!;
            Name = null!;
            Transactions = null!;
        }

        public Category(string name, int? parentCategoryId, int transactionTypeId, int userId)
        {
            Name = name;
            ParentCategoryId = parentCategoryId;
            TransactionTypeId = transactionTypeId;
            UserId = userId;
            ChildCategories = new ();
            User = null!;
            TransactionType = null!;
            Transactions = null!;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public int? ParentCategoryId { get; private set; }

        public int TransactionTypeId { get; private set; }

        public int UserId { get; private set; }

        public virtual Category? ParentCategory { get; private set; }

        public virtual User User { get; private set; }

        public virtual TransactionType TransactionType { get; private set; }

        public virtual HashSet<Category> ChildCategories { get; private set; }

        public virtual HashSet<Transaction> Transactions { get; private set; }

        public void Update(int? parentCategoryId, string name)
        {
            UpdateParentCategoryId(parentCategoryId);
            Name = name;
        }

        public void UpdateParentCategoryId(int? parentCategoryId)
        {
            ParentCategoryId = parentCategoryId;
        }
    }
}
