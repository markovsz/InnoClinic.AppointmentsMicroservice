namespace Application.DTOs.Outgoing;

public class PatientAppointmentsOutgoingDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public string DoctorFirstName { get; set; }
    public string DoctorLastName { get; set; }
    public string DoctorMiddleName { get; set; }
    public string ServiceName { get; set; }
    public Guid? ResultId { get; set; }
}
