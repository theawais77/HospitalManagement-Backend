using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagement_Backend.Models
{
    public class NurseAssignment
    {
        [Key]
        public int AssignmentID { get; set; }
        [ForeignKey("Nurse")]
        public int NurseID { get; set; }
        public User? Nurse { get; set; }
        [ForeignKey("Patient")]
        public int PatientID { get; set; }
        public Patient? Patient { get; set; }
        public DateTime AssignedAt { get; set; }
    }
}
