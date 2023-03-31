namespace Application.DTOs.Incoming;

public class AppointmentIncomingDto
{
    public Guid PatientId { get; set; }
    public Guid SpecializationId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid ServiceId { get; set; }
    public Guid OfficeId { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }

    public string DoctorFirstName { get; set; }
    public string DoctorLastName { get; set; }
    public string DoctorMiddleName { get; set; }
    public string ServiceName { get; set; }
}
