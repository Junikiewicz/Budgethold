namespace Budgethold.API.Endpoints.Auth.Dtos
{
    public record SignUpRequest
    {
        public SignUpRequest()
        {
            Email = null!;
            Password = null!;
            Name = null!;
        }

        public string Name { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
    }
}
