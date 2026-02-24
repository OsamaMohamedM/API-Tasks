using Microsoft.EntityFrameworkCore;
using Task1.Models;

namespace Task1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { Id = 1, Name = "Dr. Alice Carter", PhoneNumber = "100-200-3000" },
                new Doctor { Id = 2, Name = "Dr. Bob Harris", PhoneNumber = "100-200-4000" }
            );

            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = 1,
                    Name = "John Doe",
                    PhoneNumber = "123-456-7890",
                    Age = 15,
                    CheckInDate = new DateTime(2024, 1, 1),
                    DoctorId = 1,
                    isRegistered = true
                },
                new Patient
                {
                    Id = 2,
                    Name = "Jane Smith",
                    PhoneNumber = "987-654-3210",
                    Age = 11,
                    CheckInDate = new DateTime(2024, 1, 2),
                    DoctorId = 2,
                    isRegistered = false
                },
                new Patient
                {
                    Id = 3,
                    Name = "Alice Johnson",
                    PhoneNumber = "555-123-4567",
                    Age = 15,
                    CheckInDate = new DateTime(2024, 1, 3),
                    DoctorId = 1,
                    isRegistered = true
                },
                new Patient
                {
                    Id = 4,
                    Name = "Bob Brown",
                    PhoneNumber = "444-987-6543",
                    Age = 15,
                    CheckInDate = new DateTime(2024, 1, 4),
                    DoctorId = 2,
                    isRegistered = false
                },
                new Patient
                {
                    Id = 5,
                    Name = "Charlie Davis",
                    PhoneNumber = "333-555-7777",
                    Age = 15,
                    CheckInDate = new DateTime(2024, 1, 5),
                    DoctorId = 1,
                    isRegistered = true
                },
                new Patient
                {
                    Id = 6,
                    Name = "Diana Evans",
                    PhoneNumber = "222-444-6666",
                    Age = 16,
                    CheckInDate = new DateTime(2024, 1, 6),
                    DoctorId = 2,
                    isRegistered = false
                }
            );
        }
    }
}
