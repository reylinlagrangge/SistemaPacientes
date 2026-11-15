namespace Pacientes.Application.Dtos.InsuranceProviders;

public class SaveInsuranceProviderDto
{
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }
}

public class InsuranceProviderDto : SaveInsuranceProviderDto, IHasId
{
    public int Id { get; set; }
}
