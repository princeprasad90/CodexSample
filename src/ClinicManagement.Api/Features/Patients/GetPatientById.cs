using ClinicManagement.Api.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Api.Features.Patients;

public record GetPatientByIdQuery(int Id) : IRequest<Patient?>;

public class GetPatientByIdHandler(ApplicationDbContext db) : IRequestHandler<GetPatientByIdQuery, Patient?>
{
    public async Task<Patient?> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
    {
        return await db.Patients
            .AsNoTracking()
            .Include(p => p.Appointments)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
    }
}
