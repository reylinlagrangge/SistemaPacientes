using Pacientes.Application.Contract;
using Pacientes.Application.Core;
using Pacientes.Application.Dtos;
using Pacientes.Application.Dtos.Prescriptions;
using Pacientes.Domain.Entities;
using Pacientes.Infrastructure.Interfaces;

namespace Pacientes.Application.Services;

public class PrescriptionService : BaseService<Prescription, PrescriptionDto, SavePrescriptionDto>, IPrescriptionService
{
    private readonly IPrescriptionRepository _prescriptions;
    private readonly IMedicalRecordRepository _records;
    private readonly IMedicationRepository _medications;

    public PrescriptionService(IPrescriptionRepository prescriptions, IMedicalRecordRepository records,
        IMedicationRepository medications) : base(prescriptions, "receta")
    {
        _prescriptions = prescriptions;
        _records = records;
        _medications = medications;
    }

    public async Task<ServiceResult<List<PrescriptionDto>>> GetByMedicalRecordAsync(int medicalRecordId) =>
        ServiceResult<List<PrescriptionDto>>.Ok(ToDtos(await _prescriptions.GetByMedicalRecordAsync(medicalRecordId)));

    protected override async Task<string?> ValidateAsync(SavePrescriptionDto d, int? id)
    {
        var error = Rules.First(
            Rules.Required(d.Dosage, "La dosis", 100),
            Rules.Required(d.Frequency, "La frecuencia", 100),
            d.DurationDays is < 1 or > 365 ? "La duración debe estar entre 1 y 365 días." : null);
        if (error != null) return error;

        if (await _records.GetByIdAsync(d.MedicalRecordId) is null) return "El historial médico indicado no existe.";
        if (await _medications.GetByIdAsync(d.MedicationId) is null) return "El medicamento indicado no existe.";
        return null;
    }

    protected override PrescriptionDto ToDto(Prescription e) => new()
    {
        Id = e.Id,
        Dosage = e.Dosage,
        Frequency = e.Frequency,
        DurationDays = e.DurationDays,
        MedicalRecordId = e.MedicalRecordId,
        MedicationId = e.MedicationId
    };

    protected override Prescription ToEntity(SavePrescriptionDto d) { var e = new Prescription(); Apply(d, e); return e; }

    protected override void Apply(SavePrescriptionDto d, Prescription e)
    {
        e.Dosage = d.Dosage.Trim();
        e.Frequency = d.Frequency.Trim();
        e.DurationDays = d.DurationDays;
        e.MedicalRecordId = d.MedicalRecordId;
        e.MedicationId = d.MedicationId;
    }
}
