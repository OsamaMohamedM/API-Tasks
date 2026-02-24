using System.ComponentModel.DataAnnotations;

namespace Task1.DTO
{
    public class PatientDTO
    {
        public int Id { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string phoneNumber { get; set; }
    }
}