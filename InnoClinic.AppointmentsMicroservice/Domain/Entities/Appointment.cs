namespace Domain.Entities;

public class Appointment : BaseEntity
{
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid SpecializationId { get; set; }
    public Guid ServiceId { get; set; }
    public DateTime DateTime { get; set; }
    public bool IsApproved { get; set; }

    public Result Result { get; set; }

    public string DoctorFirstName { get; set; }
    public string DoctorLastName { get; set; }
    public string DoctorMiddleName { get; set; }
    public string ServiceName { get; set; }
    public Guid OfficeId { get; set; }

}
