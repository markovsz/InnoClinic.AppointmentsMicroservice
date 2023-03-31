namespace Application.DTOs.Incoming;

public class ResultIncomingDto
{
    public Guid AppointmentId { get; set; }
    public string Complaints { get; set; }
    public string Conclusion { get; set; }
    public string Recomendations { get; set; }
}
