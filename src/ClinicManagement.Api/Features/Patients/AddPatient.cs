using ClinicManagement.Api.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Api.Features.Patients;

public record AddPatientCommand(string Name, DateTime DateOfBirth) : IRequest<int>;

public class AddPatientHandler(ApplicationDbContext db) : IRequestHandler<AddPatientCommand, int>
{
    public async Task<int> Handle(AddPatientCommand request, CancellationToken cancellationToken)
    {
        var patient = new Patient { Name = request.Name, DateOfBirth = request.DateOfBirth };
        db.Patients.Add(patient);
        await db.SaveChangesAsync(cancellationToken);
        return patient.Id;
    }
}
