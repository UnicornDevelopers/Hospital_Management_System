using Hospital_System.Models.DTOs.Patient;
using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatient _patient;

        public PatientsController(IPatient patient) {
        
        _patient = patient;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDTO>>> GetPatients()
        {
            var patients = await _patient.GetPatients();
            return Ok(patients);
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDTO>> GetPatient(int id)
        {
            PatientDTO patient = await _patient.GetPatient(id);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        // PUT: api/Appointment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, PatientDTO UpdatedPatient)
        {
            if (id != UpdatedPatient.Id)
            {
                return BadRequest();
            }
            var CurrentPatient = await _patient.UpdatePatient(id, UpdatedPatient);
            return Ok(CurrentPatient);
        }

        // POST: api/Appointment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PatientDTO>> PostPatient(PatientDTO Patient)
        {
            if (Patient == null)
            {
                return Problem("Entity set 'Appointment'  is null.");
            }
            if (Patient.Id == null)
            {
                return NotFound();
            }
            var newPatient = await _patient.Create(Patient);

            return Ok(newPatient);
        }

        // DELETE: api/Appointment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            await _patient.Delete(id);
            return NoContent();
        }
    }
}
