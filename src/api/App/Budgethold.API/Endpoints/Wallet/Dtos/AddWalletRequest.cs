namespace Budgethold.API.Endpoints.Wallet.Dtos
{
    public class AddWalletRequest
    {
        public AddWalletRequest()
        {
            Name = null!;
        }
        public int[] UsersId { get; init; }
        public string Name { get; init; }
        public decimal StartingValue { get; init; }
    }
}