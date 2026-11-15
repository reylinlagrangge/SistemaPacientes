using Pacientes.Application.Contract;
using Pacientes.Application.Core;
using Pacientes.Application.Dtos.Medications;
using Pacientes.Domain.Entities;
using Pacientes.Infrastructure.Interfaces;

namespace Pacientes.Application.Services;

public class MedicationService : BaseService<Medication, MedicationDto, SaveMedicationDto>, IMedicationService
{
    private readonly IMedicationRepository _medications;

    public MedicationService(IMedicationRepository medications) : base(medications, "medicamento") =>
        _medications = medications;

    protected override async Task<string?> ValidateAsync(SaveMedicationDto d, int? id)
    {
        var error = Rules.First(
            Rules.Required(d.Name, "El nombre", 100),
            Rules.Optional(d.Presentation, "La presentación", 100),
            Rules.Optional(d.Description, "La descripción", 300));
        if (error != null) return error;

        var same = await _medications.GetByNameAsync(d.Name.Trim());
        return same != null && same.Id != id ? "Ya existe un medicamento con ese nombre." : null;
    }

    protected override MedicationDto ToDto(Medication e) =>
        new() { Id = e.Id, Name = e.Name, Presentation = e.Presentation, Description = e.Description };

    protected override Medication ToEntity(SaveMedicationDto d) { var e = new Medication(); Apply(d, e); return e; }

    protected override void Apply(SaveMedicationDto d, Medication e)
    {
        e.Name = d.Name.Trim();
        e.Presentation = d.Presentation?.Trim();
        e.Description = d.Description?.Trim();
    }
}
