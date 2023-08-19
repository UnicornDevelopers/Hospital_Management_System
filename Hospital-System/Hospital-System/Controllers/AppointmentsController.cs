using Hospital_System.Models.Interfaces;
using Hospital_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hospital_System.Models.DTOs.AppointmentDTO;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Appointment;

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
        public async Task<ActionResult<IEnumerable<OutAppointmentDTO>>> GetAppointments()
        {
            var appointment = await _appointment.GetAppointments();
            return Ok(appointment);
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OutAppointmentDTO>> GetAppointment(int id)
        {
            OutAppointmentDTO TheAppointment = await _appointment.GetAppointment(id);

            if (TheAppointment == null)
            {
                return NotFound();
            }

            return TheAppointment;
        }

        // PUT: api/Appointment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(int id, InAppoinmentDTO appointment)
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
        public async Task<ActionResult<OutAppointmentDTO>> PostAppointment(InAppoinmentDTO appointment)
        {
           
            try
            {
                return await _appointment.CreateAppointment(appointment);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
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
