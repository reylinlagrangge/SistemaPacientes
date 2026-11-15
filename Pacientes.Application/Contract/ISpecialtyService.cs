using Pacientes.Application.Core;
using Pacientes.Application.Dtos.Specialties;

namespace Pacientes.Application.Contract;

public interface ISpecialtyService : IBaseService<SpecialtyDto, SaveSpecialtyDto> { }
