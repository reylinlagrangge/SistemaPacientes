namespace Pacientes.Application.Dtos.Patients;

public class SavePatientDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public DateTime? BirthDate { get; set; }
    public string IdentityNumber { get; set; } = string.Empty;
    public string? Gender { get; set; }
    public string? Address { get; set; }
    public string? PolicyNumber { get; set; }
    public int? InsuranceProviderId { get; set; }
}

public class PatientDto : SavePatientDto, IHasId
{
    public int Id { get; set; }
}
