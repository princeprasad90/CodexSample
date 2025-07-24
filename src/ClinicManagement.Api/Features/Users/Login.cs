using ClinicManagement.Api.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Api.Features.Users;

public record LoginCommand(string UserName, string Password) : IRequest<string?>;

public class LoginHandler(ApplicationDbContext db) : IRequestHandler<LoginCommand, string?>
{
    public async Task<string?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName, cancellationToken);
        if (user is null) return null;
        if (!PasswordUtils.VerifyPassword(request.Password, user.PasswordHash)) return null;
        // Normally generate JWT here
        return "sample-token";
    }
}
