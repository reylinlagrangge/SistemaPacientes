using Pacientes.Application.Core;
using Pacientes.Application.Dtos;
using Pacientes.Application.Dtos.MedicalRecords;

namespace Pacientes.Application.Contract;

public interface IMedicalRecordService : IBaseService<MedicalRecordDto, SaveMedicalRecordDto>
{
    Task<ServiceResult<List<MedicalRecordDto>>> GetByPatientAsync(int patientId);
}
