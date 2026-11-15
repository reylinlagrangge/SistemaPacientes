using Microsoft.EntityFrameworkCore;
using Pacientes.Domain.Entities;
using Pacientes.Infrastructure.Context;
using Pacientes.Infrastructure.Core;
using Pacientes.Infrastructure.Interfaces;

namespace Pacientes.Infrastructure.Repositories;

public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(PacientesContext context) : base(context) { }

    public async Task<IEnumerable<Appointment>> GetByPatientAsync(int patientId) =>
        await _set.AsNoTracking().Where(a => a.PatientId == patientId).ToListAsync();

    public async Task<IEnumerable<Appointment>> GetByDoctorAsync(int doctorId) =>
        await _set.AsNoTracking().Where(a => a.DoctorId == doctorId).ToListAsync();
}
