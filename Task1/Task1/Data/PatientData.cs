using Task1.Models;

namespace Task1.Data
{
    public static class PatientData
    {
        private static List<Patient> Patients;

        public static List<Patient> GetPatients()
        {
            Patients = new List<Patient>()
            {
                new Patient
                {
                    Id = 1,
                    Name = "John Doe",
                    PhoneNumber = "123-456-7890",
                    CheckInDate = DateTime.Now.AddDays(-1),
                    DoctorId = 1,
                    isRegistered = true
                },
                new Patient
                {
                    Id = 2,
                    Name = "Jane Smith",
                    PhoneNumber = "987-654-3210",
                    CheckInDate = DateTime.Now.AddDays(-2),
                    DoctorId = 2,
                    isRegistered = false
                },
                new Patient
                {
                    Id = 3,
                    Name = "Alice Johnson",
                    PhoneNumber = "555-123-4567",
                    CheckInDate = DateTime.Now.AddDays(-3),
                    DoctorId = 1,
                    isRegistered = true
                },
                new Patient
                {
                    Id = 4,
                    Name = "Bob Brown",
                    PhoneNumber = "444-987-6543",
                    CheckInDate = DateTime.Now.AddDays(-4),
                    DoctorId = 2,
                    isRegistered = false
                },
                new Patient
                {
                    Id = 5,
                    Name = "Charlie Davis",
                    PhoneNumber = "333-555-7777",
                    CheckInDate = DateTime.Now.AddDays(-5),
                    DoctorId = 1,
                    isRegistered = true
                },
                new Patient
                {
                    Id = 6,
                    Name = "Diana Evans",
                    PhoneNumber = "222-444-6666",
                    CheckInDate = DateTime.Now.AddDays(-6),
                    DoctorId = 2,
                    isRegistered = false
                },
            };

            return Patients;
        }
    }
}