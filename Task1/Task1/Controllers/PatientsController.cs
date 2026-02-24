using Microsoft.AspNetCore.Mvc;
using Task1.Data;
using Task1.DTO;
using Task1.Models;

namespace Task1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<PatientDTO>> GetPatients()
        {
            var patients = PatientData.GetPatients()
                .Select(p => new PatientDTO
                {
                    Name = p.Name,
                    Age = p.Age,
                    phoneNumber = p.PhoneNumber
                }).ToList();

            return Ok(patients);
        }

        [HttpGet("{id}")]
        public ActionResult<PatientDTO> GetPatient(int id)
        {
            var patient = PatientData.GetPatients()
                                     .FirstOrDefault(p => p.Id == id);

            if (patient == null)
            {
                return NotFound(new { Message = $"Invalid {id}" });
            }
            var patientDto = new PatientDTO
            {
                Id = patient.Id,
                Age = patient.Age,
                phoneNumber = patient.PhoneNumber,
                Name = patient.Name
            };
            return Ok(patientDto);
        }

        [HttpPost()]
        public ActionResult<PatientDTO> CreatePatient([FromBody] PatientDTO patient)
        {
            var patients = PatientData.GetPatients();
            int nId = patients.Max(p => p.Id) + 1;
            patient.Id = nId;
            patients.Add(new Patient
            {
                Name = patient.Name,
                Age = patient.Age,
                PhoneNumber = patient.phoneNumber,
                Id = nId
            });
            return CreatedAtAction(nameof(GetPatient), patient);
        }

        [HttpPut]
        public ActionResult<Patient> UpdatePatient(int id, [FromBody] Patient patient)
        {
            var patients = PatientData.GetPatients();
            var existingPatient = patients.FirstOrDefault(p => p.Id == id);
            if (existingPatient == null)
            {
                return NotFound(new { Message = $"Invalid {patient.Id}" });
            }
            existingPatient.Name = patient.Name;
            existingPatient.PhoneNumber = patient.PhoneNumber;
            existingPatient.CheckInDate = patient.CheckInDate;
            existingPatient.DoctorId = patient.DoctorId;
            existingPatient.isRegistered = patient.isRegistered;
            return Ok(existingPatient);
        }

        [HttpDelete]
        public ActionResult DeletePatient(int id)
        {
            var patients = PatientData.GetPatients();
            var patient = patients.FirstOrDefault(p => p.Id == id);
            if (patient == null)
            {
                return NotFound(new { Message = $"Invalid {id}" });
            }
            patients.Remove(patient);
            return NoContent();
        }
    }
}