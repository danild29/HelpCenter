using AspNetCore.Identity.Database;
using System.Security.Claims;

namespace HelpCenter.Api.EndPoints;

public static class ConfigureEndpoints
{
    public static IEndpointRouteBuilder AddEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("users/me", async (ClaimsPrincipal user, ApplicationDbContext context) =>
        {
            string userId = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            User usera = await context.Users.FindAsync(userId);
            return usera;
        })
        .RequireAuthorization();

        return app;
    }
}
