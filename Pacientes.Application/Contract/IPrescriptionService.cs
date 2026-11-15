using Pacientes.Application.Core;
using Pacientes.Application.Dtos;
using Pacientes.Application.Dtos.Prescriptions;

namespace Pacientes.Application.Contract;

public interface IPrescriptionService : IBaseService<PrescriptionDto, SavePrescriptionDto>
{
    Task<ServiceResult<List<PrescriptionDto>>> GetByMedicalRecordAsync(int medicalRecordId);
}
