namespace Budgethold.API.Endpoints.Wallet.Dtos
{
    public record EditWalletRequest
    {
        public EditWalletRequest()
        {
            Name = null!;
        }

        public int[] Users { get; init; }
        public string Name { get; init; }
        public decimal? StartingValue { get; init; }
        public int? NewOwner { get; init; }
    }
}
