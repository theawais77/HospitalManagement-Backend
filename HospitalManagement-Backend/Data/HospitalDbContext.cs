using Microsoft.EntityFrameworkCore;
using HospitalManagement_Backend.Models;

namespace HospitalManagement_Backend.Data
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }
        //dbset represent a collection of tables in the database
        //this means there is a users table in db

        public DbSet<User> Users { get; set; } 
        public DbSet<Patient> Patients { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Vital> Vitals { get; set; }
        public DbSet<NurseAssignment> NurseAssignments { get; set; }
        public DbSet<MedicationSchedule> MedicationSchedule { get; set; }

        internal void DeleteUserAsync()
        {
            throw new NotImplementedException();
        }
    }
}
