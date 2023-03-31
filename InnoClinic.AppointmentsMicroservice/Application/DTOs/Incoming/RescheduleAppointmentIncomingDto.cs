namespace Application.DTOs.Incoming;

public class RescheduleAppointmentIncomingDto
{
    public Guid DoctorId { get; set; }
    public string DoctorFirstName { get; set; }
    public string DoctorLastName { get; set; }
    public string DoctorMiddleName { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
}
