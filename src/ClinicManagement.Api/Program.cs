using ClinicManagement.Api;
using ClinicManagement.Api.Features.Appointments;
using ClinicManagement.Api.Features.Patients;
using ClinicManagement.Api.Features.Users;
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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
    if (!db.Users.Any())
    {
        db.Users.Add(new ClinicManagement.Api.Domain.User
        {
            UserName = "admin",
            PasswordHash = PasswordUtils.HashPassword("Password123")
        });
        db.SaveChanges();
    }
}

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

app.MapPost("/appointments", async (AddAppointmentCommand command, IMediator mediator) =>
    Results.Ok(await mediator.Send(command)));

app.MapGet("/appointments", async (int? patientId, IMediator mediator) =>
    Results.Ok(await mediator.Send(new GetAppointmentsQuery(patientId))));

app.MapPost("/login", async (LoginCommand command, IMediator mediator) =>
{
    var token = await mediator.Send(command);
    return token is null ? Results.Unauthorized() : Results.Ok(new { token });
});

app.MapPost("/change-password", async (ChangePasswordCommand command, IMediator mediator) =>
    await mediator.Send(command) ? Results.Ok() : Results.BadRequest());

app.Run();
