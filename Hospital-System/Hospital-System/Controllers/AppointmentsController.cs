using Hospital_System.Models.DTOs;
using Hospital_System.Models.Interfaces;

using Hospital_System.Models;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointment _appointment;

        public AppointmentsController(IAppointment appointment)
        {
            _appointment = appointment;
        }


        // GET: api/Appointment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAppointments()
        {
            var appointment = await _appointment.GetAppointments();
            return Ok(appointment);
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDTO>> GetAppointment(int id)
        {
            AppointmentDTO TheAppointment = await _appointment.GetAppointment(id);

            if (TheAppointment == null)
            {
                return NotFound();
            }

            return TheAppointment;
        }

        // PUT: api/Appointment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(int id, AppointmentDTO appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest();
            }
            var updateAppointment = await _appointment.UpdateAppointment(id, appointment);
            return Ok(updateAppointment);
        }

        // POST: api/Appointment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AppointmentDTO>> PostAppointment(AppointmentDTO appointment)
        {
            if (appointment == null)
            {
                return Problem("Entity set 'Appointment'  is null.");
            }
            if (appointment.Id == null)
            {
                return NotFound();
            }
            var newAppointment = await _appointment.CreateAppointment(appointment);

            return Ok(newAppointment);
        }

        // DELETE: api/Appointment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            await _appointment.DeleteAppointment(id);
            return NoContent();
        }
    }
}
