using Pacientes.Application.Core;
using Pacientes.Application.Dtos;
using Pacientes.Application.Dtos.Doctors;

namespace Pacientes.Application.Contract;

public interface IDoctorService : IBaseService<DoctorDto, SaveDoctorDto>
{
    Task<ServiceResult<List<DoctorDto>>> GetBySpecialtyAsync(int specialtyId);
}
