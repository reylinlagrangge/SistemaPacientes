using Pacientes.Application.Core;
using Pacientes.Application.Dtos.Patients;

namespace Pacientes.Application.Contract;

public interface IPatientService : IBaseService<PatientDto, SavePatientDto> { }
