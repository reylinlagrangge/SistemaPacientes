using Pacientes.Application.Contract;
using Pacientes.Application.Core;
using Pacientes.Application.Dtos;
using Pacientes.Application.Dtos.Allergies;
using Pacientes.Domain.Entities;
using Pacientes.Infrastructure.Interfaces;

namespace Pacientes.Application.Services;

public class AllergyService : BaseService<Allergy, AllergyDto, SaveAllergyDto>, IAllergyService
{
    private readonly IAllergyRepository _allergies;
    private readonly IPatientRepository _patients;

    public AllergyService(IAllergyRepository allergies, IPatientRepository patients) : base(allergies, "alergia")
    {
        _allergies = allergies;
        _patients = patients;
    }

    public async Task<ServiceResult<List<AllergyDto>>> GetByPatientAsync(int patientId) =>
        ServiceResult<List<AllergyDto>>.Ok(ToDtos(await _allergies.GetByPatientAsync(patientId)));

    protected override async Task<string?> ValidateAsync(SaveAllergyDto d, int? id)
    {
        var error = Rules.First(
            Rules.Required(d.Name, "El nombre", 100),
            Rules.Optional(d.Severity, "La severidad", 30));
        if (error != null) return error;

        if (await _patients.GetByIdAsync(d.PatientId) is null) return "El paciente indicado no existe.";

        var patientAllergies = await _allergies.GetByPatientAsync(d.PatientId);
        if (patientAllergies.Any(a => a.Id != id && string.Equals(a.Name, d.Name.Trim(), StringComparison.OrdinalIgnoreCase)))
            return "El paciente ya tiene registrada esa alergia.";

        return null;
    }

    protected override AllergyDto ToDto(Allergy e) =>
        new() { Id = e.Id, Name = e.Name, Severity = e.Severity, PatientId = e.PatientId };

    protected override Allergy ToEntity(SaveAllergyDto d) { var e = new Allergy(); Apply(d, e); return e; }

    protected override void Apply(SaveAllergyDto d, Allergy e)
    {
        e.Name = d.Name.Trim();
        e.Severity = d.Severity?.Trim();
        e.PatientId = d.PatientId;
    }
}
