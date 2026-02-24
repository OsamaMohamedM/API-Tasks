using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task1.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string PhoneNumber { get; set; }

        public DateTime CheckInDate { get; set; }

        public Doctor Doctor { get; set; }

        [ForeignKey(nameof(Doctor))]
        public int DoctorId { get; set; }

        public bool isRegistered { get; set; }
    }
}