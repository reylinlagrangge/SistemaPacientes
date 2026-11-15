using Pacientes.Application.Core;
using Pacientes.Application.Dtos.Medications;

namespace Pacientes.Application.Contract;

public interface IMedicationService : IBaseService<MedicationDto, SaveMedicationDto> { }
