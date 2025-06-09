using ClinicManagement.Api.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Api.Features.Appointments;

public record GetAppointmentsQuery(int? PatientId) : IRequest<List<Appointment>>;

public class GetAppointmentsHandler(ApplicationDbContext db) : IRequestHandler<GetAppointmentsQuery, List<Appointment>>
{
    public async Task<List<Appointment>> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
    {
        var query = db.Appointments.AsQueryable();
        if (request.PatientId.HasValue)
        {
            query = query.Where(a => a.PatientId == request.PatientId.Value);
        }
        return await query.AsNoTracking().ToListAsync(cancellationToken);
    }
}
