namespace Budgethold.API.Endpoints.Auth.Dtos
{
    public class SignUpRequest
    {
        public SignUpRequest()
        {
            Email = null!;
            Password = null!;
            Name = null!;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
