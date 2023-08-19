using Hospital_System.Models.DTOs.Patient;
using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
namespace Hospital_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatient _patient;
        public PatientsController(IPatient patient)
        {
            _patient = patient;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OutPatientDTO>>> GetPatients()
        {
            var patients = await _patient.GetPatients();
            return Ok(patients);
        }
        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDTO>> GetPatient(int id)
        {
            var patient = await _patient.GetPatient(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }
        // PUT: api/Appointment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, InPatientDTO UpdatedPatient)
        {
            if (UpdatedPatient == null)
            {
                return BadRequest("you need to sent Patient information to be edited");
            }
            var CurrentPatient = await _patient.UpdatePatient(id, UpdatedPatient);
            if (CurrentPatient == null)
            {
                return BadRequest("There are some erorrs in this request or room is not avaliable");
            }
            return Ok(CurrentPatient);
        }
        // POST: api/Appointment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OutPatientDTO>> PostPatient(InPatientDTO Patient)
        {
            if (Patient == null)
            {
                return Problem("Entity set 'Patient'  is null.");
            }
            var newPatient = await _patient.Create(Patient);
            if (newPatient == null)
            {
                return Ok("this room is unavilable");
            }
            return Ok(newPatient);
        }
        // DELETE: api/Appointment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            await _patient.Delete(id);
            return Ok("Patient was removed seccussfully");
        }
    }
}