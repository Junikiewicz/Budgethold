namespace Budgethold.API.Endpoints.Auth.Dtos
{
    public class SignUpRequest
    {
        public SignUpRequest()
        {
            Email = null!;
            Password = null!;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
