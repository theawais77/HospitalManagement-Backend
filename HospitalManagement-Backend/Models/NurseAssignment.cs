using System.ComponentModel.DataAnnotations;

namespace HospitalManagement_Backend.Models
{
    public class NurseAssignment
    {
        [Key]
        public int AssignmentID { get; set; }
        public int NurseID { get; set; }
        public required User Nurse { get; set; }
        public int PatientID { get; set; }
        public required Patient Patient { get; set; }
        public DateTime AssignedAt { get; set; }
    }
}
