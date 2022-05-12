using HealthInstitution.Core.Examinations.Model;
using HealthInstitution.Core.MedicalRecords.Model;
using HealthInstitution.Core.Rooms.Model;
using HealthInstitution.Core.SystemUsers.Doctors.Model;

namespace HealthInstitution.Core.Operations.Model;

public class Operation
{
    public int Id { get; set; }
    public DateTime Appointment { get; set; }
    public int Duration { get; set; }
    public ExaminationStatus Status { get; set; }
    public Room Room { get; set; }
    public Doctor Doctor { get; set; }
    public MedicalRecord MedicalRecord { get; set; }
    public List<EquipmentCost> EquipmentCosts { get; set; }

    public Operation(int id, DateTime appointment, int duration, Room room, Doctor doctor, MedicalRecord medicalRecord)
    {
        this.Id = id;
        this.Status = ExaminationStatus.Scheduled;
        this.Appointment = appointment;
        this.Duration = duration;
        this.Room = room;
        this.Doctor = doctor;
        this.MedicalRecord = medicalRecord;
        this.EquipmentCosts = new List<EquipmentCost>();
    }
}