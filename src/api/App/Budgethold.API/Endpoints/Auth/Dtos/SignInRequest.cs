namespace Budgethold.API.Endpoints.Auth.Dtos
{
    public class SignInRequest
    {
        public SignInRequest()
        {
            Email = null!;
            Password = null!;
        }

        public string Email { get; init; }
        public string Password { get; init; }
    }
}
