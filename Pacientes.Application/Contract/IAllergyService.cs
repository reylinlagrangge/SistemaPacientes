using Pacientes.Application.Core;
using Pacientes.Application.Dtos;
using Pacientes.Application.Dtos.Allergies;

namespace Pacientes.Application.Contract;

public interface IAllergyService : IBaseService<AllergyDto, SaveAllergyDto>
{
    Task<ServiceResult<List<AllergyDto>>> GetByPatientAsync(int patientId);
}
