namespace HospitalManagement_Backend.Models
{
    public class Prescription
    {
        public int PrescriptionID { get; set; }
        public int PatientID { get; set; }
        public required Patient Patient { get; set; }
        public int? DoctorID { get; set; }
        public User? Doctor { get; set; }
        public int MedicationID { get; set; }
        public required Medication Medication { get; set; }
        public required string Dosage { get; set; }
        public required string Instructions { get; set; }
        public DateTime PrescribedAt { get; set; }
    }
}
