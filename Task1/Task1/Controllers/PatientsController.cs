using Microsoft.AspNetCore.Mvc;
using Task1.DTO;
using Task1.Models;
using Task1.Repository;

namespace Task1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<List<PatientDTO>>> GetPatients()
        {
            var patients = await _unitOfWork.Patients.GetAllAsync();
            var result = patients.Select(p => new PatientDTO
            {
                Name = p.Name,
                Age = p.Age,
                phoneNumber = p.PhoneNumber,
                Id = p.Id,
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDTO>> GetPatient(int id)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(id);

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

        [HttpPost]
        public async Task<ActionResult<PatientDTO>> CreatePatient([FromBody] PatientDTO patientDto)
        {
            var patient = new Patient
            {
                Name = patientDto.Name,
                Age = patientDto.Age,
                PhoneNumber = patientDto.phoneNumber
            };

            await _unitOfWork.Patients.AddAsync(patient);
            await _unitOfWork.SaveChangesAsync();

            patientDto.Id = patient.Id;
            return Ok(patientDto);
        }

        [HttpPut]
        public async Task<ActionResult<Patient>> UpdatePatient(int id, [FromBody] Patient patient)
        {
            var existingPatient = await _unitOfWork.Patients.GetByIdAsync(id);
            if (existingPatient == null)
            {
                return NotFound(new { Message = $"Invalid {patient.Id}" });
            }
            existingPatient.Name = patient.Name;
            existingPatient.PhoneNumber = patient.PhoneNumber;
            existingPatient.CheckInDate = patient.CheckInDate;
            existingPatient.DoctorId = patient.DoctorId;
            existingPatient.isRegistered = patient.isRegistered;

            _unitOfWork.Patients.Update(existingPatient);
            await _unitOfWork.SaveChangesAsync();
            return Ok(existingPatient);
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePatient(int id)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(id);
            if (patient == null)
            {
                return NotFound(new { Message = $"Invalid {id}" });
            }
            _unitOfWork.Patients.Delete(patient);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}
