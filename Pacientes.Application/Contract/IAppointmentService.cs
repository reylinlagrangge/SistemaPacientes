using Pacientes.Application.Core;
using Pacientes.Application.Dtos;
using Pacientes.Application.Dtos.Appointments;

namespace Pacientes.Application.Contract;

public interface IAppointmentService : IBaseService<AppointmentDto, SaveAppointmentDto>
{
    Task<ServiceResult<List<AppointmentDto>>> GetByPatientAsync(int patientId);
    Task<ServiceResult<List<AppointmentDto>>> GetByDoctorAsync(int doctorId);
}
