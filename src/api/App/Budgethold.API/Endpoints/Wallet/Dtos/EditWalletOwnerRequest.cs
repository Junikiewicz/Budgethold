namespace Budgethold.API.Endpoints.Wallet.Dtos
{
    public record EditWalletOwnerRequest
    {
        public EditWalletOwnerRequest()
        {
        }

        public int NewOner { get; init; }

    }
}
