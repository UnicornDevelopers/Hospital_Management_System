using Hospital_System.Models;
using Hospital_System.Models.DTOs.Medicine;
using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Hospital_System.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly IMedicine _medicine;

        public MedicinesController(IMedicine medicine)
        {
            _medicine = medicine;
        }

    
        // GET: api/Medicines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OutMedicineDTO>>> GetMedicines()
        {
            var TheMedicine = await _medicine.GetMedicines();
            return Ok(TheMedicine);
        }

        // GET: api/Medicine/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OutMedicineDTO>> GetMedicine(int id)
        {
            OutMedicineDTO TheMedicine = await _medicine.GetMedicine(id);

            if (TheMedicine == null)
            {
                return NotFound();
            }

            return TheMedicine;
        }

        // PUT: api/Medicine/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicine(int id, OutMedicineDTO medicine)
        {
            if (id != medicine.Id)
            {
                return BadRequest();
            }
            var updateMedicine = await _medicine.UpdateMedicine(id, medicine);
            return Ok(updateMedicine);
        }

        // POST: api/Medicine
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public async Task<ActionResult<OutMedicineDTO>> PostMedicine(OutMedicineDTO medicine)
        {
            if (medicine == null)
            {
                return Problem("Entity set 'Medicine'  is null.");
            }
            if (medicine.Id == null)
            {
                return NotFound();
            }
            var newMedicine = await _medicine.CreateMedicine(medicine);

            return Ok(newMedicine);
        }

        // DELETE: api/Medicine/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            await _medicine.DeleteMedicine(id);
            return NoContent();
        }
    }
}
