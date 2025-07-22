using ClinicManagement.Api;
using ClinicManagement.Api.Features.Appointments;
using ClinicManagement.Api.Features.Patients;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/patients", async (AddPatientCommand command, IMediator mediator) =>
    Results.Ok(await mediator.Send(command)));

app.MapGet("/patients", async (IMediator mediator) =>
    Results.Ok(await mediator.Send(new GetPatientsQuery())));

app.MapGet("/patients/{id:int}", async (int id, IMediator mediator) =>
    Results.Ok(await mediator.Send(new GetPatientByIdQuery(id))));

app.MapPost("/appointments", async (AddAppointmentCommand command, IMediator mediator) =>
    Results.Ok(await mediator.Send(command)));

app.MapGet("/appointments", async (int? patientId, IMediator mediator) =>
    Results.Ok(await mediator.Send(new GetAppointmentsQuery(patientId))));

app.Run();
