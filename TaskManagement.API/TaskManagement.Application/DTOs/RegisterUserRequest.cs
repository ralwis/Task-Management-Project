namespace TaskManagement.Application.DTOs
{
    public record RegisterUserRequest
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }
}
