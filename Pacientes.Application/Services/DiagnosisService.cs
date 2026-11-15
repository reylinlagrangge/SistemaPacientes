using Pacientes.Application.Contract;
using Pacientes.Application.Core;
using Pacientes.Application.Dtos;
using Pacientes.Application.Dtos.Diagnoses;
using Pacientes.Domain.Entities;
using Pacientes.Infrastructure.Interfaces;

namespace Pacientes.Application.Services;

public class DiagnosisService : BaseService<Diagnosis, DiagnosisDto, SaveDiagnosisDto>, IDiagnosisService
{
    private readonly IDiagnosisRepository _diagnoses;
    private readonly IMedicalRecordRepository _records;

    public DiagnosisService(IDiagnosisRepository diagnoses, IMedicalRecordRepository records)
        : base(diagnoses, "diagnóstico")
    {
        _diagnoses = diagnoses;
        _records = records;
    }

    public async Task<ServiceResult<List<DiagnosisDto>>> GetByMedicalRecordAsync(int medicalRecordId) =>
        ServiceResult<List<DiagnosisDto>>.Ok(ToDtos(await _diagnoses.GetByMedicalRecordAsync(medicalRecordId)));

    protected override async Task<string?> ValidateAsync(SaveDiagnosisDto d, int? id)
    {
        var error = Rules.First(
            Rules.Required(d.Code, "El código", 20),
            Rules.Required(d.Description, "La descripción", 300));
        if (error != null) return error;

        return await _records.GetByIdAsync(d.MedicalRecordId) is null
            ? "El historial médico indicado no existe."
            : null;
    }

    protected override DiagnosisDto ToDto(Diagnosis e) =>
        new() { Id = e.Id, Code = e.Code, Description = e.Description, MedicalRecordId = e.MedicalRecordId };

    protected override Diagnosis ToEntity(SaveDiagnosisDto d) { var e = new Diagnosis(); Apply(d, e); return e; }

    protected override void Apply(SaveDiagnosisDto d, Diagnosis e)
    {
        e.Code = d.Code.Trim();
        e.Description = d.Description.Trim();
        e.MedicalRecordId = d.MedicalRecordId;
    }
}
