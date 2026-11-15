using Microsoft.EntityFrameworkCore;
using Pacientes.Domain.Entities;

namespace Pacientes.Infrastructure.Context;

public class PacientesContext : DbContext
{
    public PacientesContext(DbContextOptions<PacientesContext> options) : base(options) { }

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Specialty> Specialties => Set<Specialty>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();
    public DbSet<Diagnosis> Diagnoses => Set<Diagnosis>();
    public DbSet<Prescription> Prescriptions => Set<Prescription>();
    public DbSet<Medication> Medications => Set<Medication>();
    public DbSet<InsuranceProvider> InsuranceProviders => Set<InsuranceProvider>();
    public DbSet<Allergy> Allergies => Set<Allergy>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Patient>(e =>
        {
            e.Property(p => p.FirstName).HasMaxLength(100);
            e.Property(p => p.LastName).HasMaxLength(100);
            e.Property(p => p.Email).HasMaxLength(150);
            e.Property(p => p.Phone).HasMaxLength(20);
            e.Property(p => p.IdentityNumber).HasMaxLength(20);
            e.Property(p => p.Gender).HasMaxLength(20);
            e.Property(p => p.Address).HasMaxLength(250);
            e.Property(p => p.PolicyNumber).HasMaxLength(50);
            e.HasIndex(p => p.IdentityNumber).IsUnique();
            e.HasIndex(p => p.Email).IsUnique();
        });

        mb.Entity<Doctor>(e =>
        {
            e.Property(d => d.FirstName).HasMaxLength(100);
            e.Property(d => d.LastName).HasMaxLength(100);
            e.Property(d => d.Email).HasMaxLength(150);
            e.Property(d => d.Phone).HasMaxLength(20);
            e.Property(d => d.LicenseNumber).HasMaxLength(30);
            e.HasIndex(d => d.LicenseNumber).IsUnique();
            e.HasIndex(d => d.Email).IsUnique();
        });

        mb.Entity<Specialty>(e =>
        {
            e.Property(s => s.Name).HasMaxLength(100);
            e.Property(s => s.Description).HasMaxLength(300);
            e.HasIndex(s => s.Name).IsUnique();
        });

        mb.Entity<InsuranceProvider>(e =>
        {
            e.Property(i => i.Name).HasMaxLength(100);
            e.Property(i => i.Phone).HasMaxLength(20);
            e.HasIndex(i => i.Name).IsUnique();
        });

        mb.Entity<Medication>(e =>
        {
            e.Property(m => m.Name).HasMaxLength(100);
            e.Property(m => m.Presentation).HasMaxLength(100);
            e.Property(m => m.Description).HasMaxLength(300);
            e.HasIndex(m => m.Name).IsUnique();
        });

        mb.Entity<Appointment>(e => e.Property(a => a.Reason).HasMaxLength(300));

        mb.Entity<MedicalRecord>(e =>
        {
            e.Property(m => m.Notes).HasMaxLength(1000);
            e.Property(m => m.Treatment).HasMaxLength(1000);
        });

        mb.Entity<Diagnosis>(e =>
        {
            e.Property(d => d.Code).HasMaxLength(20);
            e.Property(d => d.Description).HasMaxLength(300);
        });

        mb.Entity<Prescription>(e =>
        {
            e.Property(p => p.Dosage).HasMaxLength(100);
            e.Property(p => p.Frequency).HasMaxLength(100);
        });

        mb.Entity<Allergy>(e =>
        {
            e.Property(a => a.Name).HasMaxLength(100);
            e.Property(a => a.Severity).HasMaxLength(30);
        });

        // Evita "multiple cascade paths" en SQL Server: nada se borra en cascada.
        foreach (var fk in mb.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys()))
            fk.DeleteBehavior = DeleteBehavior.Restrict;
    }
}
