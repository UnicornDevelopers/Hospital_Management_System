﻿using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Department;
using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Data;

namespace Hospital_System.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartment _department;

        public DepartmentsController(IDepartment department)
        {
            _department = department;
        }


        // GET: api/Departments
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OutDepartmentDTO>>> GetDepartments()
        {
            var departments = await _department.GetDepartments();
            return Ok(departments);
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Doctor, Nurse")]
        public async Task<ActionResult<DepartmentDTO>> GetDepartment(int id)
        {
            DepartmentDTO TheDepartment = await _department.GetDepartment(id);

            if (TheDepartment == null)
            {
                return NotFound();
            }

            return TheDepartment;
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, OutDepartmentDTO department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
            var updateDepartment = await _department.UpdateDepartment(id, department);
            return Ok(updateDepartment);
        }

        // POST: api/Departments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InDepartmentDTO>> PostDepartment(InDepartmentDTO department)
        {
            if (department == null)
            {
                return Problem("Entity set 'Departments'  is null.");
            }
            if (department.Id == null)
            {
                return NotFound();
            }
            var newDepartment = await _department.CreateDepartment(department);

            return Ok(newDepartment);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _department.DeleteDepartment(id);
            return NoContent();
        }




        // GET: api/Department/{departmentId}/Doctors
        [HttpGet("{departmentId}/Doctors")]
        [Authorize(Policy = "read")]
        public async Task<ActionResult<List<OutDocDTO>>> GetDoctorsInDepartment(int departmentId)
        {
            var doctors = await _department.GetDoctorsInDepartment(departmentId);
            return Ok(doctors);
        }





    }
}
