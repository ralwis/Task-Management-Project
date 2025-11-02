using MediatR;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.feature.Task.Commands.Login;
using TaskManagement.Application.feature.Task.Commands.RegisterUser;

namespace TaskManagement.API.Endpoints
{
    public static class Authentication
    {
        public static void MapAuthentication(this WebApplication app)
        {
            app.MapPost("/login", async (ISender sender, LoginUserRequest request) =>
            {
                var isValid = await sender.Send(new LoginCommand(request.Email, request.Password));
                if (!isValid)
                    return Results.Unauthorized();

                return Results.Ok(new { Message = "Login successful", ValidUser = isValid });
            }).WithTags("Authentication");

            app.MapPost("/register", async (ISender sender, RegisterUserRequest request) =>
            {
                try
                {
                    var userId = await sender.Send(new RegisterUserCommand(request.Email, request.Password));
                    return Results.Ok(new { Message = "User created", ValidUser = true });
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { Message = ex.Message });
                }
            }).WithTags("Authentication");
        }
    }
}
