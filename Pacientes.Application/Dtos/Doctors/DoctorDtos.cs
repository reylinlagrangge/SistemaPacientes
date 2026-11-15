namespace Pacientes.Application.Dtos.Doctors;

public class SaveDoctorDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public DateTime? BirthDate { get; set; }
    public string LicenseNumber { get; set; } = string.Empty;
    public int SpecialtyId { get; set; }
}

public class DoctorDto : SaveDoctorDto, IHasId
{
    public int Id { get; set; }
}
