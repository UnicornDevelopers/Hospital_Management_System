using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital_System.Data;
using Hospital_System.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Hospital_System.Models.Interfaces;
using Hospital_System.Models.DTOs;

namespace Hospital_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctor _context;

        public DoctorsController(IDoctor context)
        {
            _context = context;
        }

        // GET: api/Doctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OutDocDTO>>> GetDoctors()
        {
            return await _context.GetDoctors();
        }

        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDTO>> GetDoctor(int id)
        {
            try
            {
                return await _context.GetDoctor(id);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Doctors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<DoctorDTO>> PutDoctor(int id, DoctorDTO doctor)
        {
            if (id != doctor.Id)
            {
                return BadRequest();
            }
            try
            {
                return await _context.UpdateDoctor(id, doctor);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Doctors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OutDocDTO>> PostDoctor(DoctorDTO doctor)
        {
            try
            {
                return await _context.Create(doctor);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
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

        /* private bool DoctorExists(int id)
         {
             return (_context.Doctors?.Any(e => e.Id == id)).GetValueOrDefault();
         }*/

    }
}
