using Pacientes.Application.Core;
using Pacientes.Application.Dtos;
using Pacientes.Application.Dtos.Diagnoses;

namespace Pacientes.Application.Contract;

public interface IDiagnosisService : IBaseService<DiagnosisDto, SaveDiagnosisDto>
{
    Task<ServiceResult<List<DiagnosisDto>>> GetByMedicalRecordAsync(int medicalRecordId);
}
