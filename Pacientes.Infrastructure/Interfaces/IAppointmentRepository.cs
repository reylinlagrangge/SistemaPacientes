using Pacientes.Domain.Entities;
using Pacientes.Domain.Repository;

namespace Pacientes.Infrastructure.Interfaces;

public interface IAppointmentRepository : IBaseRepository<Appointment>
{
    Task<IEnumerable<Appointment>> GetByPatientAsync(int patientId);
    Task<IEnumerable<Appointment>> GetByDoctorAsync(int doctorId);
}
