namespace Task1.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public List<Patient> Patients { get; set; }
    }
}