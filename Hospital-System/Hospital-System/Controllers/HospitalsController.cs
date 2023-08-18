using Hospital_System.Models;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Hospital;
using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalsController : ControllerBase
    {
        private readonly IHospital _context;

        public HospitalsController(IHospital context)
        {
            _context = context;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalDTO>>> GetHospitals()
        {
            return await _context.GetHospitals();
        }

        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalDTO>> GetHospital(int id)
        {
            try
            {
                return await _context.GetHospital(id);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Doctors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Hospital>> PutHospital(int id, Hospital hospital)
        {
            if (id != hospital.Id)
            {
                return BadRequest();
            }
            try
            {
                return await _context.UpdateHospital(id, hospital);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Doctors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OutHospitalDTO>> PostHospital(OutHospitalDTO hospital)
        {
            try
            {
                return await _context.Create(hospital);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHospital(int id)
        {
            try
            {
                await _context.Delete(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
