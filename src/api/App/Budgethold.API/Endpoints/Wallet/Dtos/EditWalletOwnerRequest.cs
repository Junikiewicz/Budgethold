namespace Budgethold.API.Endpoints.Wallet.Dtos
{
    public record EditWalletOwnerRequest
    {
        public int NewOwnerId { get; init; }
    }
}
