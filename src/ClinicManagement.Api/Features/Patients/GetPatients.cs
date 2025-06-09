using ClinicManagement.Api.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Api.Features.Patients;

public record GetPatientsQuery() : IRequest<List<Patient>>;

public class GetPatientsHandler(ApplicationDbContext db) : IRequestHandler<GetPatientsQuery, List<Patient>>
{
    public async Task<List<Patient>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
    {
        return await db.Patients.AsNoTracking().ToListAsync(cancellationToken);
    }
}
