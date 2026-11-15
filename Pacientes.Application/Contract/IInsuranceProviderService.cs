using Pacientes.Application.Core;
using Pacientes.Application.Dtos.InsuranceProviders;

namespace Pacientes.Application.Contract;

public interface IInsuranceProviderService : IBaseService<InsuranceProviderDto, SaveInsuranceProviderDto> { }
