﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.Interfaces;
using Hospital_System.Models.DTOs.Nurse;

namespace Hospital_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NursesController : ControllerBase
    {
        private readonly INurse _nurse;

        public NursesController(INurse nurse)
        {
            _nurse = nurse;
        }




        // GET: api/Nurse
        [HttpGet]
        public async Task<ActionResult<NurseDTO>> GetAllNurses()
        {
            var nurses = await _nurse.GetNurses();
            if (nurses == null)
            {
                return NotFound();
            }
            return Ok(nurses);
        }






        // GET: api/Nurse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NurseDTO>> GetNurse(int id)
        {
            var nurse = await _nurse.GetNurse(id);
            if (nurse == null)
            {
                return NotFound();
            }

            return Ok(nurse);
        }




        // PUT: api/Nurse/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<NurseDTO>> PutNurse(int id, NurseDTO nurse)
        {
            if (id != nurse.Id)
            {
                return BadRequest();
            }

            var updatedNurse = _nurse.UpdateNurse(id, nurse);

            if (updatedNurse == null)
                return NotFound();

            return Ok(updatedNurse);
        }





        // POST: api/Nurse
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NurseDTO>> PostNurse(NurseDTO nurse)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                var addedNurse = await _nurse.Create(nurse);

                return CreatedAtAction(nameof(GetNurse), new { id = nurse.Id }, addedNurse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");

            }

        }






        // DELETE: api/Nurse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNurse(int id)
        {
            try
            {
                var nurse = await _nurse.GetNurse(id);

                if (nurse == null)
                    return NotFound();

                await _nurse.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }







    }
}



