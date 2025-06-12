using ClinicManagement.Api.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Api.Features.Appointments;

public record AddAppointmentCommand(int PatientId, DateTime Date, string Description) : IRequest<int>;

public class AddAppointmentHandler(ApplicationDbContext db) : IRequestHandler<AddAppointmentCommand, int>
{
    public async Task<int> Handle(AddAppointmentCommand request, CancellationToken cancellationToken)
    {
        var exists = await db.Patients.AnyAsync(p => p.Id == request.PatientId, cancellationToken);
        if (!exists) return -1;
        var appointment = new Appointment
        {
            PatientId = request.PatientId,
            Date = request.Date,
            Description = request.Description
        };
        db.Appointments.Add(appointment);
        await db.SaveChangesAsync(cancellationToken);
        return appointment.Id;
    }
}
