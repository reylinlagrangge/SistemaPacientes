using Microsoft.EntityFrameworkCore;
using Pacientes.Application.Contract;
using Pacientes.Application.Services;
using Pacientes.Infrastructure.Context;
using Pacientes.Infrastructure.Interfaces;
using Pacientes.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PacientesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PacientesDb")));

// Repositorios (Infrastructure)
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
builder.Services.AddScoped<IDiagnosisRepository, DiagnosisRepository>();
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
builder.Services.AddScoped<IMedicationRepository, MedicationRepository>();
builder.Services.AddScoped<IInsuranceProviderRepository, InsuranceProviderRepository>();
builder.Services.AddScoped<IAllergyRepository, AllergyRepository>();

// Servicios (Application)
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<ISpecialtyService, SpecialtyService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
builder.Services.AddScoped<IDiagnosisService, DiagnosisService>();
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
builder.Services.AddScoped<IMedicationService, MedicationService>();
builder.Services.AddScoped<IInsuranceProviderService, InsuranceProviderService>();
builder.Services.AddScoped<IAllergyService, AllergyService>();

// CORS para que el frontend pueda consumir el API
builder.Services.AddCors(options => options.AddPolicy("Frontend", policy =>
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

// Las validaciones las hace la capa de servicio, con sus propios mensajes
builder.Services.AddControllers(options =>
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("Frontend");
app.UseAuthorization();
app.MapControllers();

app.Run();
