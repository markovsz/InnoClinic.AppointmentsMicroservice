namespace Domain.Entities;

public class Result : BaseEntity
{
    public string Complaints { get; set; }
    public string Conclusion { get; set; }
    public string Recomendations { get; set; }
    public DateTime DateTime { get; set; }
    public Guid AppointmentId { get; set; }
    public Appointment Appointment { get; set; }
}
