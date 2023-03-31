namespace Application.DTOs.Outgoing;

public class ResultOutgoingDto
{
    public Guid Id { get; set; }
    public DateTime DateTime { get; set; }
    public Guid PatientId { get; set; }
    public string DoctorFirstName { get; set; }
    public string DoctorLastName { get; set; }
    public string DoctorMiddleName { get; set; }
    public Guid SpecializationId { get; set; }
    public string ServiceName { get; set; }
    public string Complaints { get; set; }
    public string Conclusion { get; set; }
    public string Recomendations { get; set; }
}
