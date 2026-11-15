using Pacientes.Application.Contract;
using Pacientes.Application.Core;
using Pacientes.Application.Dtos.Patients;
using Pacientes.Domain.Entities;
using Pacientes.Infrastructure.Interfaces;

namespace Pacientes.Application.Services;

public class PatientService : BaseService<Patient, PatientDto, SavePatientDto>, IPatientService
{
    private readonly IPatientRepository _patients;
    private readonly IInsuranceProviderRepository _providers;

    public PatientService(IPatientRepository patients, IInsuranceProviderRepository providers)
        : base(patients, "paciente")
    {
        _patients = patients;
        _providers = providers;
    }

    protected override async Task<string?> ValidateAsync(SavePatientDto d, int? id)
    {
        var error = Rules.First(
            Rules.Required(d.FirstName, "El nombre", 100),
            Rules.Required(d.LastName, "El apellido", 100),
            Rules.Email(d.Email),
            Rules.Optional(d.Phone, "El teléfono", 20),
            Rules.BirthDate(d.BirthDate),
            Rules.Required(d.IdentityNumber, "La cédula", 20),
            Rules.Optional(d.Gender, "El género", 20),
            Rules.Optional(d.Address, "La dirección", 250),
            Rules.Optional(d.PolicyNumber, "El número de póliza", 50));
        if (error != null) return error;

        if (d.InsuranceProviderId.HasValue && await _providers.GetByIdAsync(d.InsuranceProviderId.Value) is null)
            return "La aseguradora indicada no existe.";

        var byIdentity = await _patients.GetByIdentityNumberAsync(d.IdentityNumber.Trim());
        if (byIdentity != null && byIdentity.Id != id) return "Ya existe un paciente con esa cédula.";

        var byEmail = await _patients.GetByEmailAsync(d.Email.Trim());
        if (byEmail != null && byEmail.Id != id) return "Ya existe un paciente con ese correo.";

        return null;
    }

    protected override PatientDto ToDto(Patient e) => new()
    {
        Id = e.Id,
        FirstName = e.FirstName,
        LastName = e.LastName,
        Email = e.Email,
        Phone = e.Phone,
        BirthDate = e.BirthDate,
        IdentityNumber = e.IdentityNumber,
        Gender = e.Gender,
        Address = e.Address,
        PolicyNumber = e.PolicyNumber,
        InsuranceProviderId = e.InsuranceProviderId
    };

    protected override Patient ToEntity(SavePatientDto d) { var e = new Patient(); Apply(d, e); return e; }

    protected override void Apply(SavePatientDto d, Patient e)
    {
        e.FirstName = d.FirstName.Trim();
        e.LastName = d.LastName.Trim();
        e.Email = d.Email.Trim();
        e.Phone = d.Phone?.Trim();
        e.BirthDate = d.BirthDate;
        e.IdentityNumber = d.IdentityNumber.Trim();
        e.Gender = d.Gender?.Trim();
        e.Address = d.Address?.Trim();
        e.PolicyNumber = d.PolicyNumber?.Trim();
        e.InsuranceProviderId = d.InsuranceProviderId;
    }
}
