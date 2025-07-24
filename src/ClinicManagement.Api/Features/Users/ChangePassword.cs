using ClinicManagement.Api.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Api.Features.Users;

public record ChangePasswordCommand(int UserId, string CurrentPassword, string NewPassword) : IRequest<bool>;

public class ChangePasswordHandler(ApplicationDbContext db) : IRequestHandler<ChangePasswordCommand, bool>
{
    public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
        if (user is null) return false;
        if (!PasswordUtils.VerifyPassword(request.CurrentPassword, user.PasswordHash)) return false;
        user.PasswordHash = PasswordUtils.HashPassword(request.NewPassword);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
