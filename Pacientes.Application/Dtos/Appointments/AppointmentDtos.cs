namespace Pacientes.Application.Dtos.Appointments;

public class SaveAppointmentDto
{
    public DateTime AppointmentDate { get; set; }
    public string Reason { get; set; } = string.Empty;
    /// <summary>1 = Programada, 2 = Completada, 3 = Cancelada</summary>
    public int Status { get; set; } = 1;
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
}

public class AppointmentDto : SaveAppointmentDto, IHasId
{
    public int Id { get; set; }
}
